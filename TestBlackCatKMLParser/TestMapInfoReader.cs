﻿using System;
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
    public class TestMapInfoReader
    {
        private ILog log;
        private List<Region> regions;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        [SetUp]
        public void Setup()
        {
            regions = new List<Region>();
            MapInfoReader reader = new MapInfoReader(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif");
            regions = reader.ReadRegions(new ProgressBar());
        }

        [Test]
        public void TestReadRegionNames()
        {
            Assert.AreEqual("regionName1",regions[0].RegionName);
            Assert.AreEqual("regionName2",regions[0].RegionName);
            Assert.AreEqual("regionName3", regions[0].RegionName);
        }

        [Test]
        public void TestReadRegionDataNames0()
        {
            List<String> dataNames = regions[0].DataNames;
            Assert.AreEqual("E_div_number",dataNames[0]);
            Assert.AreEqual("Elect_div",dataNames[1]);
            Assert.AreEqual("Data_Item2",dataNames[2]);
        }
        
        [Test]
        public void TestReadRegionDataNames1()
        {
            List<String> dataNames = regions[1].DataNames;
            Assert.AreEqual("E_div_number",dataNames[0]);
            Assert.AreEqual("Elect_div",dataNames[1]);
            Assert.AreEqual("Data_Item2",dataNames[2]);
        }

        [Test]
        public void TestReadRegionData0()
        {
            Assert.AreEqual("data1", regions[0].GetDataValue(0));
            Assert.AreEqual("regionName1", regions[0].GetDataValue(1));
            Assert.AreEqual("data4", regions[0].GetDataValue(2));
        }

        [Test]
        public void TestReadRegionData1()
        {
            Assert.AreEqual("data2", regions[1].GetDataValue(0));
            Assert.AreEqual("regionName2", regions[1].GetDataValue(1));
            Assert.AreEqual("data5", regions[1].GetDataValue(2));
        }

        [Test]
        public void TestReadRegionData2()
        {
            Assert.AreEqual("data3", regions[2].GetDataValue(0));
            Assert.AreEqual("regionName3", regions[2].GetDataValue(1));
            Assert.AreEqual("data6", regions[2].GetDataValue(2));
        }

        [Test]
        public void TestReadRegionType()
        {
            Assert.AreEqual("Region", regions[0].RegionType);
            Assert.AreEqual("Region", regions[1].RegionType);
            Assert.AreEqual("Region", regions[2].RegionType);
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
            Assert.AreEqual("152 -27\n153 -29", regions[0].Coordinates[0]);
        }

        [Test]
        public void TestReadRegionCoordinates1_0()
        {
            Assert.AreEqual("156 -34\n45 -23\n87 -35\n345 -86", regions[1].Coordinates[0]);
        }

        [Test]
        public void TestReadRegionCoordinates1_1()
        {
            Assert.AreEqual("67 -90\n45 -91", regions[1].Coordinates[1]);
        }

        [Test]
        public void TestReadRegionCoordinates1_2()
        {
            Assert.AreEqual("56 -90\n57 -89\n58 -90\n56 -91\n57 -87\n45 -666", regions[1].Coordinates[2]);
        }

        [Test]
        public void TestReadRegionCoordinates2_0()
        {
            Assert.AreEqual("152 -27\n153 -29", regions[2].Coordinates[0]);
        }

        [Test]
        public void TestReadRegionCoordinates2_1()
        {
            Assert.AreEqual("154 -28\n153 -34\n154 -23\n155 -27\n157 -28\n154 -56\n123 -78", regions[2].Coordinates[1]);
        }
    }
}