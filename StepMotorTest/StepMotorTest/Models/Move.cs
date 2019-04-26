using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StepMotorTest.Models
{
    public class Move
    {
        private static int counter = 1;

        [XmlAttribute]
        public int ID { get; private set; }
        public string Direction { get; set; }
        public double Distance { get; set; }
        public int RecipeID { get; set; }

        public Move()
        {
            ID = counter++;
            Direction = "Forwards";
            Distance = 50.0;
            RecipeID = 0;
        }

        public Move(int recipeID)
        {
            ID = counter++;
            Direction = "Forwards";
            Distance = 50.0;
            RecipeID = recipeID;
        }
    }
}
