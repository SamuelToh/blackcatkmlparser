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
    public class IntegrationTestGeoModel
    {
        [Test]
        public void TestBuildGeoModelSuccess_TestMap1File()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"../../Data/testMap1.mid", @"../../Data/testMap1.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            Assert.IsTrue(success);
        }

        [Test]
        public void TestBuildGeoModelSuccess_QLD_FederalFile()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            Assert.IsTrue(success);
        }

        [Test]
        public void TestDataFieldNamesCount_TestMap1File()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"../../Data/testMap1.mid", @"../../Data/testMap1.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            List<String> dataFields = model.DataFieldNames;
            Assert.AreEqual(3, dataFields.Count);
        }

        [Test]
        public void TestDataFieldNamesCount_QLD_FederalFile()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            List<String> dataFields = model.DataFieldNames;
            Assert.AreEqual(9, dataFields.Count);
        }

        [Test]
        public void TestDataFieldNames_TestMap1File()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"../../Data/testMap1.mid", @"../../Data/testMap1.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            List<String> dataFields = model.DataFieldNames;
            Assert.Contains("E_div_number", dataFields);
            Assert.Contains("Elect_div", dataFields);
            Assert.Contains("Data_Item2", dataFields);
        }

        [Test]
        public void TestDataFieldNames_QLD_FederalFile()
        {
            GeoModel model = new GeoModel();
            IGeoReader reader = new MapInfoReader(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif");
            bool success = model.BuildGeoModel(reader, new ProgressBar());
            List<String> dataFields = model.DataFieldNames;
            Assert.Contains("E_div_number", dataFields);
            Assert.Contains("Elect_div", dataFields);
            Assert.Contains("Numccds", dataFields);
            Assert.Contains("Actual", dataFields);
            Assert.Contains("Projected", dataFields);
            Assert.Contains("Total_Population", dataFields);
            Assert.Contains("Australians_Over_18", dataFields);
            Assert.Contains("Area_SqKm", dataFields);
            Assert.Contains("Sortname", dataFields);
        }
    }
}
