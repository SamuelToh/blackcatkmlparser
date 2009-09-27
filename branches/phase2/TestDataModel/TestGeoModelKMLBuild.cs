using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlackCat;
using NUnit.Framework;
using log4net.Config;

namespace TestDataModel
{
    [TestFixture]
    public class TestGeoModelKMLBuild
    {
        private GeoModel geoModel;

        //This method is called once, before any tests are run.
        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        //This method is called before each test
        [SetUp]
        public void setUp()
        {
            geoModel = new GeoModel();
            String testKML = @"..\..\Data\testKML1.kml";
            geoModel.BuildGeoModel(new KMLReader(testKML), new ProgressBar());
        }

        [Test]
        public void testGeoModelHasRegionIdentifiers()
        {
            String[] ids = geoModel.GetRegionIdentifiers();
            Assert.AreEqual(3, ids.Length);
            Assert.AreEqual("Polygon Test", ids[0]);
            Assert.AreEqual("Simple placemark", ids[1]);
            Assert.IsNull(ids[2]); //Lines do not have a <name> attribute, hence no identifier
        }

        [Test]
        public void testGeoModelPolygonCoordsCount()
        {
            String[] coords;
            coords = geoModel.GetRegionCoordinates("Polygon Test");
            Assert.AreEqual(1, coords.Length);
        }

        [Test]
        public void testGeoModelPolygonCoordsValues()
        {
            String[] coords;
            String testCoords = "\r\n153.0033333,-27.442777,0\r\n153.0033333,-27.444444,0\r\n153.005,-27.444444,0\r\n153.005,-27.442777,0\r\n153.0033333,-27.442777,0\r\n";
            coords = geoModel.GetRegionCoordinates("Polygon Test");
            Assert.AreEqual(testCoords, coords[0]);
        }

        [Test]
        public void testGeoModelProgressBar()
        {
            GeoModel model = new GeoModel();
            String testKML = @"..\..\Data\testKML1.kml";
            ProgressBar bar = new ProgressBar();

            Assert.AreEqual(0, bar.Value);
            model.BuildGeoModel(new KMLReader(testKML), bar);
            Assert.AreEqual(100, bar.Value);
        }
    }
}
