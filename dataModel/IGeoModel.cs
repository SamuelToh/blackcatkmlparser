using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BlackCat
{
    public interface IGeoModel{
    
        /// <summary>
        /// Populates the model from the supplied mid and mif files. The progress bar is updated throughout.
        /// Pre: The mid and mif files are valid and the mif columns match the mid data set.
        /// Post: The model is populated.
        /// </summary>
        /// <param name="midFileURL">The url of the mid file. Must not be null.</param>
        /// <param name="mifFileURL">The url of the mif file. Must not be null.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        void buildGeoModel(String midFileURL, String mifFileURL, ProgressBar progressBar);

        /// <summary>
        /// Populates the model from the supplied kml file. The progress bar is updated throughout.
        /// Pre: The kml file is valid.
        /// Post: The model is populated.
        /// </summary>
        /// <param name="kmlFileURL">The url of the KML file. Must not be null</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        void buildGeoModel(String kmlFileURL, ProgressBar progressBar);

        /// <summary>
        /// Writes the model in KML format to the supplied output URL. The progress bar is updated throughout.
        /// Pre: The outputFileURL is a valid, writable path.
        /// Post: The 
        /// </summary>
        /// <param name="outputFileURL">The path to write the file to.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        void outputKML(String outputFileURL, ProgressBar progressBar);

        /// <summary>
        /// Set the style of a region, if it exists in the model.
        /// </summary>
        /// <param name="regionIdentifier">The region to set.</param>
        /// <param name="style">The style - if this is null, the style will be set to the default style.</param>
        void setRegionStyle(String regionIdentifier, Style style);

        /// <summary>
        /// Return the identifiers for all regions in the model.
        /// </summary>
        /// <returns>An array of region identifiers.</returns>
        String[] getRegionIdentifiers();

        /// <summary>
        /// Return the data column of mapinfo files
        /// </summary>
        /// <returns>A list of string containning the names of column.</returns>
        List<String> DataFieldNames();

    }
}
