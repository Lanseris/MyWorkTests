using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EnotherGitTest.XmlSerializerTests
{
    [Serializable]
    public class Person
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute("Age")]
        public int Age { get; set; }

        public Person()
        { }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
