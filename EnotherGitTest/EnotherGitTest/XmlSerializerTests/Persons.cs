using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EnotherGitTest.XmlSerializerTests
{

    [Serializable]
    public class Persons
    {
        [XmlArray("Persons")]
        public List<Person> PersonList { get; set; }
    }
}
