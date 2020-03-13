using System;
using System.Collections.Generic;
using System.IO;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var lines = File.ReadLines(path);

            var today = DateTime.Today;
            List<Student> students = new List<Student>();

            foreach(var line in lines)
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
                    TypeOfStudies = studentData[2],
                    ModeOfStudies = studentData[3],
                    Index = Int32.Parse(studentData[4]),
                    BirthDate = Convert.ToDateTime(studentData[5]),
                    Email = studentData[6],
                    MothersName = studentData[7],
                    FathersName = studentData[8]
                };

            }
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
