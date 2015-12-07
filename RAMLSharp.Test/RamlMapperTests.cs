using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RAMLSharp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace RAMLSharp.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RamlMapperTests
    {
        RAMLModel expectedModel = null;
        Mock<IHttpRoute> mockRoute = null;

        [TestInitialize]
        public void TestInitialize()
        {
            var routes = new List<RouteModel>();
            expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", routes);
            
            mockRoute = new Mock<IHttpRoute>();
            mockRoute.Setup(p => p.RouteTemplate).Returns("api/test");
        }

        #region null checks for base information
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullAPIs_DisplayBasicRAMLDocument()
        {
            IEnumerable<ApiDescription> modelApiDescription = null;

            var subject = new RAMLMapper(modelApiDescription);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNoAPIs_DisplayBasicRAMLDocument()
        {
            expectedModel.Routes = null;
            var modelApiDescription = new List<ApiDescription>();

            var subject = new RAMLMapper(modelApiDescription);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullUri_DisplayBasicRAMLDocumentWithoutURI()
        {
            expectedModel.BaseUri = null;
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(expectedModel.BaseUri, "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullTitle_DisplayBasicRAMLDocumentWithBlankTitle()
        {
            expectedModel.Title = null;
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), expectedModel.Title, "1", "application/json", "test");
            var r = result.ToString();
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("title:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullDescription_DisplayBasicRAMLDocumentWithoutDescription()
        {
            expectedModel.Description = null;
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", expectedModel.Description);

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("content:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullVersion_DisplayBasicRAMLDocumentWithoutVersion()
        {
            expectedModel.Version = null;
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", expectedModel.Version, "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("version:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullBaseMediaType_DisplayBasicRAMLDocumentWithoutMediaType()
        {
            expectedModel.DefaultMediaType = null;
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", expectedModel.DefaultMediaType, "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("mediaType:"));
        }

        #endregion

        #region check for fields in base information
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithVersion_GenerateRAMLWithVersion()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("version:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithBaseMediaType_GenerateRAMLWithMediaType()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("mediaType:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithDescription_GenerateRAMLWithDescription()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("content:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithTitle_GenerateRAMLWithTitle()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("title:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithUri_GenerateRAMLWithBaseURI()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("baseUri:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithProtocol_GenerateRAMLWithProtocol()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("protocols: [HTTP]"));
        }
        #endregion

        #region ApiDescription Tests
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithOneApi_GenerateRAMLWithOneAPI()
        {
            expectedModel.Routes = new List<RouteModel>
            { 
                new RouteModel("api/test", "get", null, null, null, null, null, null, null)
            };
            
            IEnumerable<ApiDescription> model = new List<ApiDescription>()
            {
                new ApiDescription
                {
                     HttpMethod = new System.Net.Http.HttpMethod("get"),
                     RelativePath = "api/test",
                     Route = mockRoute.Object
                }
            };

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }
        #endregion
    }
}
