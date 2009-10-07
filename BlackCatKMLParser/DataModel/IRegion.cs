using System;
namespace BlackCat
{
    public interface IRegion
    {
        // Property to get and set the value of the regionName attribute
        // The getter returns the regionName, the setter returns nothing.
        // Pre: True for getting, regionName is not null for setting
        // Post: The value of regionName has been returned for getting or the value of 
        // regionName has been set to the input value for setting.

        String regionName { get; set; }

        // Property to get and set the value of the regionStyle attribute
        // The getter returns the regionStyle, the setter returns nothing.
        // Pre: True for getting, regionStyle is not null for setting
        // Post: The value of regionStyle has been returned for getting or the value of 
        // regionStyle has been set to the input value for setting.

        Style regionStyle { get; set; }

        // Property to get and set the value of the regionType attribute
        // The getter returns the regionType, the setter returns nothing.
        // Pre: True for getting, regionType is not null for setting
        // Post: The value of regionType has been returned for getting or the value of 
        // regionType has been set to the input value for setting.

        String regionType { get; set; }

    }
}
