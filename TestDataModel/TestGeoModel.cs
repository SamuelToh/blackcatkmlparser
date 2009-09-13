using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestDataModel
{
    [TestFixture] 
    public class TestGeoModel
    {

        /// <summary>
        /// Populates the model from the supplied kml file. The progress bar is updated throughout.
        /// Pre: The kml file is valid.
        /// Post: The model is populated.
        /// </summary>
        /// <param name="kmlFileURL">The url of the KML file. Must not be null</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        //void buildGeoModel(String kmlFileURL, ProgressBar progressBar);

        /// <summary>
        /// Writes the model in KML format to the supplied output URL. The progress bar is updated throughout.
        /// Pre: The outputFileURL is a valid, writable path.
        /// Post: The 
        /// </summary>
        /// <param name="outputFileURL">The path to write the file to.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        //void outputKML(String outputFileURL, ProgressBar progressBar);

        /// <summary>
        /// Set the style of a region, if it exists in the model.
        /// </summary>
        /// <param name="regionIdentifier">The region to set.</param>
        /// <param name="style">The style - if this is null, the style will be set to the default style.</param>
        //void setRegionStyle(String regionIdentifier, Style style);

        /// <summary>
        /// Return the identifiers for all regions in the model.
        /// </summary>
        /// <returns>An array of region identifiers.</returns>
        //String[] getRegionIdentifiers();

    }
}
