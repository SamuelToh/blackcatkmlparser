using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;

namespace TestController
{
    [TestFixture] 
    public class TestKMLParser
    {
        private Controller controller;

        [Test]
        public void mockTestExample()
        {
            //Create a test version of some data fields
            List<String> testDataFieldNames = new List<string>();
            testDataFieldNames.Add("field1");
            testDataFieldNames.Add("field2");

            //Create a mock version of KMLDataModel
            DynamicMock mockKmlModel = new DynamicMock(typeof(IKMLDataModel));
            //Tell the mock object that when the getDataFieldNames() method is called to return our test field list
            mockKmlModel.ExpectAndReturn("getDataFieldNames", testDataFieldNames);

            //Construct a controller with the mock kml model
            controller = new Controller((IKMLDataModel)mockKmlModel.MockInstance);

            //Test a controller method
            List<String> dataFields = controller.getKMLDataFields();
            Assert.AreEqual("field1", dataFields[0]);
            Assert.AreEqual("field2", dataFields[1]);
        }
    }
}
