using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BlackCat
{
    public interface IGeoModel{
    

        bool BuildGeoModel(IGeoReader reader, ProgressBar progressBar);

        /// <summary>
        /// Writes the model in KML format to the supplied output URL. The progress bar is updated throughout.
        /// Pre: The outputFileURL is a valid, writable path.
        /// Post: The 
        /// </summary>
        /// <param name="outputFileURL">The path to write the file to.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        //bool OutputKML(String outputFileURL, ProgressBar progressBar);
        Style[] Styles { get; }

        /// <summary>
        /// Set the style of a region, if it exists in the model.
        /// </summary>
        /// <param name="regionIdentifier">The region to set.</param>
        /// <param name="style">The style - if this is null, the style will be set to the default style.</param>
        void SetRegionStyle(String regionIdentifier, Style style);

        /// <summary>
        /// Return the identifiers for all regions in the model.
        /// </summary>
        /// <returns>An array of region identifiers.</returns>
        String[] RegionIdentifiers { get; }

        Region[] Regions { get; }

        /// <summary>
        /// Return the data column of mapinfo files
        /// </summary>
        /// <returns>A list of string containing the names of column.</returns>
        List<String> DataFieldNames {get;}

    }
}
