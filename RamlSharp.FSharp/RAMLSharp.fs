namespace RAMLSharp

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Web.Http
open System.Web.Http.Description
open RAMLSharp.Models
open RAMLSharp.Attributes
open RAMLSharp.Extensions

type RAMLMapper (description : IEnumerable<ApiDescription>) =
    let mutable _apiDescriptions = description

    /// <summary>
    /// This constructor is the main constructor to pass in your controller and find out about your Web API.
    /// </summary>
    /// <param name="controller">The controller that is hosting your API.</param>
    new (controller : ApiController) = 
        RAMLMapper(controller.Configuration.Services.GetApiExplorer().ApiDescriptions);

    /// <summary>
    /// This method takes a few more pieces not described in your API and adds them to the RAML output.
    /// </summary>
    /// <param name="baseUri">The base URL of your API.</param>
    /// <param name="title">The title or name of your API.</param>
    /// <param name="version">The version of your API.</param>
    /// <param name="defaultMediaTypes">The default media types that your API supports.  Ex: application/json or application/xml</param>
    /// <param name="description">What is the purpose of your API.</param>
    /// <returns></returns>
    member this.WebApiToRamlModel(baseUri : Uri, title : string, version : string, defaultMediaTypes : string, description : string) =
        let ramlModel = new RAMLModel(title ,baseUri, version, defaultMediaTypes, description, new List<RouteModel>())

        let GetHeaders (api:ApiDescription) = 
            let headerAttributes = api.ActionDescriptor.GetCustomAttributes<RequestHeadersAttribute>()
            match headerAttributes with
            | null -> new List<RequestHeaderModel>()
            | x    -> x.Select( fun h -> 
                                    new RequestHeaderModel(h.Name, 
                                                           h.Description, 
                                                           h.Type, 
                                                           h.IsRequired, 
                                                           h.Example, 
                                                           h.Minimum, 
                                                           h.Maximum)).ToList()
        
        let GetQueryParameters(api:ApiDescription) = 

            let mapParameters (props:System.Reflection.PropertyInfo[], isOptional, documentation, example) =
                props.Select(
                    fun q -> new RequestQueryParameterModel(q.Name, documentation, q.PropertyType, isOptional, example)
                )
            
            let results = new List<RequestQueryParameterModel>()
            api.ParameterDescriptions
            |> Seq.filter( fun x -> x.Source = ApiParameterSource.FromUri )
            |> Seq.filter( fun x -> x.ParameterDescriptor <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType.IsComplexModel() )
            |> Seq.filter( fun x -> not (api.Route.RouteTemplate.Contains(String.Format("{{{0}}}", x.ParameterDescriptor.ParameterName))))
            |> Seq.map( fun x -> (x.ParameterDescriptor.ParameterType.GetProperties(), 
                                  x.ParameterDescriptor.IsOptional, 
                                  x.Documentation, 
                                  match x.ParameterDescriptor.DefaultValue with | null -> "" | _ -> x.ParameterDescriptor.DefaultValue.ToString()) )
            |> Seq.map mapParameters
            |> Seq.iter results.AddRange
            
            api.ParameterDescriptions
            |> Seq.filter( fun x -> x.Source = ApiParameterSource.FromUri )
            |> Seq.filter( fun x -> x.ParameterDescriptor <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType <> null )
            |> Seq.filter( fun x -> not (x.ParameterDescriptor.ParameterType.IsComplexModel()) )
            |> Seq.filter( fun x -> not (api.Route.RouteTemplate.Contains(String.Format("{{{0}}}", x.ParameterDescriptor.ParameterName)))) 
            |> Seq.map ( fun x -> new RequestQueryParameterModel(x.Name, x.Documentation, x.ParameterDescriptor.ParameterType.GetForRealType(), x.ParameterDescriptor.IsOptional, match x.ParameterDescriptor.DefaultValue with | null -> "" | _ -> x.ParameterDescriptor.DefaultValue.ToString()))
            |> Seq.iter results.Add

            results

        let GetUriParameters(api:ApiDescription) = 

            let mapParameters (props:System.Reflection.PropertyInfo[], isOptional, documentation, example) =
                props.Select(
                    fun q -> new RequestUriParameterModel(q.Name, documentation, q.PropertyType, isOptional, example)
                )
            
            let results = new List<RequestUriParameterModel>()
            api.ParameterDescriptions
            |> Seq.filter( fun x -> x.Source = ApiParameterSource.FromUri )
            |> Seq.filter( fun x -> x.ParameterDescriptor <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType.IsComplexModel() )
            |> Seq.filter( fun x -> api.Route.RouteTemplate.Contains(String.Format("{{{0}}}", x.ParameterDescriptor.ParameterName)))
            |> Seq.map( fun x -> (x.ParameterDescriptor.ParameterType.GetProperties(), 
                                  x.ParameterDescriptor.IsOptional, 
                                  x.Documentation, 
                                  match x.ParameterDescriptor.DefaultValue with | null -> "" | _ -> x.ParameterDescriptor.DefaultValue.ToString()) )
            |> Seq.map mapParameters
            |> Seq.iter results.AddRange
            
            api.ParameterDescriptions
            |> Seq.filter( fun x -> x.Source = ApiParameterSource.FromUri )
            |> Seq.filter( fun x -> x.ParameterDescriptor <> null )
            |> Seq.filter( fun x -> x.ParameterDescriptor.ParameterType <> null )
            |> Seq.filter( fun x -> not (x.ParameterDescriptor.ParameterType.IsComplexModel()) )
            |> Seq.filter( fun x -> api.Route.RouteTemplate.Contains(String.Format("{{{0}}}", x.ParameterDescriptor.ParameterName)))
            |> Seq.map ( fun x -> new RequestUriParameterModel(x.Name, x.Documentation, x.ParameterDescriptor.ParameterType.GetForRealType(), x.ParameterDescriptor.IsOptional, match x.ParameterDescriptor.DefaultValue with | null -> "" | _ -> x.ParameterDescriptor.DefaultValue.ToString()))
            |> Seq.iter results.Add

            results
        
        let GetBodyParameters(api:ApiDescription) = 
            api.ParameterDescriptions
                .Where(fun r -> r.Source = ApiParameterSource.FromBody)
                .Select(fun q -> new RequestBodyParameterModel(q.Name, q.Documentation, q.ParameterDescriptor.ParameterType, q.ParameterDescriptor.IsOptional, match q.ParameterDescriptor.DefaultValue with | null -> "" | _ -> q.ParameterDescriptor.DefaultValue.ToString()))
                .ToList()

        let GetResponseBodies (api:ApiDescription) = 
            let headerAttributes = api.ActionDescriptor.GetCustomAttributes<ResponseBodyAttribute>()
            match headerAttributes with
            | null -> new List<ResponseModel>()
            | x    -> x.Select( fun h -> 
                                    new ResponseModel(h.StatusCode, 
                                                      h.ContentType, 
                                                      (match (File.Exists(h.Example)) with | true -> File.ReadAllText(h.Example) | false -> h.Example), 
                                                      h.Description, 
                                                      h.Schema)).ToList()               
        let mapApi (api:ApiDescription) = 
            let requestedContentType = match api.HttpMethod.Method.ToLower() with
                                       | "put"
                                       | "post" -> "application/x-www-form-urlencoded:"
                                       | _      -> null
            
            new RouteModel(api.Route.RouteTemplate, 
                           api.HttpMethod.Method.ToLower(), 
                           api.Documentation,
                           GetHeaders(api), 
                           GetQueryParameters(api),
                           GetUriParameters(api),
                           GetBodyParameters(api),
                           GetResponseBodies(api),
                           requestedContentType)

        _apiDescriptions
        |> Seq.map mapApi
        |> Seq.iter ramlModel.Routes.Add       

        ramlModel
