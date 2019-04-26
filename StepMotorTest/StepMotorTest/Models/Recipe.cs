using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StepMotorTest.Models
{
    public class Recipe
    {
        private static int counter = 1;

        [XmlAttribute]
        public int ID { get; private set; }
        public string Name { get; set; }
        public List<Move> Moves { get; set; }

        public Recipe()
        {
            ID = counter++;
            Name = string.Empty;
            Moves = new List<Move>();
        }
    }
}
