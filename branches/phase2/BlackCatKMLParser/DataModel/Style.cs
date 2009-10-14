using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class Style : IStyle
    {
        private String colorCode;
        private String styleName;

        //Constructors
        public Style(string colorCode, string styleName)
        {
            this.colorCode = colorCode;
            this.styleName = styleName;
        }

        public Style()
        {
            this.colorCode = "#7dFFFFFF";
            this.styleName = "White";
        }

        //Properties
        public String ColorCode
        {
            get { return this.colorCode; }
            set { this.colorCode = value; }
        }

        //prop
        public String StyleName
        {
            get { return this.styleName; }
            set { styleName = value; }
        }
    }
}
