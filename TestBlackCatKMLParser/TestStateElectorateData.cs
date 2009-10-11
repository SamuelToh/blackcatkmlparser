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
    public class TestStateElectorateData
    {
        [Test]
        public void TestClearAll()
        {
            StateElectorateData data = new StateElectorateData();
            data.StateElectorateName = "Somewhere";
            data.TPP_WinnerParty = "GRN";
            Assert.AreEqual("Somewhere", data.StateElectorateName);
            Assert.AreEqual("GRN", data.TPP_WinnerParty);

            data.ClearAll();
            Assert.IsNull(data.StateElectorateName);
            Assert.IsNull(data.TPP_WinnerParty);
        }
    }
}
