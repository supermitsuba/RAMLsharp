using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RAMLSharp.Attributes;
using RAMLSharp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Diagnostics.CodeAnalysis;

namespace RAMLSharp.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RamlMapperResponseBodyTests
    {
        RAMLModel expectedModel = null;
        IEnumerable<ApiDescription> descriptions = null;
        Mock<HttpActionDescriptor> mockActionDescriptor = null;
        List<ResponseModel> expectedResponseBody = null;
        Collection<ResponseBodyDocumentationAttribute> expectedResponseBodyAttributes = null;

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
                        Responses= null
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

            expectedResponseBody = new List<ResponseModel>
            {
                new ResponseModel {
                      ContentType="application/json",
                      Description="This is a json response.",
                      Example="{ 'value':'Hello World' }",
                      StatusCode= System.Net.HttpStatusCode.OK
                },
                new ResponseModel {
                      ContentType="application/xml",
                      Description="This is an error response in xml.",
                      Example="<error><message>opps</message></error>",
                      StatusCode= System.Net.HttpStatusCode.BadRequest
                }
            };

            expectedResponseBodyAttributes = new Collection<ResponseBodyDocumentationAttribute>(
                    expectedResponseBody.Select(
                    p => new ResponseBodyDocumentationAttribute
                    {
                        ContentType = p.ContentType,
                        Description = p.Description,
                        Example = p.Example,
                        StatusCode = p.StatusCode
                    }
                ).ToList());
        }

        [TestMethod]
        public void RamlMapper_CreateOneResponseBody_GenerateRAML()
        {
            expectedModel.Routes[0].Responses = new List<ResponseModel>
            {
                expectedResponseBody[0]
            };
            var responseBodyAttribute = new Collection<ResponseBodyDocumentationAttribute>
            {
                expectedResponseBodyAttributes[0]
            };
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(responseBodyAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
        }

        [TestMethod]
        public void RamlMapper_CreateTwoResponseBody_GenerateRAML()
        {
            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
            Assert.IsTrue(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RamlMapper_CreateNullResponseBody_GenerateRAML()
        {
            expectedResponseBodyAttributes = null;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RamlMapper_CreateEmptyResponseBody_GenerateRAML()
        {
            expectedResponseBody = new List<ResponseModel>();
            expectedResponseBodyAttributes = new Collection<ResponseBodyDocumentationAttribute>();

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [TestMethod]
        public void RamlMapper_CreateNullContentType_GenerateRAMLWithoutResponse()
        {
            expectedResponseBody[0].ContentType = null;
            expectedResponseBodyAttributes[0].ContentType = null;
            expectedResponseBody[1].ContentType = null;
            expectedResponseBodyAttributes[1].ContentType = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
        }

        [TestMethod]
        public void RamlMapper_CreateNullExample_GenerateRAMLWithoutExample()
        {
            expectedResponseBody[0].Example = null;
            expectedResponseBodyAttributes[0].Example = null;
            expectedResponseBody[1].Example = null;
            expectedResponseBodyAttributes[1].Example = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("            example:"));
        }
        
        [TestMethod]
        public void RamlMapper_CreateNullDescription_GenerateRAMLWithoutDescription()
        {
            expectedResponseBody[0].Example = null;
            expectedResponseBodyAttributes[0].Example = null;
            expectedResponseBody[1].Example = null;
            expectedResponseBodyAttributes[1].Example = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyDocumentationAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("            description:"));
        }
    }
}
