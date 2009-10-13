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
    public class IntegrationTestController
    {
        // Loads the MapInfo files midFileURL and mifFileURL into the system, arranging 
        // the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: midFileURL, mifFileURL are not empty strings and progressBar is not null.
        // Post: midFileURL and mifFileURL have been loaded and an integer denoting 
        // success or otherwise has been returned.
        //int LoadMapInfo(String midFileURL, String mifFileURL, ProgressBar progressBar);
        [Test]
        public void TestLoadMapInfo_testMap1File()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            int result = controller.LoadMapInfo(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif", new ProgressBar());
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestLoadMapInfo_QLDFederalFile()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            int result = controller.LoadMapInfo(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif", new ProgressBar());
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestGetMapInfoDataFieldsCount_testMap1File()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif", new ProgressBar());
            List<String> dataFields = controller.GetMapInfoDataFields();
            Assert.AreEqual(3, dataFields.Count);
        }

        [Test]
        public void TestGetMapInfoDataFieldsCount_QLDFederalFile()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif", new ProgressBar());
            List<String> dataFields = controller.GetMapInfoDataFields();
            Assert.AreEqual(9, dataFields.Count);
        }

        [Test]
        public void TestGetMapInfoDataFields_testMap1File()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif", new ProgressBar());
            List<String> dataFields = controller.GetMapInfoDataFields();
            Assert.Contains("E_div_number", dataFields);
            Assert.Contains("Elect_div", dataFields);
            Assert.Contains("Data_Item2", dataFields);
        }

        [Test]
        public void TestGetMapInfoDataFields_QLDFederalFile()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif", new ProgressBar());
            List<String> dataFields = controller.GetMapInfoDataFields();
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

        [Test]
        public void TestCanAddSociologicalData_testMap1File()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif", new ProgressBar());
            bool canAdd = controller.CanAddSociologicalData();
            Assert.IsFalse(canAdd);
        }

        [Test]
        public void TestCanAddSociologicalData_QLDFederalFile()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            controller.LoadMapInfo(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif", new ProgressBar());
            bool canAdd = controller.CanAddSociologicalData();
            Assert.IsTrue(canAdd);
        }

    }
}
