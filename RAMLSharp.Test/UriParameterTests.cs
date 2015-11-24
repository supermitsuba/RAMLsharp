using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAMLSharp.Models;
using System.Collections.Generic;
using Moq;
using System.Web.Http.Description;
using System.Web.Http.Controllers;
using System.Collections.ObjectModel;
using RAMLSharp.Test.Fakes;
using System.Diagnostics.CodeAnalysis;

namespace RAMLSharp.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UriParameterTests
    {
        RAMLModel expectedModel = null;
        IEnumerable<ApiDescription> descriptions = null;
        FakeApiDescription sampleDescription = null;
        ApiParameterDescription sampleApiParameterDescription = null;
        Mock<HttpParameterDescriptor> mockHttpParameterDescriptor = null;

        [TestInitialize]
        public void TestInitialize()
        {
            mockHttpParameterDescriptor = new Mock<HttpParameterDescriptor>();

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

            sampleApiParameterDescription = new ApiParameterDescription()
            {
                Name = "Value1",
                Source = ApiParameterSource.FromUri,
                Documentation = ""
            };
        }

        [TestMethod]
        public void UriParameters_ComplexObject_ReturnsAllPublicProperties()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };

            mockHttpParameterDescriptor.Setup(p => p.IsOptional)
                                       .Returns(true);
            mockHttpParameterDescriptor.Setup(p => p.DefaultValue)
                                       .Returns(null);
            mockHttpParameterDescriptor.Setup(p => p.ParameterType)
                                       .Returns(typeof(FakeComplex));

            sampleApiParameterDescription.ParameterDescriptor = mockHttpParameterDescriptor.Object;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("      field1:"));
            Assert.IsTrue(result.ToString().Contains("      field2:"));
            Assert.IsTrue(result.ToString().Contains("      field3:"));
            Assert.IsTrue(result.ToString().Contains("        type: integer"));
            Assert.IsTrue(result.ToString().Contains("        type: string"));
            Assert.IsTrue(result.ToString().Contains("        type: date"));
        }

        [TestMethod]
        public void UriParameters_Primitive_ReturnsThatProperty()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };

            mockHttpParameterDescriptor.Setup(p => p.IsOptional)
                                       .Returns(true);
            mockHttpParameterDescriptor.Setup(p => p.DefaultValue)
                                       .Returns(null);
            mockHttpParameterDescriptor.Setup(p => p.ParameterType)
                                       .Returns(typeof(string));

            sampleApiParameterDescription.ParameterDescriptor = mockHttpParameterDescriptor.Object;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("      Value1:"));
            Assert.IsTrue(result.ToString().Contains("        type: string"));
        }

        [TestMethod]
        public void UriParameters_InheritedProperties_ReturnsThatProperty()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };

            mockHttpParameterDescriptor.Setup(p => p.IsOptional)
                                       .Returns(true);
            mockHttpParameterDescriptor.Setup(p => p.DefaultValue)
                                       .Returns(null);
            mockHttpParameterDescriptor.Setup(p => p.ParameterType)
                                       .Returns(typeof(FakeInheritedComplex));

            sampleApiParameterDescription.ParameterDescriptor = mockHttpParameterDescriptor.Object;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("      field1:"));
            Assert.IsTrue(result.ToString().Contains("      field2:"));
            Assert.IsTrue(result.ToString().Contains("      field3:"));
            Assert.IsTrue(result.ToString().Contains("      Field4:"));
            Assert.IsTrue(result.ToString().Contains("        type: integer"));
            Assert.IsTrue(result.ToString().Contains("        type: string"));
            Assert.IsTrue(result.ToString().Contains("        type: date"));
        }

        [TestMethod]
        public void UriParameters_NestedObject_ReturnsFirstLevelProperties()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };

            mockHttpParameterDescriptor.Setup(p => p.IsOptional)
                                       .Returns(true);
            mockHttpParameterDescriptor.Setup(p => p.DefaultValue)
                                       .Returns(null);
            mockHttpParameterDescriptor.Setup(p => p.ParameterType)
                                       .Returns(typeof(FakeNestedComplex));

            sampleApiParameterDescription.ParameterDescriptor = mockHttpParameterDescriptor.Object;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsTrue(result.ToString().Contains("      field5:"));
            Assert.IsTrue(!result.ToString().Contains("      field1:"));
            Assert.IsTrue(!result.ToString().Contains("      field2:"));
            Assert.IsTrue(!result.ToString().Contains("      field3:"));
            Assert.IsTrue(!result.ToString().Contains("        type: integer"));
            Assert.IsTrue(result.ToString().Contains("        type: string"));
            Assert.IsTrue(!result.ToString().Contains("        type: date"));
        }

        [TestMethod]
        public void UriParameters_ParameterDescriptor_IfPropertyIsNullThenSkip()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };
            
            sampleApiParameterDescription.ParameterDescriptor = null;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

            Assert.IsFalse(String.IsNullOrEmpty(result.ToString()));
        }


        [TestMethod]
        public void UriParameters_ParameterDescriptor_IfTypeIsNullThenSkip()
        {
            var parameterDescriptions = new Collection<ApiParameterDescription>()
            {
                sampleApiParameterDescription
            };
            sampleDescription = new FakeApiDescription(parameterDescriptions)
            {
                HttpMethod = new System.Net.Http.HttpMethod("get"),
                RelativePath = "api/test"
            };

            mockHttpParameterDescriptor.Setup(p => p.IsOptional)
                                       .Returns(true);
            mockHttpParameterDescriptor.Setup(p => p.DefaultValue)
                                       .Returns(null);
            mockHttpParameterDescriptor.Setup(p => p.ParameterType)
                                       .Returns<Type>(null);

            sampleApiParameterDescription.ParameterDescriptor = mockHttpParameterDescriptor.Object;

            descriptions = new List<ApiDescription>() { sampleDescription };

            var subject = new RAMLMapper(descriptions);
            var result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");
            
            Assert.IsFalse(String.IsNullOrEmpty(result.ToString()));
        }


    }
}
