using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BlackCat
{
    public interface IGeoModel
    {
        // Writes the model in KML format to the supplied output URL. The progress bar is 
        // updated throughout.
        // Returns a boolean indicating whether a GeoModel was able to be created or not.
        // Pre: The outputFileURL is a valid, writable path and reader is not null.
        // Post: True is returned if a GeoModel could be built from the data provided through the reader, otherwise false has been returned.

        bool BuildGeoModel(IGeoReader reader, ProgressBar progressBar);

        // Property to get and set the value of the data fields defined for each region.
        // The getter returns the dataFieldNames, the setter returns nothing.
        // Pre: True for getting, dataFieldNames is not null for setting
        // Post: The value of dataFieldNames has been returned for getting or the value of 
        // dataFieldNames has been set to the input value for setting.
        List<String> DataFieldNames { get; set; }

        // Property to get an array of the identifiers of all Regions in the model
        // Returns an array containing the region identifiers (names).
        // Pre: True 
        // Post: The array of region names has been returned.
        String[] RegionIdentifiers { get; }

        // Property to get the collection of region objects associated with the geographical data.
        // Returns an array containing the regions.
        // Pre: True
        // Post: The array of regions has been returned.
        Region[] Regions { get; }

        // Property to get the collection of styles that can be used to construct the display. 
        // Returns an array containing these styles.
        // Pre: True. 
        // Post: The array of styles that can be used to display the data has been returned.
        Style[] Styles { get; }

        // Set the style attribute of the region regionIdentifier.
        // There is no return value, as this is a setter.
        // Pre: regionIdentifier is not null and style is not null
        // Post: The style of a region is set.

        void SetRegionStyle(String regionIdentifier, Style style);

        //TODO: comment
        void SetRegionCategory(String regionIdentifier, String districtName);

        // Set the region data of the region regionIdentifier.
        // There is no return value, as this is a setter.
        // Pre: regionIdentifier is not null and style is not null
        //      data value is clear 
        // Post: The datavalue of a region is set.

        void SetRegionSecondaryData
            (bool seatWinnerIsMainDisplay, string regionIdentifier, string data);


    }
}
