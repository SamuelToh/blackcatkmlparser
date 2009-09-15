using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using BlackCat;

namespace TestDataModel
{
    
    public class TestGeoModel
    {
        /// <summary>
        /// Writes the model in KML format to the supplied output URL. The progress bar is updated throughout.
        /// Pre: The outputFileURL is a valid, writable path.
        /// Post: The 
        /// </summary>
        /// <param name="outputFileURL">The path to write the file to.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        //void outputKML(String outputFileURL, ProgressBar progressBar);
        [Test]
        public void testOutputReturnsTrueCreatesFile()
        {
            GeoModel geoModel = new GeoModel();
            String testKML = @"..\..\Data\testKML1.kml";
            geoModel.BuildGeoModel(testKML, new ProgressBar());

            string tempKml = @"..\..\Data\temp.kml";
            Assert.IsTrue(geoModel.OutputKML(tempKml, new ProgressBar()));
            Assert.IsTrue( File.Exists(tempKml));
            File.Delete(tempKml);
        }
            

        /// <summary>
        /// Set the style of a region, if it exists in the model.
        /// </summary>
        /// <param name="regionIdentifier">The region to set.</param>
        /// <param name="style">The style - if this is null, the style will be set to the default style.</param>
        //void setRegionStyle(String regionIdentifier, Style style);
        [Test]
        public void testSetRegionStyleBuiltFromMapInfo()
        {
            //Create the model
            String midFilePath = @"..\..\Data\testMap1.mid";
            String mifFilePath = @"..\..\Data\testMap1.mif";
            GeoModel model = new GeoModel();
            model.BuildGeoModel(midFilePath, mifFilePath, new ProgressBar());

            //and the styles
            Style testStyle1 = new Style("color1", "name1");
            Style testStyle2 = new Style("color2", "name2");
            Style testStyle3 = new Style("color3", "name3");

            //set the styles
            String[] ids = model.GetRegionIdentifiers();
            model.SetRegionStyle(ids[0], testStyle1);
            model.SetRegionStyle(ids[1], testStyle2);
            model.SetRegionStyle(ids[2], testStyle3);

            //check the styles
            Assert.AreSame(testStyle1, model.GetRegionStyle(ids[0]));
            Assert.AreSame(testStyle2, model.GetRegionStyle(ids[1]));
            Assert.AreSame(testStyle3, model.GetRegionStyle(ids[2]));
        }

        [Test]
        public void testSetRegionStyleBuiltFromKML()
        {
            //Create the model
            String testKML = @"..\..\Data\testKML1.kml";
            GeoModel model = new GeoModel();
            model.BuildGeoModel(testKML, new ProgressBar());

            //and the styles
            Style testStyle1 = new Style("color1", "name1");
            Style testStyle2 = new Style("color2", "name2");
            Style testStyle3 = new Style("color3", "name3");

            //set the styles
            String[] ids = model.GetRegionIdentifiers();
            model.SetRegionStyle(ids[0], testStyle1);
            model.SetRegionStyle(ids[1], testStyle2);
            model.SetRegionStyle(ids[2], testStyle3);

            //check the styles
            Assert.AreSame(testStyle1, model.GetRegionStyle(ids[0]));
            Assert.AreSame(testStyle2, model.GetRegionStyle(ids[1]));
            Assert.AreSame(testStyle3, model.GetRegionStyle(ids[2]));
        }

    }
}
