using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Description;
using Moq;
using RAMLSharp.Models;
using System.Diagnostics.CodeAnalysis;

namespace RAMLSharp.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RamlMapperTests
    {
        #region null checks for base information
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullAPIs()
        {
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = "test",
                Title = "test",
                Version = "1",
                Routes = new List<RouteModel>()
            };
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNoAPIs()
        {
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = "test",
                Title = "test",
                Version = "1"
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullUri()
        {
            Uri expectedUri = null;
            var expected = new RAMLModel
            {
                BaseUri = expectedUri,
                DefaultMediaType = "application/json",
                Description = "test",
                Title = "test",
                Version = "1"
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(expectedUri, "test", "1", "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullTitle()
        {
            string expectedTitle = null;
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = "test",
                Title = expectedTitle,
                Version = "1"
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), expectedTitle, "1", "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullDescription()
        {
            string expectedDescription = null;
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = expectedDescription,
                Title = "test",
                Version = "1"
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", expectedDescription);

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("content:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullVersion()
        {
            string expectedVersion = null;
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = "test",
                Title = "test",
                Version = expectedVersion
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", expectedVersion, "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("version:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithNullBaseMediaType()
        {
            string expectedBaseMediaType = null;
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = expectedBaseMediaType,
                Description = "test",
                Title = "test",
                Version = "1"
            };
            var model = new List<ApiDescription>();

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", expectedBaseMediaType, "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("mediaType:"));
        }

        #endregion

        #region check for fields in base information
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithVersion()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("version:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithBaseMediaType()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("mediaType:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithDescription()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("content:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithTitle()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("title:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithUri()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("baseUri:"));
        }

        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithProtocol()
        {
            IEnumerable<ApiDescription> model = null;

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("protocols: [HTTP]"));
        }
        #endregion

        #region ApiDescription Tests
        [TestMethod]
        public void RAMLSharp_CreateRamlDocumentWithOneApi()
        {
            var expected = new RAMLModel
            {
                BaseUri = new Uri("http://www.test.com"),
                DefaultMediaType = "application/json",
                Description = "test",
                Title = "test",
                Version = "1",
                Routes = new List<RouteModel>
                { 
                    new RouteModel
                    { 
                        UrlTemplate="api/test", 
                        Verb="get" 
                    } 
                }
            };
            IEnumerable<ApiDescription> model = new List<ApiDescription>()
            {
                new ApiDescription
                {
                     HttpMethod = new System.Net.Http.HttpMethod("get"),
                     RelativePath = "api/test"
                }
            };

            var subject = new RAMLMapper(model);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expected.ToString(), result.ToString(), "The RAML string must be the same.");
        }
        #endregion
    }
}
