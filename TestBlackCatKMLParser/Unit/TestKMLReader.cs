using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class TestKMLReader
    {
        private ILog log;
        private List<Region> regions;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        [SetUp]
        public void Setup()
        {
            regions = new List<Region>();
            KMLReader reader = new KMLReader(@"..\..\Data\testKML1.kml");
            regions = reader.ReadRegions(new ProgressWrapper(new ProgressBar()));
        }

        [Test]
        public void TestReadRegionNames()
        {
            Assert.AreEqual("Polygon Test", regions[0].RegionName);
            Assert.AreEqual("Simple placemark", regions[1].RegionName);
            Assert.AreEqual(null, regions[2].RegionName); //the line object has no name tag in our test data
        }

        [Test]
        public void TestReadRegionDataNames0()
        {
            List<String> dataNames = regions[0].DataNames;
            //test can preserve old data
            Assert.AreEqual("OriginalDesc", dataNames[0]);
        }

        [Test]
        public void TestReadRegionDataNames1()
        {
            List<String> dataNames = regions[1].DataNames;
            Assert.AreEqual("OriginalDesc", dataNames[0]);
        }

        [Test]
        public void TestReadRegionData0()
        {
            //test preserved data value
            Assert.AreEqual("Hello description tag!", regions[0].GetDataValue(0));
        }

        [Test]
        public void TestReadRegionData1()
        {
            Assert.AreEqual("This is a test data to see weather our reader " +
                            "is able to record old text value.", regions[1].GetDataValue(0));
        }

        [Test]
        public void TestReadRegionData2()
        {
            //test no preserve data scenario
            Assert.AreEqual(null, regions[2].GetDataValue(0));
        }

        [Test]
        public void TestReadRegionType()
        {
            /*
            Assert.AreEqual("Region", regions[0].RegionType);
            Assert.AreEqual("Region", regions[1].RegionType);
            Assert.AreEqual("Region", regions[2].RegionType);
             */

            //We should be testing what kind of region it is
            Assert.AreEqual("POLYGON", regions[0].RegionType);
            Assert.AreEqual("POINT", regions[1].RegionType);
            Assert.AreEqual("LINE", regions[2].RegionType);
        }

        [Test]
        public void TestReadRegionCoordinatesCount()
        {
            Assert.NotNull(regions);
            Assert.AreEqual(3, regions.Count());
        }

        [Test]
        public void TestReadRegionCoordinates0_0()
        {

            string coordinate = "\r\n153.0033333,-27.442777,0\r\n153.0033333,-27.444444,0" +
                                "\r\n153.005,-27.444444,0\r\n153.005,-27.442777,0" +
                                "\r\n153.0033333,-27.442777,0\r\n";

            Assert.AreEqual(coordinate, regions[0].Coordinates[0]);
        }

        [Test]
        public void TestReadRegionCoordinates1_0()
        {
            string coordinate = "\r\n153.0029465223778,-27.44338980361695,0\r\n";

            Assert.AreEqual(coordinate, regions[1].Coordinates[0]);
        }

        /*
         * Region 2 only consist of 1 set of coordinate
        [Test]
        public void TestReadRegionCoordinates1_1()
        {
            string coordinate = regions[1].Coordinates[1];
            Assert.AreEqual("67 -90\n45 -91", regions[1].Coordinates[1]);
        }
        */

        /*
         * same as above, region 2 only has 1 set of coordinate
        [Test]
        public void TestReadRegionCoordinates1_2()
        {
            string coordinate = regions[1].Coordinates[2];
            Assert.AreEqual("56 -90\n57 -89\n58 -90\n56 -91\n57 -87\n45 -666", regions[1].Coordinates[2]);
        }*/

        [Test]
        public void TestReadRegionCoordinates2_0()
        {
            string coordinate = "\r\n153.00300, -27.443819,0\r\n153.00300,-27.444810,0\r\n";
            Assert.AreEqual(coordinate, regions[2].Coordinates[0]);
        }

        /*
         * We only have 1 set of coordinate for region 3
        [Test]
        public void TestReadRegionCoordinates2_1()
        {
            string coordinate = regions[2].Coordinates[1];
            Assert.AreEqual("154 -28\n153 -34\n154 -23\n155 -27\n157 -28\n154 -56\n123 -78", regions[2].Coordinates[1]);
        }*/
    }
}
