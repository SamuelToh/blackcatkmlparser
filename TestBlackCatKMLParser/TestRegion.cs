using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class TestRegion
    {
        [Test]
        public void TestAddData()
        {
            Region region = new Region();
            region.AddDataValue("Population 230");
            region.AddDataValue("Average tide 2m");
            region.AddDataValue("Yearly Rainfall 22mm");

            Assert.AreEqual("Population 230", region.GetDataValue(0));
            Assert.AreEqual("Average tide 2m", region.GetDataValue(1));
            Assert.AreEqual("Yearly Rainfall 22mm", region.GetDataValue(2));
        }
    }
}
