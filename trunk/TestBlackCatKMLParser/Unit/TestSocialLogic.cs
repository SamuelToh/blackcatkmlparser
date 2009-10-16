using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    [TestFixture]
    public class TestSocialLogic
    {
        private SocialLogic logic;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        // Calculates how safe each Federal seat is using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as 
        // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre: Model is not null and isMainDisplay is not null.
        // Post: Seat safety data has been added to the model according to whether 
        // isMainDisplay is true or false.
        //void CalculateSeatSafety(GeoModel model, Boolean isMainDisplay);
        private String ConvertSafetyPercentageToCategory(float safetyPercentage)
        {
            if (safetyPercentage <= 5)
                return "Marginal Seat";
            else if (safetyPercentage <= 10)
                return "Moderately Safe";
            else if (safetyPercentage <= 15)
                return "Safe";
            else if (safetyPercentage <= 25)
                return "Very Safe";
            else
                return "Rock Solid";
        }
        
        [Test]
        public void TestCalculateSeatSafety()
        {
            FederalElectorateData federalResults1 = new FederalElectorateData();
            federalResults1.FederalElectorateName = "fed1";
            federalResults1.FirstPref_SeatWinner = "LNP";
            federalResults1.FirstPref_LP_Percent = 80;
            federalResults1.ALP_Votes = 20;
            federalResults1.LP_Votes = 80;
            federalResults1.HeldSince = 1996; //2 elections

            List<String> stateSeats1 = new List<string>();
            stateSeats1.Add("state1");

            StateElectorateData stateResults1 = new StateElectorateData();
            stateResults1.StateElectorateName = "state1";
            stateResults1.TPP_WinnerParty = "LNP";

            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));
            mockISocialReader.ExpectAndReturn("GetFederalResults", federalResults1, "fed1");
            mockISocialReader.ExpectAndReturn("GetStateSeats", stateSeats1, "fed1");
            mockISocialReader.ExpectAndReturn("GetStateResults", stateResults1, "state1");

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;
            /*
            float currentMargin1 = 60;
            float previouslyWonFactor1 = 5/100;
            float stateImpactFactor = 2/100;
            float safety = currentMargin1 * previouslyWonFactor1 * stateImpactFactor;*/
            String safetyDescription = ConvertSafetyPercentageToCategory(50); //TODO: gotta be pretty good anyway
            
            Region region1 = new Region();
            region1.RegionName = "fed1";
            GeoModel model = new GeoModel();
            model.Regions = new Region[]{region1};

            logic.CalculateSeatSafety(model, false, new ProgressWrapper(new ProgressBar()));
            Assert.AreEqual(safetyDescription, region1.GetDataValue(0));
        }

        // Calculate which party won the seat using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre:model is not null and isMainDisplay is not null;
        // Post: Seat winner data has been added to the model according to whether 
        // isMainDisplay is true or false.
        //void CalculateSeatWinners(GeoModel model, Boolean isMainDisplay);        
        private void SetupCalculateWinnersLogic()
        {
            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));

            FederalElectorateData results1 = new FederalElectorateData();
            results1.FederalElectorateName = "electorate1";
            results1.FirstPref_ALP_Percent = 55;
            results1.FirstPref_DEM_Percent = 5;
            results1.FirstPref_GRN_Percent = 20;
            results1.FirstPref_LP_Percent = 10;
            results1.FirstPref_NP_Percent = 0;
            results1.ALP_Votes = 500;
            results1.LP_Votes = 50;
            mockISocialReader.ExpectAndReturn("GetFederalResults", results1, "electorate1");

            FederalElectorateData results2 = new FederalElectorateData();
            results2.FederalElectorateName = "electorate2";
            results2.FirstPref_ALP_Percent = 5;
            results2.FirstPref_DEM_Percent = 5;
            results2.FirstPref_GRN_Percent = 70;
            results2.FirstPref_LP_Percent = 20;
            results2.FirstPref_NP_Percent = 0;
            results2.ALP_Votes = 100;
            results2.LP_Votes = 1000;
            mockISocialReader.ExpectAndReturn("GetFederalResults", results2, "electorate2");

            FederalElectorateData results3 = new FederalElectorateData();
            results3.FederalElectorateName = "electorate3";
            results3.FirstPref_ALP_Percent = 20;
            results3.FirstPref_DEM_Percent = 20;
            results3.FirstPref_GRN_Percent = 20;
            results3.FirstPref_LP_Percent = 20;
            results3.FirstPref_NP_Percent = 20;
            results3.ALP_Votes = 100;
            results3.LP_Votes = 1000;
            mockISocialReader.ExpectAndReturn("GetFederalResults", results3, "electorate3");

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;  
        }

        [Test]
        public void TestCalculateSeatWinnersNotMainDisplayStyle()
        {
            SetupCalculateWinnersLogic();

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            geoModel.Regions = new Region[] { region1, region2 };

            Style defaultStyle = new Style();

            logic.CalculateSeatWinners(geoModel, false, new ProgressWrapper(new ProgressBar()));
            Assert.AreEqual(1, geoModel.Styles.Length);
            Assert.AreEqual(defaultStyle.StyleName, geoModel.Regions[0].RegionStyle.StyleName);
            Assert.AreEqual(defaultStyle.ColorCode, geoModel.Regions[0].RegionStyle.ColorCode);
            Assert.AreEqual(defaultStyle.StyleName, geoModel.Regions[1].RegionStyle.StyleName);
            Assert.AreEqual(defaultStyle.ColorCode, geoModel.Regions[1].RegionStyle.ColorCode);
        }

        [Test]
        public void TestCalculateSeatWinnersNotMainDisplayDataNames()
        {
            SetupCalculateWinnersLogic();   

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            geoModel.Regions = new Region[] { region1 };
            logic.CalculateSeatWinners(geoModel, false, new ProgressWrapper(new ProgressBar()));

            Assert.AreEqual(1, region1.DataNames.Count);
            Assert.AreEqual("Winning Party : ", region1.DataNames[0]);
        }


        [Test]
        public void TestCalculateSeatWinnersNotMainDisplayWinner()
        {
            SetupCalculateWinnersLogic();

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            Region region3 = new Region();
            region3.RegionName = "electorate3";
            geoModel.Regions = new Region[] { region1, region2, region3 };
            logic.CalculateSeatWinners(geoModel, false, new ProgressWrapper(new ProgressBar()));

            Assert.AreEqual("ALP", region1.GetDataValue(0));
            Assert.AreEqual("GRN", region2.GetDataValue(0));
            Assert.AreEqual("LNP", region3.GetDataValue(0));
        }

        [Test]
        public void TestCalculateSeatWinnersIsMainDisplayStyle()
        {
            SetupCalculateWinnersLogic();

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            geoModel.Regions = new Region[] { region1, region2 };

            logic.CalculateSeatWinners(geoModel, true, new ProgressWrapper(new ProgressBar()));
            Assert.AreEqual(2, geoModel.Styles.Length);
            Assert.IsNotNull(geoModel.Regions[0].RegionStyle);
            Assert.IsNotNull(geoModel.Regions[1].RegionStyle);
        }

        [Test]
        public void TestCalculateSeatWinnersIsMainDisplayDataNames()
        {
            SetupCalculateWinnersLogic();

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            geoModel.Regions = new Region[] { region1 };

            logic.CalculateSeatWinners(geoModel, true, new ProgressWrapper(new ProgressBar()));
            Assert.AreEqual(1, region1.DataNames.Count);
            Assert.AreEqual("Winning Party : ", region1.DataNames[0]);
        }

        [Test]
        public void TestCalculateSeatWinnersIsMainDisplayWinner()
        {
            SetupCalculateWinnersLogic();

            GeoModel geoModel = new GeoModel();
            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            Region region3 = new Region();
            region3.RegionName = "electorate3";
            geoModel.Regions = new Region[] { region1, region2, region3 };

            logic.CalculateSeatWinners(geoModel, true, new ProgressWrapper(new ProgressBar()));
            Assert.AreEqual("ALP", region1.GetDataValue(0));
            Assert.AreEqual("GRN", region2.GetDataValue(0));
            Assert.AreEqual("LNP", region3.GetDataValue(0));
        }

        // Check if the regions in the GeoModel can be found in database. This prevent the 
        // user from trying to add sociological data to files that the database does not have 
        // data for.
        // Returns true if there is data in the database for this geographical data set, otherwise // returns false.
        // Pre: model is not null
        // Post: True has been returned if sociological data for this data set can be found, else // false has been returned.
        //Boolean CanMatchSociologicalData(GeoModel model);   
        [Test]
        public void TestCanMatchSociologicalDataTrueExact()
        {
            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));

            List<String> fedElectNames = new List<string>();
            fedElectNames.Add("electorate1");
            fedElectNames.Add("electorate2");
            mockISocialReader.ExpectAndReturn("GetFederalElectorateNames", fedElectNames);

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;

            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            GeoModel model = new GeoModel();
            model.Regions = new Region[] { region1, region2 };

            Assert.IsTrue(logic.CanMatchSociologicalData(model));
        }

        [Test]
        public void TestCanMatchSociologicalDataTrueExtra()
        {
            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));

            List<String> fedElectNames = new List<string>();
            fedElectNames.Add("electorate1");
            fedElectNames.Add("electorate2");
            fedElectNames.Add("electorate3");
            fedElectNames.Add("electorate4");
            mockISocialReader.ExpectAndReturn("GetFederalElectorateNames", fedElectNames);

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;

            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            GeoModel model = new GeoModel();
            model.Regions = new Region[] { region1, region2 };

            Assert.IsTrue(logic.CanMatchSociologicalData(model));
        }

        [Test]
        public void TestCanMatchSociologicalDataFalse()
        {
            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));

            List<String> fedElectNames = new List<string>();
            fedElectNames.Add("electorate1");
            fedElectNames.Add("electorate2");
            mockISocialReader.ExpectAndReturn("GetFederalElectorateNames", fedElectNames);

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;

            Region region1 = new Region();
            region1.RegionName = "electorate1";
            Region region2 = new Region();
            region2.RegionName = "electorate2";
            Region region3 = new Region();
            region3.RegionName = "electorate3";
            GeoModel model = new GeoModel();
            model.Regions = new Region[] { region1, region2, region3 };

            Assert.IsFalse(logic.CanMatchSociologicalData(model));
        }


        //void SetFederalDistricts(GeoModel model);
        [Test]
        public void TestSetFederalDistricts()
        {
            Region region1 = new Region();
            region1.RegionName = "Reg1";
            Region region2 = new Region();
            region2.RegionName = "Reg2";
            Region region3 = new Region();
            region3.RegionName = "Reg3";
            Region region4 = new Region();
            region4.RegionName = "Reg4";
            Region region5 = new Region();
            region5.RegionName = "Reg5";
            Region[] regions = new Region[] { region1, region2, region3, region4, region5 };
            GeoModel model = new GeoModel();
            model.Regions = regions;
            
            DynamicMock mockISocialReader = new DynamicMock(typeof(ISocialReader));

            List<String> regionNames1 = new List<string>();
            regionNames1.Add(region1.RegionName);
            regionNames1.Add(region5.RegionName);

            List<String> regionNames2 = new List<string>();
            regionNames2.Add(region2.RegionName);
            regionNames2.Add(region3.RegionName);
            regionNames2.Add(region4.RegionName);

            IDistrict district1 = new District();
            district1.DistrictName = "district1";
            district1.RegionNames = regionNames1;

            IDistrict district2 = new District();
            district2.DistrictName = "district2";
            district2.RegionNames = regionNames2;

            List<IDistrict> districts = new List<IDistrict>();
            districts.Add(district1);
            districts.Add(district2);

            mockISocialReader.ExpectAndReturn("GetFederalElectorateDistricts", districts);

            logic = new SocialLogic();
            logic.Reader = (ISocialReader)mockISocialReader.MockInstance;
            logic.SetFederalDistricts(model);

            Assert.AreEqual(district1.DistrictName, region1.RegionCategory.CategoryName);
            Assert.AreEqual(district2.DistrictName, region2.RegionCategory.CategoryName);
            Assert.AreEqual(district2.DistrictName, region3.RegionCategory.CategoryName);
            Assert.AreEqual(district2.DistrictName, region4.RegionCategory.CategoryName);
            Assert.AreEqual(district1.DistrictName, region5.RegionCategory.CategoryName);
        }
    }
}
