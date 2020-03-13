using System;
using System.Xml.Serialization;

namespace cw2
{
    public class Student
    {
        [XmlAttribute(AttributeName = "indexNumber")]
        public int Index { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public Studies Studies { get; set; }
    }

    public class Studies
    {
        public string TypeOfStudies { get; set; }
        public string ModeOfStudies { get; set; }
    }
}
