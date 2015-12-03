namespace RAMLSharp

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Web.Http
open System.Web.Http.Description

type RAMLMapper () =
    new (controller : ApiController) = 
        RAMLMapper(controller.Configuration.Services.GetApiExplorer().ApiDescriptions);

    new(description : IEnumerable<ApiDescription>) = 
        RAMLMapper(description)
        
        
