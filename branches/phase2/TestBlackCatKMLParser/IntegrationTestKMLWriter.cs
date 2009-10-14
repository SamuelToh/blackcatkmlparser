using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class IntegrationTestKMLWriter
    {
        private ILog log;
        private String[] testLines;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        private void WriteReadTestKMLNoExtras()
        {
            GeoModel model = new GeoModel();
            MapInfoReader reader = new MapInfoReader(@"..\..\Data\testMap1.mid", @"..\..\Data\testMap1.mif");
            model.BuildGeoModel(reader, new ProgressWrapper(new ProgressBar()));

            KMLWriter writer = new KMLWriter();
            String tempKmlPath = @"..\..\Data\tempFile.kml";
            writer.WriteToFile(model, new List<string>(), tempKmlPath, new ProgressWrapper(new ProgressBar()));

            testLines = File.ReadAllLines(tempKmlPath);
            for (int i = 0; i < testLines.Length; i++)
                testLines[i] = testLines[i].Trim();
        }

        [Test]
        public void TestWriteStyleNoSociological()
        {
            WriteReadTestKMLNoExtras();
            Assert.Contains("<Style id=\"White\">", testLines);
        }
    }
}
