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
    public class TestGeoModelMapInfoBuild
    {
        private String midFilePath = @"..\..\Data\testMap1.mid";
        private String mifFilePath = @"..\..\Data\testMap1.mif";
        private GeoModel model;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [SetUp]
        public void setUp()
        {
            model = new GeoModel();
            model.BuildGeoModel(midFilePath, mifFilePath, new ProgressBar());
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
            String[] ids = model.GetRegionIdentifiers();
            Assert.AreEqual(3, ids.Length);
            Assert.IsTrue(ids.Contains("regionName1"));
            Assert.IsTrue(ids.Contains("regionName2"));
            Assert.IsTrue(ids.Contains("regionName3"));
        }

        [Test]
        public void testBuildModelHasRegionCoordinates()
        {
            String[] coords = model.GetRegionCoordinates("regionName1");
            Assert.AreEqual(2, coords.Length);
            Assert.AreEqual("152 -27", coords[0]);
            Assert.AreEqual("153 -29", coords[1]);

            coords = model.GetRegionCoordinates("regionName2");
            Assert.AreEqual(12, coords.Length);
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

            coords = model.GetRegionCoordinates("regionName3");
            Assert.AreEqual(9, coords.Length);
            Assert.AreEqual("152 -27", coords[0]);
            Assert.AreEqual("153 -29", coords[1]);
            Assert.AreEqual("154 -28", coords[2]);
            Assert.AreEqual("153 -34", coords[3]);
            Assert.AreEqual("154 -23", coords[4]);
            Assert.AreEqual("155 -27", coords[5]);
            Assert.AreEqual("157 -28", coords[6]);
            Assert.AreEqual("154 -56", coords[7]);
            Assert.AreEqual("123 -78", coords[8]);
        }

        [Test]
        public void testBuildModelUpdatesProgressBar()
        {
            GeoModel progressModel = new GeoModel();
            ProgressBar bar = new ProgressBar();

            Assert.AreEqual(0, bar.Value);
            progressModel.BuildGeoModel(midFilePath, mifFilePath, bar);
            Assert.AreEqual(100, bar.Value);
        }


        [Test]
        public void testGeoModelBuild()
        {
            GeoModel model = new GeoModel();
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";

            bool result = model.BuildGeoModel(testMid, testMif, new ProgressBar());
            Assert.IsTrue(result);
        }
    }
}
