using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class TestSocialReader
    {
        private ILog log;
        private SocialReader reader;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());

            //Only database select is possible, so create reader once
            reader = new SocialReader();
        }    

        // Gets a list of strings representing the names of all the federal seats in the database.
        // Returns the list of strings containing the names of all the federal seats in the 
        // database.
        // Pre: True
        // Post: The list of federal seats contained within the database has been returned.
        // List<String> GetFederalElectorateNames();
        [Test]
        public void TestGetFederalElectorateNamesCount()
        {
            List<String> names = reader.GetFederalElectorateNames();
            Assert.AreEqual(150, names.Count);
        }
        
        [Test]
        public void TestGetFederalElectorateNames()
        {
            List<String> names = reader.GetFederalElectorateNames();
            Assert.IsTrue(names.Contains("Blair"),"No federal electorate named Blair");
            Assert.IsTrue(names.Contains("Bonner"), "No federal electorate named Bonner");
            Assert.IsTrue(names.Contains("Bowman"),"No federal electorate named Bowman");
            Assert.IsTrue(names.Contains("Brisbane"),"No federal electorate named Brisbane");
            Assert.IsTrue(names.Contains("Capricornia"),"No federal electorate named Capricornia");
            Assert.IsTrue(names.Contains("Dawson"),"No federal electorate named Dawson");
            Assert.IsTrue(names.Contains("Dickson"),"No federal electorate named Dickson");
            Assert.IsTrue(names.Contains("Fadden"),"No federal electorate named Fadden");
            Assert.IsTrue(names.Contains("Fairfax"),"No federal electorate named Fairfax");
            Assert.IsTrue(names.Contains("Fisher"),"No federal electorate named Fisher");
            Assert.IsTrue(names.Contains("Forde"),"No federal electorate named Forde");
            Assert.IsTrue(names.Contains("Griffith"),"No federal electorate named Griffith");
            Assert.IsTrue(names.Contains("Groom"),"No federal electorate named Groom");
            Assert.IsTrue(names.Contains("Herbert"),"No federal electorate named Herbert");
            Assert.IsTrue(names.Contains("Hinkler"),"No federal electorate named Hinkler");
            Assert.IsTrue(names.Contains("Kennedy"),"No federal electorate named Kennedy");
            Assert.IsTrue(names.Contains("Leichhardt"),"No federal electorate named Leichhardt");
            Assert.IsTrue(names.Contains("Lilley"),"No federal electorate named Lilley");
            Assert.IsTrue(names.Contains("Longman"),"No federal electorate named Longman");
            Assert.IsTrue(names.Contains("Maranoa"),"No federal electorate named Maranoa");
            Assert.IsTrue(names.Contains("McPherson"),"No federal electorate named McPherson");
            Assert.IsTrue(names.Contains("Moncrieff"),"No federal electorate named Moncrieff");
            Assert.IsTrue(names.Contains("Moreton"),"No federal electorate named Moreton");
            Assert.IsTrue(names.Contains("Oxley"),"No federal electorate named Oxley");
            Assert.IsTrue(names.Contains("Petrie"),"No federal electorate named Petrie");
            Assert.IsTrue(names.Contains("Rankin"),"No federal electorate named Rankin");
            Assert.IsTrue(names.Contains("Ryan"),"No federal electorate named Ryan");
            Assert.IsTrue(names.Contains("Wide Bay"),"No federal electorate named Wide Bay");
        }

        // Gets a FederalElectorateData object containing federal data for federalSeat
        // Returns a FederalElectorateData object containing the federal data required to 
        // calculate the winning party and seat safety for federalSeat.
        // Pre: federalSeat is not null
        // Post: The FederalElectorateData object has been populated with the data for 
        // federalSeat and returned.
        //FederalElectorateData GetFederalResults(string federalSeat);
        [Test]
        public void TestFederalResultsRyan()
        {
            FederalElectorateData data = reader.GetFederalResults("Ryan");
            Assert.AreEqual(31438,data.ALP_Votes);
            Assert.AreEqual(29.41f,data.FirstPref_ALP_Percent);
            Assert.AreEqual(2.42f,data.FirstPref_DEM_Percent);
            Assert.AreEqual(9.76f,data.FirstPref_GRN_Percent);
            Assert.AreEqual(54.76f,data.FirstPref_LP_Percent);
            Assert.AreEqual(0.00f,data.FirstPref_NP_Percent);
            Assert.AreEqual("LIB",data.FirstPref_SeatWinner);
            Assert.AreEqual(2001,data.HeldSince);
            Assert.AreEqual(47997,data.LP_Votes);
        }

        [Test]
        public void TestFederalResultsGroom()
        {
            FederalElectorateData data = reader.GetFederalResults("Groom");
            Assert.AreEqual(25275,data.ALP_Votes);
            Assert.AreEqual(23.98f,data.FirstPref_ALP_Percent);
            Assert.AreEqual(1.06f,data.FirstPref_DEM_Percent);
            Assert.AreEqual(4.00f,data.FirstPref_GRN_Percent);
            Assert.AreEqual(60.36f,data.FirstPref_LP_Percent);
            Assert.AreEqual(0.00f,data.FirstPref_NP_Percent);
            Assert.AreEqual("LIB",data.FirstPref_SeatWinner);
            Assert.AreEqual(1998,data.HeldSince);
            Assert.AreEqual(56121,data.LP_Votes);
        }

        // Gets a StateElectorateData object containing state data for stateSeat
        // Returns a StateElectorateData object containing the state data required in a seat 
        // safety calculation.
        // Pre: stateSeat is not null
        // Post: The StateElectorateData object has been populated with the data for stateSeat // and returned.
        //StateElectorateData GetStateResults(string stateSeat);
        [Test]
        public void TestGetStateResultsBurdekin()
        {
            StateElectorateData data = reader.GetStateResults("Burdekin");
            Assert.AreEqual("NPA",data.TPP_WinnerParty);

        }

        [Test]
        public void TestGetStateResultsMaroochydore()
        {
            StateElectorateData data = reader.GetStateResults("Maroochydore");
            Assert.AreEqual("NPA",data.TPP_WinnerParty);

        }

        [Test]
        public void TestGetStateResultsStafford()
        {
            StateElectorateData data = reader.GetStateResults("Stafford");
            Assert.AreEqual("ALP",data.TPP_WinnerParty);

        }

        // Gets a string array containing the names of all the state seats partially or wholly 
        // contained within the borders of federalSeat.
        // Returns a string array containing the names of every seat that is partially or wholly // within the borders of federalSeat.
        // Pre:federalSeat is not null
        // Post: The array of state seat names that are partially or wholly contained within the // borders of federalSeat have been returned.
        //List<String> GetStateSeats(String federalSeat);
        [Test]
        public void TestGetStateSeatCountBlair()
        {
            List<String> stateSeats = reader.GetStateSeats("Blair");
            Assert.AreEqual(4, stateSeats.Count);
        }

        [Test]
        public void TestGetStateSeatNamesBlair()
        {
            List<String> stateSeats = reader.GetStateSeats("Blair");
            Assert.IsTrue(stateSeats.Contains("Callide"));
            Assert.IsTrue(stateSeats.Contains("Ipswich"));
            Assert.IsTrue(stateSeats.Contains("Ipswich West"));
            Assert.IsTrue(stateSeats.Contains("Nanango"));
        }

        [Test]
        public void TestGetStateSeatCountGroom()
        {
            List<String> stateSeats = reader.GetStateSeats("Groom");
            Assert.AreEqual(5, stateSeats.Count);
        }

        [Test]
        public void TestGetStateSeatNamesGroom()
        {
            List<String> stateSeats = reader.GetStateSeats("Groom");
            Assert.IsTrue(stateSeats.Contains("Lockyer"));
            Assert.IsTrue(stateSeats.Contains("Toowoomba South"));
            Assert.IsTrue(stateSeats.Contains("Toowoomba North"));
            Assert.IsTrue(stateSeats.Contains("Darling Downs"));
            Assert.IsTrue(stateSeats.Contains("Cunningham"));
        }


        [Test]
        public void TestGetStateSeatCountLongman()
        {
            List<String> stateSeats = reader.GetStateSeats("Longman");
            Assert.AreEqual(3, stateSeats.Count);
        }

        [Test]
        public void TestGetStateSeatNamesLongman()
        {
            List<String> stateSeats = reader.GetStateSeats("Longman");
            Assert.IsTrue(stateSeats.Contains("Glass House"));
            Assert.IsTrue(stateSeats.Contains("Pumicestone"));
            Assert.IsTrue(stateSeats.Contains("Nanango"));
        }

    }
}
