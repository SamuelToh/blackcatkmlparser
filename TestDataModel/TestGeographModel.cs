using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            String mifPath = "C:\\Documents and Settings\\keatinve\\My Documents\\Uni\\projectData\\QLD_Federal_Electoral_Boundaries.mif";
            String midPath = "C:\\Documents and Settings\\keatinve\\My Documents\\Uni\\projectData\\QLD_Federal_Electoral_Boundaries.mid";
            StreamReader mifReader = new StreamReader(mifPath);
            StreamReader midReader = new StreamReader(midPath);
            geoModel.buildGeographModel(midReader, mifReader);
        }
    }
}
