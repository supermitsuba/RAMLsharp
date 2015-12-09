using System;
using System.Linq;
using Moq;
using RAMLSharp.Attributes;
using RAMLSharp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Routing;
using NUnit.Framework;

namespace RAMLSharp.Test
{
	[TestFixture]
    [ExcludeFromCodeCoverage]
    public class RamlMapperResponseBodyTests
    {
        RAMLModel expectedModel = null;
        IEnumerable<ApiDescription> descriptions = null;
        Mock<HttpActionDescriptor> mockActionDescriptor = null;
        List<ResponseModel> expectedResponseBody = null;
        Collection<ResponseBodyAttribute> expectedResponseBodyAttributes = null;
        Mock<IHttpRoute> mockRoute = null;

        [SetUp]
        public void TestInitialize()
        {
            mockActionDescriptor = new Mock<HttpActionDescriptor>();
            mockRoute = new Mock<IHttpRoute>();
            mockRoute.Setup(p => p.RouteTemplate).Returns("api/test");
            var routes = new List<RouteModel>
            { 
                new RouteModel("api/test", "get", null, null, null, null, null, null, null)
            };
            expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", routes);

            descriptions = new List<ApiDescription>()
            {
                new ApiDescription
                {
                     HttpMethod = new System.Net.Http.HttpMethod("get"),
                     RelativePath = "api/test",
                     ActionDescriptor = mockActionDescriptor.Object,
                     Route = mockRoute.Object
                }
            };

            expectedResponseBody = new List<ResponseModel>
            {
                new ResponseModel(System.Net.HttpStatusCode.OK, "application/json", "{ 'value':'Hello World' }", "This is a json response.", null),
                new ResponseModel(System.Net.HttpStatusCode.BadRequest, "application/xml", "<error><message>opps</message></error>", "This is an error response in xml.", null)
            };

            expectedResponseBodyAttributes = new Collection<ResponseBodyAttribute>(
                    expectedResponseBody.Select(
                    p => new ResponseBodyAttribute
                    {
                        ContentType = p.ContentType,
                        Description = p.Description,
                        Example = p.Example,
                        StatusCode = p.StatusCode
                    }
                ).ToList());
        }

        [Test]
        public void RamlMapper_CreateOneResponseBody_GenerateRAML()
        {
            expectedModel.Routes[0].Responses = new List<ResponseModel>
            {
                expectedResponseBody[0]
            };
            var responseBodyAttribute = new Collection<ResponseBodyAttribute>
            {
                expectedResponseBodyAttributes[0]
            };
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(responseBodyAttribute);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
        }

        [Test]
        public void RamlMapper_CreateTwoResponseBody_GenerateRAML()
        {
            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
            Assert.IsTrue(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [Test]
        public void RamlMapper_CreateNullResponseBody_GenerateRAML()
        {
            expectedResponseBodyAttributes = null;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [Test]
        public void RamlMapper_CreateEmptyResponseBody_GenerateRAML()
        {
            expectedResponseBody = new List<ResponseModel>();
            expectedResponseBodyAttributes = new Collection<ResponseBodyAttribute>();

            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
        }

        [Test]
        public void RamlMapper_CreateNullContentType_GenerateRAMLWithoutResponse()
        {
            expectedResponseBody[0].ContentType = null;
            expectedResponseBodyAttributes[0].ContentType = null;
            expectedResponseBody[1].ContentType = null;
            expectedResponseBodyAttributes[1].ContentType = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsFalse(result.ToString().Contains("    body:"));
            Assert.IsFalse(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("          application/xml:"));
        }

        [Test]
        public void RamlMapper_CreateNullExample_GenerateRAMLWithoutExample()
        {
            expectedResponseBody[0].Example = null;
            expectedResponseBodyAttributes[0].Example = null;
            expectedResponseBody[1].Example = null;
            expectedResponseBodyAttributes[1].Example = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
                                .Returns(expectedResponseBodyAttributes);

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
            Assert.IsTrue(result.ToString().Contains("    body:"));
            Assert.IsTrue(result.ToString().Contains("          application/json:"));
            Assert.IsFalse(result.ToString().Contains("            example:"));
        }
        
        [Test]
        public void RamlMapper_CreateNullDescription_GenerateRAMLWithoutDescription()
        {
            expectedResponseBody[0].Example = null;
            expectedResponseBodyAttributes[0].Example = null;
            expectedResponseBody[1].Example = null;
            expectedResponseBodyAttributes[1].Example = null;

            expectedModel.Routes[0].Responses = expectedResponseBody;
            mockActionDescriptor.Setup(p => p.GetCustomAttributes<ResponseBodyAttribute>())
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
