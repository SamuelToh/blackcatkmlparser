using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackCat;
using NUnit.Framework;
using NUnit.Mocks;
using log4net.Config;

namespace TestDataManipulator
{
    [TestFixture] 
    public class TestDataMerger
    {
        private IDataMerger dataMerger;

        [TestFixtureSetUp]
        public void fixtureSetUp()
        {
            BasicConfigurator.Configure();
        }

        [SetUp]
        public void setUp()
        {
            dataMerger = new DataMerger();
        }

        // Tests whether kml model and sociological model can link by the selected columns.
        // The models are considered able to link if every value in the GeoModel column has a matching value
        // in the SocialModel column.
        //
        // Pre: kmlModel and socialTbl are not null and kmlColumnName and socColumnName are not empty strings
        // Post: Returns true if column names are matched, otherwise returns false. 
        // Return: true if kml model and sociological model can link. Otherwise, returns false.
        // bool canLink(IGeoModel geoM, String geoColumnName, ISocialModel socialM, String socColumnName);
        [Test]
        public void testCanLinkMatching()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers",new string[]{"id1","id2","id3"});
            mockGeoModel.Expect("SetRegionStyle");
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("id1");
            socioIds.Add("id2");
            socioIds.Add("id3");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsTrue(result);
        }

        [Test]
        public void testCanLinkMatchingExceptCase()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "iD1", "id2", "Id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("Id1");
            socioIds.Add("id2");
            socioIds.Add("ID3");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsTrue(result);
        }

        [Test]
        public void testCanLinkMatchingOutOfOrder()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "id2", "id1", "id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("id1");
            socioIds.Add("id2");
            socioIds.Add("id3");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsTrue(result);
        }

        [Test]
        public void testCanLinkMatchingExtraSocioEntries()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "id2", "id1", "id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("id1");
            socioIds.Add("id2");
            socioIds.Add("id3");
            socioIds.Add("id4");
            socioIds.Add("id5");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsTrue(result);
        }
        
        [Test]
        public void testCanLinkMatchingExtraWhiteSpace()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "id2 ", " id1", "id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("id1");
            socioIds.Add("id2\t");
            socioIds.Add("id3");
            socioIds.Add("id4");
            socioIds.Add(" id5");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsTrue(result);
        }

        [Test]
        public void testCanLinkNonMatching()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "frank", " wheels ", "id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            socioIds.Add("id1");
            socioIds.Add("hyperactive");
            socioIds.Add("loopy");
            socioIds.Add("giraffe");
            socioIds.Add(" id5");
            mockSocioModel.ExpectAndReturn("getSocioColumnData", socioIds, "socioCol");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            bool result = dataMerger.canLink(geoModel, "geoCol", socioModel, "socioCol");
            Assert.IsFalse(result);
        }

        // Links a Geographical Model with the data in a SocialModel using the columns the user has indicated should be used. 
        // pre: geoM and socialM are not null. geoColName and socialColName are not empty string.
        // post: Returns an integer denoting success(0) or failure (1).
        // int linkDataModels(IGeoModel geoM, ISocialModel socialM, string geoColName, string socialColName);
        [Test]
        public void testDoesLinkMatching()
        {
            DynamicMock mockGeoModel = new DynamicMock(typeof(IGeoModel));
            mockGeoModel.ExpectAndReturn("GetRegionIdentifiers", new string[] { "id1", "id2", "id3" });
            IGeoModel geoModel = (IGeoModel)mockGeoModel.MockInstance;

            DynamicMock mockSocioModel = new DynamicMock(typeof(ISocialModel));
            List<String> socioIds = new List<string>(3);
            mockSocioModel.ExpectAndReturn("getSeatWinner", "ALP", "id1");
            mockSocioModel.ExpectAndReturn("getSeatWinner", "LP", "id2");
            mockSocioModel.ExpectAndReturn("getSeatWinner", "OTHER", "id3");
            ISocialModel socioModel = (ISocialModel)mockSocioModel.MockInstance;

            Assert.NotNull(dataMerger);
            int result = dataMerger.linkDataModels(geoModel, "geoCol", socioModel, "socioCol");
            Assert.AreEqual(0, result);
        }
    }
}
