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
        String RegionName { get; set; }

        // Property to get and set the value of the regionStyle attribute
        // The getter returns the regionStyle, the setter returns nothing.
        // Pre: True for getting, regionStyle is not null for setting
        // Post: The value of regionStyle has been returned for getting or the value of 
        // regionStyle has been set to the input value for setting.
        Style RegionStyle { get; set; }

        // Property to get and set the value of the regionType attribute
        // The getter returns the regionType, the setter returns nothing.
        // Pre: True for getting, regionType is not null for setting
        // Post: The value of regionType has been returned for getting or the value of 
        // regionType has been set to the input value for setting.
        String RegionType { get; set; }

        // Property to get and set the value of the coordinates attribute
        // The getter returns the list of coordinates for the Region
        // Pre: True for getting, coordinates is not null for setting
        // Post: The value of coordinates has been returned for getting or the value of 
        // coordinates has been set to the input value for setting.
        List<String> Coordinates { get; set; }

        //Method to get the data value at a particular index
        //Return value is the String data at that index 
        //or null if the index is out of bounds.
        //Pre: Data has been added at that index.
        //Post: The data value has been returned.
        String GetDataValue(int index);

        // Method to add a data value to the region. 
        //Pre: True
        //Post: The data value has been added at the end of the indexed field.
        void AddDataValue(String data);

    }
}
