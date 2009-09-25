using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NUnit.Framework;
using log4net.Config;
using BlackCat;

namespace TestDataModel
{
    [TestFixture]
    public class TestGeoModelMapInfo
    {
        private String dataFolder = @"..\..\Data";
        private GeoModel model1;
        private ProgressBar bar1;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [SetUp]
        public void setUp()
        {
            String midPath = Path.Combine(dataFolder, "testMap1.mid");
            String mifPath = Path.Combine(dataFolder, "testMap1.mif");
            model1 = new GeoModel();
            bar1 = new ProgressBar();
            model1.buildGeoModel(midPath, mifPath, bar1);
        }
        /// <summary>
        /// Populates the model from the supplied mid and mif files. The progress bar is updated throughout.
        /// Pre: The mid and mif files are valid and the mif columns match the mid data set.
        /// Post: The model is populated.
        /// </summary>
        /// <param name="midFileURL">The url of the mid file. Must not be null.</param>
        /// <param name="mifFileURL">The url of the mif file. Must not be null.</param>
        /// <param name="progressBar">This will be updated during the process.</param>
        //void buildGeoModel(String midFileURL, String mifFileURL, ProgressBar progressBar);
        [Test]
        public void testBuildModelHasRegionIdentifiers()
        {
            String[] ids = model1.getRegionIdentifiers();
            Assert.AreEqual(3, ids.Length);
            Assert.IsTrue(ids.Contains("regionName1"));
            Assert.IsTrue(ids.Contains("regionName2"));
            Assert.IsTrue(ids.Contains("regionName3"));
        }

        [Test]
        public void testBuildModelHasRegionCoordinates()
        {
            String[] coords = model1.getRegionCoordinates("regionName1");
            Assert.AreEqual("152 -27", coords[0]);
            Assert.AreEqual("153 -29", coords[1]);

            coords = model1.getRegionCoordinates("regionName2");
            Assert.AreEqual("156 -34", coords[0]);
            Assert.AreEqual("45 -23", coords[1]);
            Assert.AreEqual("87 -35", coords[2]);
            Assert.AreEqual("345 -86", coords[3]);
            Assert.AreEqual("67 -90", coords[4]);
            Assert.AreEqual("45 -91", coords[5]);
            Assert.AreEqual("56 -90", coords[6]);
            Assert.AreEqual("57 -89", coords[7]);
            Assert.AreEqual("58 -90", coords[8]);
            Assert.AreEqual("56 -91", coords[9]);
            Assert.AreEqual("57 -87", coords[10]);
            Assert.AreEqual("45 -666", coords[11]);

            coords = model1.getRegionCoordinates("regionName3");
            Assert.AreEqual("152 -27", coords[1]);
            Assert.AreEqual("153 -29", coords[2]);
            Assert.AreEqual("154 -28", coords[3]);
            Assert.AreEqual("153 -34", coords[4]);
            Assert.AreEqual("154 -23", coords[5]);
            Assert.AreEqual("155 -27", coords[6]);
            Assert.AreEqual("157 -28", coords[7]);
            Assert.AreEqual("154 -56", coords[8]);
            Assert.AreEqual("123 -78", coords[9]);
        }

    }
}
