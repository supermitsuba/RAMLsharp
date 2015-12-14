using Moq;
using RAMLSharp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using NUnit.Framework;

namespace RAMLSharp.Test
{
	[TestFixture]
    [ExcludeFromCodeCoverage]
    public class RamlMapperTests
    {
        RAMLModel expectedModel = null;
        Mock<IHttpRoute> mockRoute = null;

        [SetUp]
        public void TestInitialize()
        {
            var routes = new List<RouteModel>();
            expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", routes);
            
            mockRoute = new Mock<IHttpRoute>();
            mockRoute.Setup(p => p.RouteTemplate).Returns("api/test");
        }

        #region check for fields in base information
        [Test]
        public void RAMLSharp_CreateRamlDocumentWithVersion_GenerateRAMLWithVersion()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("version:"));
        }

        [Test]
        public void RAMLSharp_CreateRamlDocumentWithBaseMediaType_GenerateRAMLWithMediaType()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("mediaType:"));
        }

        [Test]
        public void RAMLSharp_CreateRamlDocumentWithDescription_GenerateRAMLWithDescription()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("content:"));
        }

        [Test]
        public void RAMLSharp_CreateRamlDocumentWithTitle_GenerateRAMLWithTitle()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("title:"));
        }

        [Test]
        public void RAMLSharp_CreateRamlDocumentWithUri_GenerateRAMLWithBaseURI()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("baseUri:"));
        }

        [Test]
        public void RAMLSharp_CreateRamlDocumentWithProtocol_GenerateRAMLWithProtocol()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("protocols: [HTTP]"));
        }
        #endregion

        #region ApiDescription Tests
        [Test]
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
