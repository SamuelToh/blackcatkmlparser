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
    public class TestFederalElectorateData
    {
        [Test]
        public void TestFirstPref_OTH_Percent()
        {
            FederalElectorateData data = new FederalElectorateData();
            data.FirstPref_ALP_Percent = 20;
            data.FirstPref_DEM_Percent = 10;
            data.FirstPref_GRN_Percent = 40;
            data.FirstPref_LP_Percent = 5;
            data.FirstPref_NP_Percent = 15;
            Assert.AreEqual(10, data.FirstPref_OTH_Percent);
        }

        [Test]
        public void TestClearAll()
        {
            FederalElectorateData data = new FederalElectorateData();
            data.FirstPref_ALP_Percent = 5;
            data.FirstPref_DEM_Percent = 50;
            data.FirstPref_GRN_Percent = 7;
            data.FirstPref_LP_Percent = 5;
            data.FirstPref_NP_Percent = 15;
            data.FirstPref_SeatWinner = "DEM";
            data.HeldSince = 2000;
            data.ALP_Votes = 30;
            data.LP_Votes = 70;
            data.FederalElectorateName = "Nowhere";
            Assert.AreEqual(5, data.FirstPref_ALP_Percent);
            Assert.AreEqual(50, data.FirstPref_DEM_Percent);
            Assert.AreEqual(7, data.FirstPref_GRN_Percent);
            Assert.AreEqual(5, data.FirstPref_LP_Percent);
            Assert.AreEqual(15, data.FirstPref_NP_Percent);
            Assert.AreEqual(18, data.FirstPref_OTH_Percent);
            Assert.AreEqual("DEM", data.FirstPref_SeatWinner);
            Assert.AreEqual(2000, data.HeldSince);
            Assert.AreEqual(30, data.ALP_Votes);
            Assert.AreEqual(70, data.LP_Votes);
            Assert.AreEqual("Nowhere", data.FederalElectorateName);

            data.ClearAll();
            Assert.IsNull(data.FirstPref_ALP_Percent);
            Assert.IsNull(data.FirstPref_DEM_Percent);
            Assert.IsNull(data.FirstPref_GRN_Percent);
            Assert.IsNull(data.FirstPref_LP_Percent);
            Assert.IsNull(data.FirstPref_NP_Percent);
            Assert.IsNull(data.FirstPref_OTH_Percent);
            Assert.IsNull(data.FirstPref_SeatWinner);
            Assert.IsNull(data.HeldSince);
            Assert.IsNull(data.ALP_Votes);
            Assert.IsNull(data.LP_Votes);
            Assert.IsNull(data.FederalElectorateName);
        }
    }
}
