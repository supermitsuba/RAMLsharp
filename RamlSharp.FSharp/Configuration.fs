namespace RAMLSharp.Configuration

open System
open System.Reflection
open System.Runtime.CompilerServices
open System.Diagnostics.CodeAnalysis
open System.Globalization
open System.Linq
open System.Reflection
open System.Web.Http
open System.Web.Http.Controllers
open System.Web.Http.Description
open System.Xml.XPath

type IModelDocumentationProvider=
    abstract member GetDocumentation: MemberInfo -> string 
    abstract member GetDocumentation: Type -> string

[<ExcludeFromCodeCoverage()>]
[<Extension()>] 
type HttpConfigurationExtension =
    //let ApiModelPrefix = "MS_HelpPageApiModel_"

    /// <summary>
    /// Sets the documentation provider for help page.
    /// </summary>
    /// <param name="config">The <see cref="HttpConfiguration"/>.</param>
    /// <param name="documentationProvider">The documentation provider.</param>
    [<Extension()>]
    static member SetDocumentationProvider(config:HttpConfiguration , documentationProvider:IDocumentationProvider ) =
        config.Services.Replace(typeof<IDocumentationProvider>, documentationProvider)
        ()

/// <summary>
/// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
/// </summary>
[<ExcludeFromCodeCoverage()>] // This was auto generated code from Web API Help Pages
type XmlDocumentationProvider =
    new(documentPath:string) = 
        new XmlDocumentationProvider(documentPath)

    interface IDocumentationProvider with
        member this.GetDocumentation(controllerDescriptor:HttpControllerDescriptor) =
            ""

        member this.GetDocumentation(actionDescriptor:HttpActionDescriptor) =
            ""

        member this.GetDocumentation (parameterDescriptor:HttpParameterDescriptor) = 
            ""

        member this.GetResponseDocumentation (actionDescriptor:HttpActionDescriptor) = 
            ""

    interface IModelDocumentationProvider with
        member this.GetDocumentation ( ```member`` :MemberInfo) =
            ""

        member this.GetDocumentation ( ```type`` :Type) =
            ""