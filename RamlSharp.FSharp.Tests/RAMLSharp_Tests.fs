namespace RamlSharp.FSharp.Tests
open System
open System.Web
open System.Collections.Generic
open NUnit.Framework
open RAMLSharp
open RAMLSharp.Models
open System.Web.Http.Description
open System.Web.Http.Routing

[<TestFixture>]
type Test() = 

    [<SetUp>]
    member x.Init() =
        ()

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullAPIs_DisplayBasicRAMLDocument() =
        let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", new List<RouteModel>())
        let list:IEnumerable<ApiDescription> = null

        let subject = new RAMLMapper(list)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNoAPIs_DisplayBasicRAMLDocument() = 
        let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", "test", null)
        let list:IEnumerable<ApiDescription> = null
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", "test")

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullUri_DisplayBasicRAMLDocumentWithoutURI() = 
        let expectedModel = new RAMLModel("test", null, "1", "application/json", "test", new List<RouteModel>())
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(expectedModel.BaseUri, "test", "1", "application/json", "test")

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullTitle_DisplayBasicRAMLDocumentWithBlankTitle() = 
        let expectedModel = new RAMLModel(null, new Uri("http://www.test.com"), "1", "application/json", "test", new List<RouteModel>())
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), expectedModel.Title, "1", "application/json", "test")
        let r = result.ToString()

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
        Assert.IsTrue(result.ToString().Contains("title:"))

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullDescription_DisplayBasicRAMLDocumentWithoutDescription () =
        let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", "application/json", null, new List<RouteModel>())
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", "application/json", expectedModel.Description)

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
        Assert.IsFalse(result.ToString().Contains("content:"))

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullVersion_DisplayBasicRAMLDocumentWithoutVersion() =
        let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), null, "application/json", "test", new List<RouteModel>())
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", expectedModel.Version, "application/json", "test")

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
        Assert.IsFalse(result.ToString().Contains("version:"))

    [<Test>]
    member x.RAMLSharp_CreateRamlDocumentWithNullBaseMediaType_DisplayBasicRAMLDocumentWithoutMediaType() = 
        let expectedModel = new RAMLModel("test", new Uri("http://www.test.com"), "1", null, "test", new List<RouteModel>())
        let model = new List<ApiDescription>()

        let subject = new RAMLMapper(model)
        let result = subject.WebApiToRamlModel(new Uri("http://www.test.com"), "test", "1", expectedModel.DefaultMediaType, "test")

        Assert.AreEqual(expectedModel.ToString(), result.ToString(), "The RAML string must be the same.")
        Assert.IsFalse(result.ToString().Contains("mediaType:"))