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
            var path = @"dane.csv";

            var lines = File.ReadLines(path);

            var today = DateTime.Today;
            var students = new HashSet<Student>(new OwnComparer());
            var activeStudies = new List<Course>();

            foreach (var line in lines)
            {
                var studentData = line.Split(",");
                if(studentData.Length < 9)
                {
                    Log(line);
                    break;
                }
                Student std = new Student
                {
                    FName = studentData[0],
                    LName = studentData[1],
                    Studies = new Studies { TypeOfStudies = studentData[2] , ModeOfStudies = studentData[3] },
                    Index = Int32.Parse(studentData[4]),
                    BirthDate = Convert.ToDateTime(studentData[5]),
                    Email = studentData[6],
                    MothersName = studentData[7],
                    FathersName = studentData[8]
                };

                students.Add(std);
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

            FileStream writer = new FileStream(@"data.xml", FileMode.Create); 
            XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia)); 
            serializer.Serialize(writer, uczelnia);

        }

        static void Log(string txt)
        {
            using (StreamWriter sw = File.CreateText("log.txt"))
            {
                sw.WriteLine(txt);

                Console.WriteLine(txt);
            }
        }
    }
}
