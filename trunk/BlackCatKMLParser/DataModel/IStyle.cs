using System;
namespace BlackCat
{
    public interface IStyle
    {
        // Property to get and set the value of the colorCode attribute
        // The getter returns the colorCode, the setter returns nothing.
        // Pre: True for getting, colorCode is not null for setting
        // Post: The value of colorCode has been returned for getting or the value of 
        // colorCode has been set to the input value for setting.
        String ColorCode { get; set; }

        // Property to get and set the value of styleName.
        // The getter returns the styleName, the setter returns nothing.
        // Pre: True for getting, styleName is not null for setting
        // Post: The value of styleName has been returned for getting or the value of 
        // styleName has been set to the input value for setting.
        String StyleName { get; set; }

    }
}
