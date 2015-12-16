module RAMLSharpTest.Headers

open System
open System.Web
open System.Net.Http
open System.Collections.Generic
open System.Collections.ObjectModel
open RAMLSharp
open RAMLSharp.Attributes
open RAMLSharp.Models
open System.Web.Http.Controllers
open System.Web.Http.Description
open System.Web.Http.Routing
open System.Diagnostics.CodeAnalysis
open Moq

open NUnit.Framework

[<ExcludeFromCodeCoverage>]
let init () =
    let routes = new List<RouteModel>()
    routes.Add(new RouteModel("api/test", "get", null, null, null, null, null, null, null))
    
    let mockRoute = new Mock<IHttpRoute>()
    mockRoute.Setup(fun p -> p.RouteTemplate).Returns("api/test") |> ignore
    let mockActionDescriptor = new Mock<HttpActionDescriptor>()
    let apiDescription =  new ApiDescription()
    apiDescription.HttpMethod <- new HttpMethod("get")
    apiDescription.ActionDescriptor <- mockActionDescriptor.Object
    apiDescription.Route <- mockRoute.Object

    let descriptions = new List<ApiDescription>()
    let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", routes)
    let expectedHeader = new List<RequestHeaderModel>()
    let expectedHeaderAttributes = new List<RequestHeadersAttribute>()
    let addHeaderAttribute (x:RequestHeaderModel) =
        let value = new RequestHeadersAttribute()
        value.Name <- x.Name
        value.Description <- x.Description
        value.Type <- x.Type
        value.IsRequired <- x.IsRequired
        value.Example <- x.Example
        expectedHeaderAttributes.Add(value)
        ()

    expectedHeader.Add(new RequestHeaderModel("Accept", "Used to tell the server what format it will accept.", typeof<string>, false, "application/json", 0, 0))
    expectedHeader.Add(new RequestHeaderModel("Date", "Used to tell the server when the request was made.", typeof<DateTime>, false, "application/json", 0, 0))
    expectedHeader
    |> Seq.iter addHeaderAttribute

    descriptions.Add(apiDescription)
    (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel)

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Create one API with 1 header should display 1 header`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Headers <- new List<RequestHeaderModel>()
    expectedModel.Routes.[0].Headers.Add(expectedHeader.[0])
    let headerAttribute = new Collection<RequestHeadersAttribute>()
    headerAttribute.Add(expectedHeaderAttributes.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
    Assert.IsTrue(result.ToString().Contains("headers:"));
    Assert.IsTrue(result.ToString().Contains("Accept:"));

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``One API with a null Header does not display Header section`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Headers <- null
    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns<Collection<RequestHeadersAttribute>>(null) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.");
    Assert.IsFalse(result.ToString().Contains("headers:"));

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Create RAML with 2 Headers`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Headers <- expectedHeader
    
    let headerAttribute = new Collection<RequestHeadersAttribute>(expectedHeaderAttributes)

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("headers:"))
    Assert.IsTrue(result.ToString().Contains("Accept:"))
    Assert.IsTrue(result.ToString().Contains("Date:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``If header name is null, do not display header section`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedHeader.[0].Name <- null
    expectedHeaderAttributes.[0].Name <- null

    expectedModel.Routes.[0].Headers <- new List<RequestHeaderModel>()
    expectedModel.Routes.[0].Headers.Add(expectedHeader.[0])

    let headerAttribute = new Collection<RequestHeadersAttribute>()
    headerAttribute.Add(expectedHeaderAttributes.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsFalse(result.ToString().Contains("headers:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``If 2 header names are null, do not display header section`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedHeader.[0].Name <- null
    expectedHeader.[1].Name <- null
    expectedModel.Routes.[0].Headers <- expectedHeader

    expectedHeaderAttributes.[0].Name <- null
    expectedHeaderAttributes.[1].Name <- null
            
    let headerAttribute = new Collection<RequestHeadersAttribute>(expectedHeaderAttributes)
    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsFalse(result.ToString().Contains("headers:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display description field for a header if it is null`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedHeader.[0].Description <- null
    expectedHeaderAttributes.[0].Description <- null

    expectedModel.Routes.[0].Headers <- new List<RequestHeaderModel>()
    expectedModel.Routes.[0].Headers.Add(expectedHeader.[0])
    let headerAttribute = new Collection<RequestHeadersAttribute>()
    headerAttribute.Add(expectedHeaderAttributes.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("headers:"))
    Assert.IsFalse(result.ToString().Contains("        description:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display example field for a header if it is null`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedHeader.[0].Example <- null
    expectedHeaderAttributes.[0].Example <- null

    expectedModel.Routes.[0].Headers <- new List<RequestHeaderModel>()
    expectedModel.Routes.[0].Headers.Add(expectedHeader.[0])
    let headerAttribute = new Collection<RequestHeadersAttribute>()
    headerAttribute.Add(expectedHeaderAttributes.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("headers:"))
    Assert.IsFalse(result.ToString().Contains("        example:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display type field for a header if it is null`` () =
    let (expectedHeader, expectedHeaderAttributes, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedHeader.[0].Type <- null
    expectedHeaderAttributes.[0].Type <- null

    expectedModel.Routes.[0].Headers <- new List<RequestHeaderModel>()
    expectedModel.Routes.[0].Headers.Add(expectedHeader.[0])
    let headerAttribute = new Collection<RequestHeadersAttribute>()
    headerAttribute.Add(expectedHeaderAttributes.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<RequestHeadersAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("headers:"))
    Assert.IsFalse(result.ToString().Contains("        type:"))