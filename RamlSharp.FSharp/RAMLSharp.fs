namespace RAMLSharp

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Web.Http
open System.Web.Http.Description
open RAMLSharp.Models

type RAMLMapper () =
    /// <summary>
    /// This constructor is the main constructor to pass in your controller and find out about your Web API.
    /// </summary>
    /// <param name="controller">The controller that is hosting your API.</param>
    new (controller : ApiController) = 
        RAMLMapper(controller.Configuration.Services.GetApiExplorer().ApiDescriptions);

    /// <summary>
    /// This constructor is used to test.
    /// </summary>
    /// <param name="descriptions">A list of fake ApiDescriptions to generate a raml from.</param>
    new(description : IEnumerable<ApiDescription>) = 
        RAMLMapper(description)

    member this.WebApiToRamlModel(baseUri : Uri, title : string, version : string, defaultMediaTypes : string, description : string) =
        new RAMLModel("",new Uri(""),"","", "", new List<RouteModel>())
        
        
