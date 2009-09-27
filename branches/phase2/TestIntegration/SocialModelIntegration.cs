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
    public class SocialModelIntegration
    {
        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [Test]
        public void testBuildSocialModel()
        {
            ResourceReader reader = new ResourceReader(@"..\..\Data\Qld_FederalResults by Electorate-2004.xls", new ProgressBar());
            SocialModel model = new SocialModel(reader);

            List<String> columns = model.getColumnNames();
            Assert.IsNotNull(columns);
            Assert.AreEqual(10, columns.Count); 

            List<String> column0 = model.getSocioColumnData(columns[0]);
            Assert.AreEqual(150, column0.Count);
        }
    }
}
