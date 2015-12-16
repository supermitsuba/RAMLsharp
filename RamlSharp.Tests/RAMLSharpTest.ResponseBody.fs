module RAMLSharpTest.ResponseBody

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
    let mockActionDescriptor = new Mock<HttpActionDescriptor>()
    let mockRoute = new Mock<IHttpRoute>()
    mockRoute.Setup(fun p -> p.RouteTemplate).Returns("api/test") |> ignore
    let routes = new List<RouteModel>()
    routes.Add(new RouteModel("api/test", "get", null, null, null, null, null, null, null))
    let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", routes)
    let apiDescription =  new ApiDescription()
    apiDescription.HttpMethod <- new HttpMethod("get")
    apiDescription.ActionDescriptor <- mockActionDescriptor.Object
    apiDescription.Route <- mockRoute.Object
    let descriptions = new List<ApiDescription>()
    descriptions.Add(apiDescription)
    let expectedResponseBody = new List<ResponseModel>()
    expectedResponseBody.Add(new ResponseModel(System.Net.HttpStatusCode.OK, "application/json", "{ 'value':'Hello World' }", "This is a json response.", null))
    expectedResponseBody.Add(new ResponseModel(System.Net.HttpStatusCode.BadRequest, "application/xml", "<error><message>opps</message></error>", "This is an error response in xml.", null))
    
    let expectedResponseBodyAttribute = new List<ResponseBodyAttribute>()
    let addResponseBodyAttribute (x:ResponseModel) =
        let value = new ResponseBodyAttribute()
        value.ContentType <- x.ContentType
        value.Description <- x.Description
        value.StatusCode <- x.StatusCode
        value.Example <- x.Example
        expectedResponseBodyAttribute.Add(value)
        ()
    expectedResponseBody
    |> Seq.iter addResponseBodyAttribute

    (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel)

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``RAMLMapper should create 1 response body`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Responses <- new List<ResponseModel>()
    expectedModel.Routes.[0].Responses.Add(expectedResponseBody.[0])
    let headerAttribute = new Collection<ResponseBodyAttribute>()
    headerAttribute.Add(expectedResponseBodyAttribute.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test");

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("    body:"))
    Assert.IsTrue(result.ToString().Contains("          application/json:"))


[<Test>]
[<ExcludeFromCodeCoverage>]
let ``One API with a null Response Body does not display response body section`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Responses <- null
    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns<Collection<ResponseBodyAttribute>>(null) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.IsFalse(result.ToString().Contains("    body:"))
    Assert.IsFalse(result.ToString().Contains("          application/json:"))
    Assert.IsFalse(result.ToString().Contains("          application/xml:"))
    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``One API with an empty Response Body does not display response body section`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Responses <- new List<ResponseModel>()
    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(new Collection<ResponseBodyAttribute>()) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")
    
    Assert.IsFalse(result.ToString().Contains("    body:"))
    Assert.IsFalse(result.ToString().Contains("          application/json:"))
    Assert.IsFalse(result.ToString().Contains("          application/xml:"))
    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Create RAML with 2 Response Body`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedModel.Routes.[0].Responses <- expectedResponseBody
    
    let headerAttribute = new Collection<ResponseBodyAttribute>(expectedResponseBodyAttribute)

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(headerAttribute) |> ignore

    let subject = new RAMLMapper(descriptions)
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.IsTrue(result.ToString().Contains("    body:"))
    Assert.IsTrue(result.ToString().Contains("          application/json:"))
    Assert.IsTrue(result.ToString().Contains("          application/xml:"))
    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display description field for a Response Body if it is null`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedResponseBody.[0].Description <- null
    expectedResponseBodyAttribute.[0].Description <- null

    expectedModel.Routes.[0].Responses <- new List<ResponseModel>()
    expectedModel.Routes.[0].Responses.Add(expectedResponseBody.[0])
    let responseAttribute = new Collection<ResponseBodyAttribute>()
    responseAttribute.Add(expectedResponseBodyAttribute.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(responseAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("    body:"))
    Assert.IsTrue(result.ToString().Contains("          application/json:"))
    Assert.IsFalse(result.ToString().Contains("            description:"))

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display example field for a Response Body if it is null`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedResponseBody.[0].Example <- null
    expectedResponseBodyAttribute.[0].Example <- null

    expectedModel.Routes.[0].Responses <- new List<ResponseModel>()
    expectedModel.Routes.[0].Responses.Add(expectedResponseBody.[0])
    let responseAttribute = new Collection<ResponseBodyAttribute>()
    responseAttribute.Add(expectedResponseBodyAttribute.[0])

    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(responseAttribute) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")
    
    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsTrue(result.ToString().Contains("    body:"))
    Assert.IsTrue(result.ToString().Contains("          application/json:"))
    Assert.IsFalse(result.ToString().Contains("            example:"))
    
[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Do not display response body if Content Type is null`` () =
    let (expectedResponseBody, expectedResponseBodyAttribute, mockActionDescriptor, descriptions, expectedModel) = init() 
    expectedResponseBody.[0].ContentType <- null
    expectedResponseBodyAttribute.[0].ContentType <- null
    expectedResponseBody.[1].ContentType <- null
    expectedResponseBodyAttribute.[1].ContentType <- null

    let responseCollection = new Collection<ResponseBodyAttribute>(expectedResponseBodyAttribute)
    expectedModel.Routes.[0].Responses <- expectedResponseBody
    mockActionDescriptor.Setup(fun p -> p.GetCustomAttributes<ResponseBodyAttribute>())
                        .Returns(responseCollection) |> ignore

    let subject = new RAMLMapper(descriptions);
    let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

    Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
    Assert.IsFalse(result.ToString().Contains("    body:"))
    Assert.IsFalse(result.ToString().Contains("          application/json:"))
    Assert.IsFalse(result.ToString().Contains("          application/xml:"))