using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ICategory
    {
        // Property to get and set the value of the CategoryName attribute
        // The getter returns the CategoryName, the setter returns nothing.
        // Pre: True for getting, CategoryName is not null for setting
        // Post: The value of CategoryName has been returned for getting or the value of 
        // CategoryName has been set to the input value for setting.
        String CategoryName { get; set; }

        // Property to get and set the value of CategoryDesc.
        // The getter returns the CategoryDesc, the setter returns nothing.
        // Pre: True for getting, CategoryDesc is not null for setting
        // Post: The value of CategoryDesc has been returned for getting or the value of 
        // CategoryDesc has been set to the input value for setting.
        String CategoryDesc { get; set; }
    }
}
