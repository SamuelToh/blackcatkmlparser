using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;
using System.Data;
using log4net.Config;

namespace TestInput
{
    [TestFixture] 
    public class TestResourceReader
    {
        ResourceReader reader;
        DataTable table;
        
        //This method is called once, before any tests are run.
        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }       

        [SetUp]
        public void SetUp()
        {
            reader = new ResourceReader(@"..\..\Qld_FederalResults by Electorate-2004.xls");
            table = reader.getSocialTable();
        }

        // pre: true
        // post: Returns a sociological table
        //DataTable getSocialTable();
        [Test]
        public void testTableColumnCount()
        {
            DataColumnCollection cols = table.Columns;
            Assert.AreEqual(10, cols.Count);
        }

        [Test]
        public void testTableRowCount()
        {
            DataRowCollection rows = table.Rows;
            Assert.AreEqual(150, rows.Count);
        }

        [Test]
        public void testTableColumnNames()
        {
            DataColumnCollection cols = table.Columns;
            Assert.AreEqual("Division", cols[0].ColumnName);
            Assert.AreEqual("State", cols[1].ColumnName);
            Assert.AreEqual("ALP%First Preferences", cols[2].ColumnName);
            Assert.AreEqual("LP%First Preferences", cols[3].ColumnName);
            Assert.AreEqual("NP%First Preferences", cols[4].ColumnName);
            Assert.AreEqual("DEM%First Preferences", cols[5].ColumnName);
            Assert.AreEqual("GRN%First Preferences", cols[6].ColumnName);
            Assert.AreEqual("OTH%First Preferences", cols[7].ColumnName);
            Assert.AreEqual("LNP%TPP", cols[8].ColumnName);
            Assert.AreEqual("ALP%TPP", cols[9].ColumnName);
        }

        [Test]
        public void testTableFirstRow()
        {
            DataRow rowOne = table.Rows[0];
            Assert.AreEqual("Adelaide", rowOne[0]);
            Assert.AreEqual("SA", rowOne[1]);
            Assert.AreEqual("41.92", rowOne[2]);
            Assert.AreEqual("45.29", rowOne[3]);
            Assert.AreEqual("0", rowOne[4]);
            Assert.AreEqual("1.59", rowOne[5]);
            Assert.AreEqual("7.99", rowOne[6]);
            Assert.AreEqual("3.21", rowOne[7]);
            Assert.AreEqual("48.67", rowOne[8]);
            Assert.AreEqual("51.33", rowOne[9]);
        }

    }
}
