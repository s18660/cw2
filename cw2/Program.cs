using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathIn = @"data.csv";
            var pathOut = @"result.xml";
            var fileExt = "xml";

            try
            {
                pathIn = args[0];
                pathOut = args[1];
                fileExt = args[2];
            }
            catch (IndexOutOfRangeException e)
            {

                Console.WriteLine(e);
            }

            IEnumerable<string> lines = new List<string>();
            try
            {
                lines = File.ReadLines(pathIn);
            }catch(FileNotFoundException e)
            {
                Log(e.Message);
                throw new FileNotFoundException("Plik o tej nazwie nie istnieje");
            }

            var today = DateTime.Today;
            var students = new HashSet<Student>(new OwnComparer());
            var activeStudies = new List<Course>();

            bool shouldAddToSet = true;

            foreach (var line in lines)
            {
                var studentData = line.Split(",");
                if(studentData.Length < 9)
                {
                    Log(line);
                    break;
                }

                foreach(var data in studentData)
                {
                    if(string.IsNullOrEmpty(data))
                    {
                        shouldAddToSet = false;
                        break;
                    }
                }
                if (shouldAddToSet)
                {
                    Student std = new Student
                    {
                        FName = studentData[0],
                        LName = studentData[1],
                        Studies = new Studies { TypeOfStudies = studentData[2], ModeOfStudies = studentData[3] },
                        Index = Int32.Parse(studentData[4]),
                        BirthDate = Convert.ToDateTime(studentData[5]),
                        Email = studentData[6],
                        MothersName = studentData[7],
                        FathersName = studentData[8]
                    };

                    students.Add(std);
                }
            }

            foreach(var student in students)
            {
                var index = activeStudies.FindIndex(x => x.courseName == student.Studies.TypeOfStudies);

                if (index >= 0)
                {
                    activeStudies[index].numberOfStudents += 1;
                }
                else
                {
                    activeStudies.Add(new Course { courseName = student.Studies.TypeOfStudies, numberOfStudents = 1 });
                }
            }

            var uczelnia = new Uczelnia()
            {
                createdAt = today.ToShortDateString(),
                author = "Rafał Piórek",
                studenci = students,
                activeStudies = activeStudies
            };

            try
            {
                FileStream writer = new FileStream(pathOut, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));
                serializer.Serialize(writer, uczelnia);
            }
            catch(IOException e)
            {
                Log(e.Message);
                throw new ArgumentException("Podana ścieżka jest niepoprawna");
            }

        }

        static void Log(string txt)
        {
            using (StreamWriter sw = File.CreateText("log.txt"))
            {
                sw.WriteLine(txt);
            }
        }
    }
}
