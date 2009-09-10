using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class Style
    {
        private string color;
        private string name;

        //Constructor
        public Style(string color, string name)
        {
            this.color = color;
            this.name = name;
        }

        //Properties

        public String Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public String Name
        {
            get { return this.name; }
            set { name = value; }
        }
    }
}
