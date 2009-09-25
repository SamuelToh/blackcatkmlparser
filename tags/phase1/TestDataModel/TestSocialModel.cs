using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;
using System.Data;
using log4net.Config;

namespace TestDataModel
{
    [TestFixture] 
    class TestSocialModel
    {
        private static string DIVISION = "Division";
        private static string STATE = "State";
        private static string ALP_FIRST_PREF = "ALP%First Preferences";
        private static string LP_FIRST_PREF = "LP%First Preferences";
        private static string NP_FIRST_PREF = "NP%First Preferences";
        private static string DEM_FIRST_PREF = "DEM%First Preferences";
        private static string GRN_FIRST_PREF = "GRN%First Preferences";
        private static string OTH_FIRST_PREF = "OTH%First Preferences";
        private static string LNP_TPP = "LNP%TPP";
        private static string ALP_TPP = "ALP%TPP";

        private SocialModel socialModel;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {
            DataTable table = createValidDataTable();
            DynamicMock mockIResourceReader = new DynamicMock(typeof(IResourceReader));
            mockIResourceReader.ExpectAndReturn("getSocialTable", table);

            socialModel = new SocialModel((IResourceReader)mockIResourceReader.MockInstance);
        }

        private DataTable createValidDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(DIVISION);
            table.Columns.Add(STATE);
            table.Columns.Add(ALP_FIRST_PREF);
            table.Columns.Add(LP_FIRST_PREF);
            table.Columns.Add(NP_FIRST_PREF);
            table.Columns.Add(DEM_FIRST_PREF);
            table.Columns.Add(GRN_FIRST_PREF);
            table.Columns.Add(OTH_FIRST_PREF);
            table.Columns.Add(LNP_TPP);
            table.Columns.Add(ALP_TPP);

            table.Rows.Add(new String[] { "Div1", "QLD", "41.92", "45.29", "0", "1.59", "7.99", "3.21", "48.67", "51.33" }); //ALP wins via TPP
            table.Rows.Add(new String[] { "Div2", "QLD", "53.97", "31.02", "0", "1.27", "4.64", "9.28", "37.13", "62.87" }); //ALP wins
            table.Rows.Add(new String[] { "Div3", "QLD", "38.53", "0", "0", "1.86", "6.77", "52.84", "52.81", "47.19" }); //OTH wins
            table.Rows.Add(new String[] { "Div4", "QLD", "21.47", "53.17", "10.59", "1.48", "4.08", "9.21", "69.88", "30.12" }); //LP wins
            table.Rows.Add(new String[] { "Div5", "QLD", "34.09", "0", "48.73", "1.54", "4.38", "11.26", "57.7", "42.3" }); //LNP wins via TPP

            return table;
        }

        // pre: true
        // post: returns the sociological column names
        //List<String> getColumnNames();
        [Test]
        public void testGetColumnNamesCount()
        {
            List<String> cols = socialModel.getColumnNames();
            Assert.AreEqual(10, cols.Count);
        }

        [Test]
        public void testGetColumnNamesValues()
        {
            List<String> cols = socialModel.getColumnNames();
            Assert.AreEqual(DIVISION, cols[0]);
        }

        // The winner party for the selected electorate is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        //string getSeatWinner(string electorate);
        [Test]
        public void testGetSeatWinnerOne()
        {
            string winner = socialModel.getSeatWinner("Div1");
            Assert.AreEqual("ALP", winner);
        }

        [Test]
        public void testGetSeatWinnerTwo()
        {
            string winner = socialModel.getSeatWinner("Div2");
            Assert.AreEqual("ALP", winner);
        }

        [Test]
        public void testGetSeatWinnerThree()
        {
            string winner = socialModel.getSeatWinner("Div3");
            Assert.AreEqual("OTH", winner);
        }

        [Test]
        public void testGetSeatWinnerFour()
        {
            string winner = socialModel.getSeatWinner("Div4");
            Assert.AreEqual("LP", winner);
        }

        [Test]
        public void testGetSeatWinnerFive()
        {
            string winner = socialModel.getSeatWinner("Div5");
            Assert.AreEqual("LNP", winner);
        }

        [Test]
        public void testGetSeatWinnerCaseInsensitive()
        {
            string winner = socialModel.getSeatWinner("DiV5");
            Assert.AreEqual("LNP", winner);
        }

        // This method retrieves selected column data from the sociological data table
        // pre: tblSocialData is not null and selectedColName is not empty string
        // post: Returns the selected sociological column data
        //List<String> getSocioColumnData(String selectedColName);
        [Test]
        public void testGetSocioColumnDataCount()
        {
            List<String> data = socialModel.getSocioColumnData(DIVISION);
            Assert.AreEqual(5, data.Count);
        }

        [Test]
        public void testGetSocioColumnDataValues()
        {
            List<String> data = socialModel.getSocioColumnData(DIVISION);
            Assert.AreEqual("Div1", data[0]);
            Assert.AreEqual("Div2", data[1]);
            Assert.AreEqual("Div3", data[2]);
            Assert.AreEqual("Div4", data[3]);
            Assert.AreEqual("Div5", data[4]);
        }
    }
}
