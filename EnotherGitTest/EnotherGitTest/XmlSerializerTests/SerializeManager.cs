using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EnotherGitTest.XmlSerializerTests
{
    class SerializeManager
    {
        public Persons Persons { get; set; }

        [XmlArray("Persons")]
        public List<Person> Persons_V2 { get; set; }

        public XmlSerializer FormatterMetanit { get; set; }
        public XmlSerializer FormatterMetanit_V2 { get; set; }


        public delegate void SerializeStateHandler(string massage);

        public event SerializeStateHandler Notify;

        public SerializeManager()
        {


            #region Пример с классом Persons
            FormatterMetanit = new XmlSerializer(typeof(Persons));

            Persons = new Persons();
            Persons.PersonList = new List<Person>();
            Persons.PersonList.Add(new Person("Name1", 22));
            Persons.PersonList.Add(new Person("Name2", 32));
            #endregion

            #region MyRegion
            FormatterMetanit_V2 = new XmlSerializer(typeof(List<Person>),new XmlRootAttribute("Persons"));


            Persons_V2 = new List<Person>();
            Persons_V2.Add(new Person("Name1", 22));
            Persons_V2.Add(new Person("Name2", 32));

            #endregion
        }

        public bool TrySerialize()
        {
            #region Пример с классом Persons
            using (FileStream fs = new FileStream("persons.xml", FileMode.Create))
            {
                FormatterMetanit.Serialize(fs, Persons);
            }
            #endregion

            #region MyRegion
            using (FileStream fs = new FileStream("persons_v2.xml", FileMode.Create))
            {
                FormatterMetanit_V2.Serialize(fs, Persons_V2);
            }
            #endregion

            Notify?.Invoke("Сериализация успешна");

            return true;

        }

        public bool TryDeserialize()
        {
            using (FileStream fs = new FileStream("persons_v2.xml", FileMode.Open))
            {
                var _persons = FormatterMetanit_V2.Deserialize(fs);
            }

            return true;
        }
    }
}
