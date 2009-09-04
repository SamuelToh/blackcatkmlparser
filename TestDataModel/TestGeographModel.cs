using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;

namespace TestDataModel
{
    [TestFixture] 
    public class TestGeographModel
    {
        [Test]
        public void TestCountGeographicRegions()
        {
            GeographModel geoModel = new GeographModel();
            Assert.AreEqual(0, geoModel.countGeographicRegions());
        }
    }
}
