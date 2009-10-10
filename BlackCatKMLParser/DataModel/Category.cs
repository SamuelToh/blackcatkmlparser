using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class Category : ICategory
    {
        private String categoryName;
        private String categoryDesc;

        //Constructor
        public Category() { }

        public Category(string categoryName,
                            string categoryDesc)
        {
            this.categoryName = categoryName;
            this.categoryDesc = categoryDesc;
        }

        //Properties
        public String CategoryName
        {
            get { return this.categoryName; }
            set { this.categoryName = value; }
        }

        //prop
        public String CategoryDesc
        {
            get { return this.categoryDesc; }
            set { categoryDesc = value; }
        }
    }
}
