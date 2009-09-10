using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;

namespace TestInput
{
    [TestFixture] 
    public class TestExcelReader
    {
        ExcelReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new ExcelReader("Qld_FederalResults by Electorate-2004.xls");
        }
        // pre: true
        // post: Returns list of column names
        //ArrayList getColList();
        [Test]
        public void TestGetColListCount()
        {
            Assert.AreEqual(10, reader.getColList());
        }

        [Test]
        public void TestGetColListValues()
        {
            ArrayList cols = reader.getColList();
            Assert.IsTrue(cols.Contains("Division"));
            Assert.IsTrue(cols.Contains("State"));
            Assert.IsTrue(cols.Contains("ALP"));
            Assert.IsTrue(cols.Contains("LP"));
            Assert.IsTrue(cols.Contains("NP"));
            Assert.IsTrue(cols.Contains("DEM"));
            Assert.IsTrue(cols.Contains("GRN"));
            Assert.IsTrue(cols.Contains("OTH"));
            Assert.IsTrue(cols.Contains("LNP"));
            Assert.IsTrue(cols.Contains("ALP"));
        }

        // pre: true
        // post: Returns a sociological table
        //DataTable getSocialTable();

        // pre: true
        // post: Returns first preferences party names
        //ArrayList getFirstPrefParties();

        // pre: true
        // post: Returns TPP party names
        //ArrayList getTPPNames();
    }
}
