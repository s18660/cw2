using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace cw2
{
    public class Uczelnia
    {
        [XmlAttribute]
        public string createdAt { get; set; }
        [XmlAttribute]
        public string author { get; set; }
        public HashSet<Student> studenci { get; set; }
        public List<Course> activeStudies { get; set; }
    }

    public class Course
    {
        [XmlAttribute]
        public string courseName { get; set; }
        [XmlAttribute]
        public int numberOfStudents { get; set; }
    }
}
