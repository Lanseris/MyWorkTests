using System;
using System.Collections.Generic;
using System.Text;

namespace EnotherGitTest.XmlSerializerTests
{
    public class Good
    {
        public int GoodId { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Articul { get; set; }

        public string Currency { get; set; }

        public Producer Producer { get; set; }

        public GoodType GoodType { get; set; }

    }
}
