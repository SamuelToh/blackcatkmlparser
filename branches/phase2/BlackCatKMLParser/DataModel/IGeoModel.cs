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

        // Retrieves the list of additional data fields that are in the MapInfo file.
        // Returns a string list containing the names of the additional data fields
        // Pre: True
        // Post: The string list of available additional MapInfo data has been returned.

        List<String> DataFieldNames();

        // Gets the collection of region names that appear in the geographical data.
        // Returns an array containing the region identifiers (names).
        // Pre: True
        // Post: The array of region names has been returned.

        String[] GetRegionIdentifiers();

        // Gets the collection of region objects associated with the geographical data.
        // Returns an array containing the regions.
        // Pre: True
        // Post: The array of regions has been returned.

        Region[] GetRegions();

        // Gets the collection of styles that can be used to construct the display. 
        // Returns an array containing these styles.
        // Pre: True. 
        // Post: The array of styles that can be used to display the data has been returned.

        Style[] GetStyles();

        // Set the style attribute of the region regionIdentifier.
        // There is no return value, as this is a setter.
        // Pre: regionIdentifier is not null and style is not null
        // Post: The style of a region is set.

        void SetRegionStyle(String regionIdentifier, Style style);


    }
}
