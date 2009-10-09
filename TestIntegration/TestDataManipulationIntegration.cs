using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlackCat;
using NUnit.Framework;
using log4net.Config;

namespace TestIntegration
{
    [TestFixture]
    public class TestDataManipulationIntegration
    {
        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [Test]
        public void testCanLinkFiles()
        {
            ResourceReader reader = new ResourceReader(@"..\..\Data\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());
            SocialModel socioModel = new SocialModel(reader);

            GeoModel geoModel = new GeoModel();
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            geoModel.BuildGeoModel(new MapInfoReader(testMid, testMif), new ProgressBar());

            DataMerger merger = new DataMerger();

            String geoCol = geoModel.DataFieldNames()[0];
            String socioCol = socioModel.getColumnNames()[0];

            bool result = merger.canLink(geoModel, geoCol, socioModel, socioCol);
            Assert.IsTrue(result);
        }

        [Test]
        public void testLinkFiles()
        {
            ResourceReader reader = new ResourceReader(@"..\..\Data\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());
            SocialModel socioModel = new SocialModel(reader);

            GeoModel geoModel = new GeoModel();
            String testMid = @"..\..\Data\QLD_Federal_Electoral_Boundaries.MID";
            String testMif = @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif";
            geoModel.BuildGeoModel(new MapInfoReader(testMid, testMif), new ProgressBar());

            DataMerger merger = new DataMerger();

            String geoCol = geoModel.DataFieldNames()[0];
            String socioCol = socioModel.getColumnNames()[0];

            int result = merger.linkDataModels(geoModel, geoCol, socioModel, socioCol);
            Assert.AreEqual(0, result);
        }
    }
}
