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
            Assert.AreEqual(1, coords.Length);
            Assert.AreEqual("152,-27,0\r\n153,-29,0", coords[0].Trim());

            coords = model.GetRegionCoordinates("regionName2");
            Assert.AreEqual(1, coords.Length);
            String coordString = "156,-34,0\r\n45,-23,0\r\n87,-35,0\r\n345,-86,0\r\n" +
                "67,-90,0\r\n45,-91,0\r\n56,-90,0\r\n57,-89,0\r\n" +
                "58,-90,0\r\n56,-91,0\r\n57,-87,0\r\n45,-666,0";
            Assert.AreEqual(coordString, coords[0].Trim());

            coords = model.GetRegionCoordinates("regionName3");
            Assert.AreEqual(1, coords.Length);
            coordString = "152,-27,0\r\n153,-29,0\r\n154,-28,0\r\n" +
                "153,-34,0\r\n154,-23,0\r\n155,-27,0\r\n157,-28,0\r\n154,-56,0\r\n123,-78,0";
            Assert.AreEqual(coordString, coords[0].Trim());
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
