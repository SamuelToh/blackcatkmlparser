using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class TestGeoModel
    {
        private ILog log;
        private GeoModel model;
        private Region testRegion1;
        private Region testRegion2;
        private Region testRegion3;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        [SetUp]
        public void Setup()
        {
            model = new GeoModel();

            //Create test list of regions
            List<Region> regionList = new List<Region>();            
            List<String> dataNames = new List<string>();
            dataNames.Add("data1");
            dataNames.Add("data2");
            dataNames.Add("data3");
            testRegion1 = new Region();
            testRegion1.RegionName = "reg1";
            testRegion1.DataNames = dataNames;
            testRegion1.AddDataValue("Value1-1");
            testRegion1.AddDataValue("Value1-2");
            testRegion1.AddDataValue("Value1-3");
            regionList.Add(testRegion1);
            testRegion2 = new Region();
            testRegion2.RegionName = "reg2";
            testRegion2.DataNames = dataNames;
            testRegion2.AddDataValue("Value2-1");
            testRegion2.AddDataValue("Value2-2");
            testRegion2.AddDataValue("Value2-3");
            regionList.Add(testRegion2);
            testRegion3 = new Region();
            testRegion3.RegionName = "reg3";
            testRegion3.DataNames = dataNames;
            testRegion3.AddDataValue("Value3-1");
            testRegion3.AddDataValue("Value3-2");
            testRegion3.AddDataValue("Value3-3");
            regionList.Add(testRegion3);
            //Create mock GeoReader
            DynamicMock mockIGeoReader = new DynamicMock(typeof(IGeoReader));
            ProgressWrapper bar = new ProgressWrapper(new ProgressBar());
            mockIGeoReader.ExpectAndReturn("ReadRegions", regionList, new Object[]{bar});
            //Build model            
            model.BuildGeoModel((IGeoReader)mockIGeoReader.MockInstance, bar);
        }

        // Writes the model in KML format to the supplied output URL. The progress bar is 
        // updated throughout.
        // Returns a boolean indicating whether a GeoModel was able to be created or not.
        // Pre: The outputFileURL is a valid, writable path and reader is not null.
        // Post: True is returned if a GeoModel could be built from the data provided through the reader, otherwise false has been returned.
        // bool BuildGeoModel(IGeoReader reader, ProgressBar progressBar);

        // Property to get and set the value of the data fields defined for each region.
        // The getter returns the dataFieldNames, the setter returns nothing.
        // Pre: True for getting, dataFieldNames is not null for setting
        // Post: The value of dataFieldNames has been returned for getting or the value of 
        // dataFieldNames has been set to the input value for setting.
        // List<String> DataFieldNames { get; set; }
        [Test]
        public void TestDataFieldNamesCount()
        {
            List<String> dataFields = model.DataFieldNames;
            Assert.AreEqual(3, dataFields.Count);
        }

        [Test]
        public void TestDataFieldNames()
        {
            List<String> dataFields = model.DataFieldNames;
            Assert.Contains("data1", dataFields);
            Assert.Contains("data2", dataFields);
            Assert.Contains("data3", dataFields);
        }

        // Property to get an array of the identifiers of all Regions in the model
        // Returns an array containing the region identifiers (names).
        // Pre: True 
        // Post: The array of region names has been returned.
        // String[] RegionIdentifiers { get; }
        [Test]
        public void TestRegionIdentifiersCount()
        {
            String[] regionIdentifiers = model.RegionIdentifiers;
            Assert.AreEqual(3, regionIdentifiers.Length);
        }

        [Test]
        public void TestRegionIdentifiers()
        {
            String[] regionIdentifiers = model.RegionIdentifiers;
            Assert.Contains("reg1",regionIdentifiers);
            Assert.Contains("reg2", regionIdentifiers);
            Assert.Contains("reg3", regionIdentifiers);
        }

        // Property to get the collection of region objects associated with the geographical data.
        // Returns an array containing the regions.
        // Pre: True
        // Post: The array of regions has been returned.
        // Region[] Regions { get; }
        [Test]
        public void TestRegionsCount()
        {
            Assert.AreEqual(3, model.Regions.Length);
        }

        [Test]
        public void TestRegions()
        {
            Region[] regions = model.Regions;
            Assert.Contains(testRegion1, regions);
            Assert.Contains(testRegion2, regions);
            Assert.Contains(testRegion3, regions);
        }

        // Property to get the collection of styles that can be used to construct the display. 
        // Returns an array containing these styles.
        // Pre: True. 
        // Post: The array of styles that can be used to display the data has been returned.
        // Style[] Styles { get; }
        [Test]
        public void TestStylesInitial()
        {
            Style[] styles = model.Styles;
            Assert.AreEqual(0, styles.Length);
        }

        [Test]
        public void TestStyleAddOneCount()
        {
            Style style = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style;

            Style[] storedStyles = model.Styles;
            Assert.AreEqual(1, storedStyles.Length);
        }

        [Test]
        public void TestStyleAddOne()
        {
            Style style1 = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style1;

            Style[] storedStyles = model.Styles;
            Assert.AreEqual("Pink", storedStyles[0].StyleName);
            Assert.AreEqual("#123456", storedStyles[0].ColorCode);
        }

        [Test]
        public void TestStyleAddManyCount()
        {
            Style style1 = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style1;

            Style style2 = new Style("#234567", "Purple");
            model.Regions[1].RegionStyle = style2;

            Style style3 = new Style("#345678", "Orange");
            model.Regions[2].RegionStyle = style3;

            Style[] storedStyles = model.Styles;
            Assert.AreEqual(3, storedStyles.Length);
        }

        [Test]
        public void TestStyleAddMany()
        {
            Style style1 = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style1;

            Style style2 = new Style("#234567", "Purple");
            model.Regions[1].RegionStyle = style2;

            Style style3 = new Style("#345678", "Orange");
            model.Regions[2].RegionStyle = style3;

            Style[] storedStyles = model.Styles;

            Assert.AreEqual("Pink", storedStyles[0].StyleName);
            Assert.AreEqual("#123456", storedStyles[0].ColorCode);

            Assert.AreEqual("Purple", storedStyles[1].StyleName);
            Assert.AreEqual("#234567", storedStyles[1].ColorCode);

            Assert.AreEqual("Orange", storedStyles[2].StyleName);
            Assert.AreEqual("#345678", storedStyles[2].ColorCode);
        }

        [Test]
        public void TestStyleAddSameCount()
        {
            Style style1 = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style1;

            model.Regions[1].RegionStyle = style1;

            Style style3 = new Style("#345678", "Orange");
            model.Regions[2].RegionStyle = style3;

            Style[] storedStyles = model.Styles;
            Assert.AreEqual(2, storedStyles.Length);
        }

        [Test]
        public void TestStyleAddSame()
        {
            Style style1 = new Style("#123456", "Pink");
            model.Regions[0].RegionStyle = style1;

            model.Regions[1].RegionStyle = style1;

            Style style3 = new Style("#345678", "Orange");
            model.Regions[2].RegionStyle = style3;

            Style[] storedStyles = model.Styles;

            Assert.AreEqual("Pink", storedStyles[0].StyleName);
            Assert.AreEqual("#123456", storedStyles[0].ColorCode);

            Assert.AreEqual("Orange", storedStyles[1].StyleName);
            Assert.AreEqual("#345678", storedStyles[1].ColorCode);
        }

        // Set the style attribute of the region regionIdentifier.
        // There is no return value, as this is a setter.
        // Pre: regionIdentifier is not null and style is not null
        // Post: The style of a region is set.
        // void SetRegionStyle(String regionIdentifier, Style style);
        [Test]
        public void TestSetRegionStyleOne()
        {
            Style style1 = new Style("#123456", "Pink");
            model.SetRegionStyle("reg1", style1);
            Assert.AreSame(style1, testRegion1.RegionStyle);
        }

        [Test]
        public void TestSetRegionStyleTwo()
        {
            Style style1 = new Style("#123456", "Pink");
            Style style2 = new Style("#234567", "Purple");
            model.SetRegionStyle("reg1", style1);
            model.SetRegionStyle("reg2", style2);
            Assert.AreSame(style1, testRegion1.RegionStyle);
            Assert.AreSame(style2, testRegion2.RegionStyle);
        }

        [Test]
        public void TestSetRegionStyleThree()
        {
            Style style1 = new Style("#123456", "Pink");
            Style style2 = new Style("#234567", "Purple");
            Style style3 = new Style("#345678", "Orange");
            model.SetRegionStyle("reg1", style1);
            model.SetRegionStyle("reg2", style2);
            model.SetRegionStyle("reg3", style3);
            Assert.AreSame(style1, testRegion1.RegionStyle);
            Assert.AreSame(style2, testRegion2.RegionStyle);
            Assert.AreSame(style3, testRegion3.RegionStyle);
        }

        [Test]
        public void TestSetRegionStyleRepeat()
        {
            Style style1 = new Style("#123456", "Pink");
            Style style2 = new Style("#234567", "Purple");
            model.SetRegionStyle("reg1", style1);
            model.SetRegionStyle("reg2", style2);
            model.SetRegionStyle("reg3", style2);
            Assert.AreSame(style1, testRegion1.RegionStyle);
            Assert.AreSame(style2, testRegion2.RegionStyle);
            Assert.AreSame(style2, testRegion3.RegionStyle);
        }

        [Test]
        public void TestSetRegionStyleReset()
        {
            Style style1 = new Style("#123456", "Pink");
            Style style2 = new Style("#234567", "Purple");
            model.SetRegionStyle("reg1", style1);
            Assert.AreSame(style1, testRegion1.RegionStyle);
            model.SetRegionStyle("reg1", style2);
            Assert.AreSame(style2, testRegion1.RegionStyle);
        }
    }
}
