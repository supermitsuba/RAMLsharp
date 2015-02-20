using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RAMLSharp.Attributes;
using RAMLSharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace RAMLSharp.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RamlMapperHeaderTests
    {
        RAMLModel expectedModel = null;
        IEnumerable<ApiDescription> descriptions = null;
        Mock<HttpActionDescriptor> mockActionDescriptor = null;
        List<RequestHeaderModel> expectedHeader = null;
        Collection<RequestHeadersAttribute> expectedHeaderAttributes = null;
        
        [TestInitialize]
        public void TestInitialize()
        {
            mockActionDescriptor = new Mock<HttpActionDescriptor>();

            expectedModel = new RAMLModel
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
                        Verb="get",
                        Headers = null
                    } 
                }
            };

            descriptions = new List<ApiDescription>()
            {
                new ApiDescription
                {
                     HttpMethod = new System.Net.Http.HttpMethod("get"),
                     RelativePath = "api/test",
                     ActionDescriptor = mockActionDescriptor.Object
                }
            };

            expectedHeader = new List<RequestHeaderModel>
            {
                new RequestHeaderModel {
                    Name = "Accept",
                    Description = "Used to tell the server what format it will accept.",
                    Example = "application/json",
                    IsRequired = false,
                    Type = typeof(string)
                },
                new RequestHeaderModel {
                    Name = "Date",
                    Description = "Used to tell the server when the request was made.",
                    Example = "application/json",
                    IsRequired = false,
                    Type = typeof(DateTime)
                }
            };

            expectedHeaderAttributes = new Collection<RequestHeadersAttribute>(
                    expectedHeader.Select(
                    p => new RequestHeadersAttribute
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Example = p.Example,
                        IsRequired = p.IsRequired,
                        Type = p.Type
                    }
                ).ToList());
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithOneHeader_Display1Headers()
        {
            expectedModel.Routes[0].Headers = new List<RequestHeaderModel>
            {
                expectedHeader[0]
            };
            var headerAttribute = new Collection<RequestHeadersAttribute>
            {
                expectedHeaderAttributes[0]
            };
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("headers:"));
            Assert.IsTrue(result.ToString().Contains("Accept:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithNullHeader_DoNotDisplayHeadersSection()
        {
            expectedModel.Routes[0].Headers = null;
            Collection<RequestHeadersAttribute> headerAttributes = null;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("headers:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithMultipleHeaders_Display2Headers()
        {
            expectedModel.Routes[0].Headers = expectedHeader;

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(expectedHeaderAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("headers:"));
            Assert.IsTrue(result.ToString().Contains("Accept:"));
            Assert.IsTrue(result.ToString().Contains("Date:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithNullName_DoNotDisplayHeaderSection()
        {
            expectedHeader[0].Name = null;
            expectedHeaderAttributes[0].Name = null;

            expectedModel.Routes[0].Headers = new List<RequestHeaderModel>
            {
                expectedHeader[0]
            };
            var headerAttribute = new Collection<RequestHeadersAttribute>
            {
                expectedHeaderAttributes[0]
            };

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("headers:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithTwoNullName_DoNotDisplayHeaderSection()
        {
            expectedHeader[0].Name = null;
            expectedHeader[1].Name = null;
            expectedModel.Routes[0].Headers = expectedHeader;

            expectedHeaderAttributes[0].Name = null;
            expectedHeaderAttributes[1].Name = null;
            
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(expectedHeaderAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("headers:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithNullDescription_DoNotDisplayDescriptionField()
        {
            expectedHeader[0].Description = null;
            expectedHeaderAttributes[0].Description = null;

            expectedModel.Routes[0].Headers = new List<RequestHeaderModel>
            {
                expectedHeader[0]
            };
            var headerAttribute = new Collection<RequestHeadersAttribute>
            {
                expectedHeaderAttributes[0]
            };

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            var r = result.ToString();
            Assert.IsTrue(result.ToString().Contains("headers:"));
            Assert.IsFalse(result.ToString().Contains("        description:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithNullExample_DoNotDisplayExampleField()
        {
            expectedHeader[0].Example = null;
            expectedHeaderAttributes[0].Example = null;

            expectedModel.Routes[0].Headers = new List<RequestHeaderModel>
            {
                expectedHeader[0]
            };
            var headerAttribute = new Collection<RequestHeadersAttribute>
            {
                expectedHeaderAttributes[0]
            };

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            var r = result.ToString();
            Assert.IsTrue(result.ToString().Contains("headers:"));
            Assert.IsFalse(result.ToString().Contains("        example:"));
        }

        [TestMethod]
        public void RamlMapper_CreateOneApiWithNullType_DoNotDisplayTypeField()
        {
            expectedHeader[0].Type = null;
            expectedHeaderAttributes[0].Type = null;

            expectedModel.Routes[0].Headers = new List<RequestHeaderModel>
            {
                expectedHeader[0]
            };
            var headerAttribute = new Collection<RequestHeadersAttribute>
            {
                expectedHeaderAttributes[0]
            };

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<RequestHeadersAttribute>())
                                .Returns(headerAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            var r = result.ToString();
            Assert.IsTrue(result.ToString().Contains("headers:"));
            Assert.IsFalse(result.ToString().Contains("        type:"));
        }
    }
}
