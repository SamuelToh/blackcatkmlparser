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
    public class IntegrationTestSocialLogic
    {
        //TODO: some Districts are inaccurate in tests, default is currently Brisbane
        private ILog log;

        private GeoModel model;
        private IGeoReader geoReader;
        private SocialLogic socialLogic;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        [SetUp]
        public void SetUp()
        {
            model = new GeoModel();
            geoReader = new MapInfoReader(@"..\..\Data\QLD_Federal_Electoral_Boundaries.MID", @"..\..\Data\QLD_Federal_Electoral_Boundaries.mif");
            socialLogic = new SocialLogic();
            model.BuildGeoModel(geoReader, new ProgressWrapper(new ProgressBar()));
        }

        [Test]
        public void TestSetFederalDistrictsBlair()
        {  
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Blair", regions));
            Assert.AreEqual("Western Qld", GetRegionByName("Blair", regions).RegionCategory.CategoryName);
        }

        [Test]
        public void TestSetFederalDistrictsBonner()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Bonner", regions));
            Assert.AreEqual("Brisbane", GetRegionByName("Bonner", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsBowman()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Bowman", regions));
            Assert.AreEqual("Brisbane", GetRegionByName("Bowman", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsBrisbane()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Brisbane", regions));

            Assert.AreEqual("Brisbane", GetRegionByName("Brisbane", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsCapricornia()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Capricornia", regions));

            Assert.AreEqual("Central Qld", GetRegionByName("Capricornia", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsDawson()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Dawson", regions));

            Assert.AreEqual("Northern Qld", GetRegionByName("Dawson", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsDickson()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Dickson", regions));

            Assert.AreEqual("Brisbane", GetRegionByName("Dickson", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsFadden()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Fadden", regions));

            Assert.AreEqual("Brisbane", GetRegionByName("Fadden", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsFairfax()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Fairfax", regions));

            Assert.AreEqual("Sunshine Coast", GetRegionByName("Fairfax", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsFisher()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Fisher", regions));

            Assert.AreEqual("Sunshine Coast", GetRegionByName("Fisher", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsForde()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Forde", regions));

            Assert.AreEqual("SouthEast Qld", GetRegionByName("Forde", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsGriffith()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Griffith", regions));

            Assert.AreEqual("Brisbane", GetRegionByName("Griffith", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsGroom()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Groom", regions));

            Assert.AreEqual("SouthEast Qld", GetRegionByName("Groom", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsHerbert()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Herbert", regions));

            Assert.AreEqual("Northern Qld", GetRegionByName("Herbert", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsHinkler()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Hinkler", regions));

            Assert.AreEqual("Central Qld", GetRegionByName("Hinkler", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsKennedy()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Kennedy", regions));

            Assert.AreEqual("Western Qld", GetRegionByName("Kennedy", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsLeichhardt()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Leichhardt", regions));

            Assert.AreEqual("Northern Qld", GetRegionByName("Leichhardt", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsLilley()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Lilley", regions));

            Assert.AreEqual("Brisbane", GetRegionByName("Lilley", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsLongman()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Longman", regions));

            Assert.AreEqual("Sunshine Coast", GetRegionByName("Longman", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsMaranoa()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Maranoa", regions));

            Assert.AreEqual("Western Qld", GetRegionByName("Maranoa", regions).RegionCategory.CategoryName);
        }

        [Test]
        public void TestSetFederalDistrictsMcPherson()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("McPherson", regions));

            Assert.AreEqual("Gold Coast", GetRegionByName("McPherson", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsMoncrieff()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Moncrieff", regions));
            //TODO: define district
            Assert.AreEqual("Gold Coast", GetRegionByName("Moncrieff", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsMoreton()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Moreton", regions));
            //TODO: define district
            Assert.AreEqual("Brisbane", GetRegionByName("Moreton", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsOxley()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Oxley", regions));
            //TODO: define district
            Assert.AreEqual("Brisbane", GetRegionByName("Oxley", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsPetrie()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Petrie", regions));
            //TODO: define district
            Assert.AreEqual("Brisbane", GetRegionByName("Petrie", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsRankin()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Rankin", regions));
            //TODO: define district
            Assert.AreEqual("Brisbane", GetRegionByName("Rankin", regions).RegionCategory.CategoryName);
        }


        [Test]
        public void TestSetFederalDistrictsRyan()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Ryan", regions));
            //TODO: define district
            Assert.AreEqual("Brisbane", GetRegionByName("Ryan", regions).RegionCategory.CategoryName);
        }

        [Test]
        public void TestSetFederalDistrictsWideBay()
        {
            socialLogic.SetFederalDistricts(model);
            Region[] regions = model.Regions;

            Assert.NotNull(GetRegionByName("Wide Bay", regions));
            //TODO: define district
            Assert.AreEqual("Sunshine Coast", GetRegionByName("Wide Bay", regions).RegionCategory.CategoryName);
        }

        private Region GetRegionByName(String name, Region[] regions)
        {
            foreach (Region r in regions)
            {
                if (r.RegionName.ToUpper() == name.ToUpper())
                    return r;
            }
            return null;
        }
    }
}
