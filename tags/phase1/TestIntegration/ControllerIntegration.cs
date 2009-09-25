using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;
using System.Data;
using log4net.Config;


namespace TestIntegration
{
    [TestFixture]
    public class ControllerIntegration
    {        
        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [TearDown]
        public void tearDown()
        {
            KMLParserControl.Instance().ClearFields();
        }

        [Test]
        public void testLoadExcel()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            Assert.IsNull(controller.SociologicalModel);

            int result = controller.loadExcel(@"..\..\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());
            Assert.AreEqual(0, result);

            Assert.IsNotNull(controller.SociologicalModel);
        }

        [Test]
        public void testLoadMapInfo()
        {
            KMLParserControl controller = KMLParserControl.Instance();
            Assert.IsNull(controller.GeologicalModel);

            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            int result = controller.loadMapInfo(testMid,testMif, new ProgressBar());
            Assert.AreEqual(0, result);

            Assert.IsNotNull(controller.GeologicalModel);
        }

        [Test]
        public void testCanLink()
        {
            KMLParserControl controller = KMLParserControl.Instance();

            //Load geo
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            controller.loadMapInfo(testMid, testMif, new ProgressBar());

            //Load excel
            controller.loadExcel(@"..\..\Data\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());

            string geoField = controller.getGeographicalDataFields()[0];
            string socioField = controller.getSociologicalDataFields()[0];

            bool canLink = controller.setLinkFields(geoField, socioField);
            Assert.IsTrue(canLink);
        }

        [Test]
        public void testCanOutputFromMapInfo()
        {
            KMLParserControl controller = KMLParserControl.Instance();

            //Load geo
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            controller.loadMapInfo(testMid, testMif, new ProgressBar());

            String testKML = @"..\..\Data\tempKml.kml";
            controller.generateKMLFile(testKML, new ProgressBar());
            Assert.IsTrue(File.Exists(testKML));
            File.Delete(testKML);
        }


        [Test]
        public void testCanOutputFromMapInfoPlusExcel()
        {
            KMLParserControl controller = KMLParserControl.Instance();

            //Load geo
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            controller.loadMapInfo(testMid, testMif, new ProgressBar());

            //Load excel
            controller.loadExcel(@"..\..\Data\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());

            string geoField = controller.getGeographicalDataFields()[0];
            string socioField = controller.getSociologicalDataFields()[0];

            //Combine the data
            bool link = controller.setLinkFields(geoField, socioField);
            Assert.IsTrue(link);

            String testKML = @"..\..\Data\tempKml.kml";
            Assert.IsFalse(File.Exists(testKML));
            controller.generateKMLFile(testKML, new ProgressBar());
            Assert.IsTrue(File.Exists(testKML));
            File.Delete(testKML);
        }
    }
}
