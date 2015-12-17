namespace RAMLSharp.Models

open System
open System.Collections.Generic
open System.Text
open System.Linq
open System.Net
open RAMLSharp.Extensions
open ModelHelpers

/// <summary>
/// This model represents what a request body may look like.
/// </summary>
type RequestBodyParameterModel (name, description, ``type``, isRequired, example) = 

    /// <summary>
    /// The name of a body you can submit to the API
    /// </summary>
    member val Name : string = name   with get, set
    /// <summary>
    /// The description of what the request body payload does.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// The data type of the payload.  This is later converted to RAML types.
    /// </summary>
    member val Type : Type = ``type``  with get, set
    /// <summary>
    /// Is a body required would be true, otherwise false.
    /// </summary>
    member val IsRequired : bool = isRequired with get, set  
    /// <summary>
    /// An example of the request body payload.
    /// </summary>
    member val Example : string = example with get, set

/// <summary>
/// This model represents the headers from the RequestHeadersAttribute.
/// </summary>
type RequestHeaderModel (name, description, ``type``, isRequired, example, minimum, maximum) = 

    /// <summary>
    /// The name of the header. Ex. Accept
    /// </summary>
    member val Name : string = name   with get, set
    /// <summary>
    /// The description of what the header does for you.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// The .net type that the header value is.  This is later converted to a RAML type.
    /// </summary>
    member val Type : Type = ``type``  with get, set
    /// <summary>
    /// If the header is required, then it is true, otherwise false.
    /// </summary>
    member val IsRequired : bool = isRequired with get, set  
    /// <summary>
    /// An example of the header values.
    /// </summary>
    member val Example : string = example with get, set
    /// <summary>
    /// The minimum value of the header if it is of type number or integer
    /// </summary>
    member val Minimum : int = minimum with get, set
    /// <summary>
    /// The maximum value of the header if it is of type number or integer
    /// </summary>
    member val Maximum : int = maximum with get, set

/// <summary>
/// This represents the query parameters for a given resource.
/// </summary>
type RequestQueryParameterModel (name, description, ``type``, isRequired, example) = 

    /// <summary>
    /// The name of the query parameter.
    /// </summary>
    member val Name : string = name   with get, set
    /// <summary>
    /// The description of what the parameter does for you.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// The .net type that the query string parameter.  This is later converted into a RAML type.
    /// </summary>
    member val Type : Type = ``type``  with get, set
    /// <summary>
    /// If the query parameter is required then this will be true, else false.
    /// </summary>
    member val IsRequired : bool = isRequired with get, set
    /// <summary>
    /// The example values that could be used for the query string parameter.
    /// </summary>
    member val Example : string = example with get, set

/// <summary>
/// This model represents a request uri parameter.
/// </summary>
type RequestUriParameterModel (name, description, ``type``, isRequired, example) = 

    /// <summary>
    /// The name of the URI parameter.
    /// </summary>
    member val Name : string = name   with get, set
    /// <summary>
    /// The description of what the URI parameter.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// The .net type of the URI parameter.  This is later converted to RAML type.
    /// </summary>
    member val Type : Type = ``type``  with get, set
    /// <summary>
    /// If the URI parameter is required, true, otherwise false.
    /// </summary>
    member val IsRequired : bool = isRequired with get, set
    /// <summary>
    /// Example of values for the URI parameter.
    /// </summary>
    member val Example : string = example with get, set

/// <summary>
/// This model is a representation of a response.
/// </summary>
type ResponseModel (statusCode, contentType, example, description, schema) =

    /// <summary>
    /// The status code of a response.  Ex:  200, 300, 400, 500, etc.
    /// </summary>
    member val  StatusCode : HttpStatusCode = statusCode  with get, set
    /// <summary>
    /// The content type of a response.  Ex:  application/json
    /// </summary>
    member val ContentType : string = contentType with get, set
    /// <summary>
    /// The example payload of a response.
    /// </summary>
    member val Example : string = example with get, set
    /// <summary>
    /// The description of what is contained in a response.
    /// </summary>
    member val Description : string = description with get, set
    /// <summary>
    /// The schema of the payload for the response.
    /// </summary>
    member val Schema : string = schema with get, set

/// <summary>
/// This model represents a given URL resource.
/// </summary>
type RouteModel(urlTemplate, verb, description, headers, queryParameters, uriParameters, bodyParameters, responses, requestContentType) =

    /// <summary>
    /// This is the template string for a uri. Ex:  /api/test/{testId}
    /// </summary>
    member val UrlTemplate : string = urlTemplate with get, set

    /// <summary>
    /// This is the Http Verb of the resource.  Ex:  GET, PUT, POST, DELETE, HEAD, etc.
    /// </summary>
    member val Verb : string = verb  with get, set

    /// <summary>
    /// The description of what that resource does.
    /// </summary>
    member val Description : string = description  with get, set

    /// <summary>
    /// List of all the headers on the given resource.
    /// </summary>
    member val Headers : IList<RequestHeaderModel> = headers  with get, set

    /// <summary>
    /// List of all query string parameters on the resource.
    /// </summary>
    member val QueryParameters : IList<RequestQueryParameterModel> = queryParameters  with get, set

    /// <summary>
    /// List of all Uri parameters on the resource.
    /// </summary>
    member val UriParameters : IList<RequestUriParameterModel> = uriParameters  with get, set

    /// <summary>
    /// List of all the body parameters on a given resource.
    /// </summary>
    member val BodyParameters : IList<RequestBodyParameterModel> = bodyParameters  with get, set

    /// <summary>
    /// List of all possible responses for a given resource.
    /// </summary>
    member val Responses : IList<ResponseModel> = responses  with get, set
     
    /// <summary>
    /// The content type of a requested resource.  Ex:  application/json
    /// </summary>
    member val RequestContentType : string = requestContentType  with get, set
    
/// <summary>
/// This is the main object that is parsed from the API descriptors in the ASP.net Web API Help pages API.  We use this object to convert it into RAML.
/// </summary>
type RAMLModel(title, baseUrl, version, defaultMediaType, description, routes) = 
    /// <summary>
    /// The title of the API.
    /// </summary>
    member val Title : string = title with get, set

    /// <summary>
    /// The base URL of the API.
    /// </summary>
    member val BaseUri : Uri = baseUrl with get, set

    /// <summary>
    /// The Version of the API
    /// </summary>
    member val Version : string = version with get, set

    /// <summary>
    /// The default media types used to submit a request and response.
    /// </summary>
    member val DefaultMediaType : string = defaultMediaType with get, set

    /// <summary>
    /// The description of your API.
    /// </summary>
    member val Description : string = description with get, set
        
    /// <summary>
    /// A list of routes, or in an API's case, a list of resources in the API. 
    /// </summary>
    member val Routes : IList<RouteModel> = routes with get, set
    
    /// <summary>
    /// This is used to output RAML from the RAMLModel.
    /// </summary>
    /// <returns>Returns a raml string of the model.</returns>
    override this.ToString() = 
        let hasVisitedRoutes = new Dictionary<string, bool>()

        let SetRamlRoot (sb:StringBuilder) = 

            let UriScheme = match this.BaseUri with | null -> "HTTP" | _ -> this.BaseUri.Scheme.ToUpper()
            sb.Append("#%RAML 0.8" + Environment.NewLine) 
              .Append("---" + Environment.NewLine)
              .AppendFormat("title: {0}{1}", this.Title , Environment.NewLine)
              .AppendFormat("baseUri: {0}{1}", this.BaseUri , Environment.NewLine)
              .AppendFormat("protocols: [{0}]{1}", UriScheme , Environment.NewLine) |> ignore
            
            match this.Version with 
            | null -> () 
            | _ -> sb.AppendFormat("version: {0}{1}", this.Version , Environment.NewLine) |> ignore

            match this.DefaultMediaType with 
            | null -> () 
            | _ -> sb.AppendFormat("mediaType: {0}{1}", this.DefaultMediaType , Environment.NewLine) |> ignore

            match this.Description with 
            | null -> sb 
            | _ -> sb |> createDocumentation(this.Description)
                   
        let setHttpVerb (sb:StringBuilder, route:RouteModel) = 
            sb.AppendFormat("  {0}:{1}", route.Verb, Environment.NewLine) |> ignore

        let setDescription (sb:StringBuilder, route:RouteModel) = 
            sb.AppendFormat("    description: {0}{1}", route.Description, Environment.NewLine) |> ignore

        let setRequest (sb:StringBuilder, route:RouteModel) = 
            match route.BodyParameters = null || route.BodyParameters.Count = 0 with
            | true -> ()
            |false -> sb |> createBody(route.RequestContentType)
                      route.BodyParameters
                      |> Seq.iter (fun x -> sb |> createBodyParameters (x.Name, x.Description, x.Type.ToRamlType(),  x.IsRequired.ToString().ToLower(), x.Example))

        let setHeaders (sb:StringBuilder, route:RouteModel) = 
            match route.Headers = null || route.Headers.Count <= 0 || route.Headers.All(fun p -> String.IsNullOrEmpty(p.Name)) with
            |  true -> ()
            | false -> sb.AppendFormat("    headers:{0}", Environment.NewLine) |> ignore
                       route.Headers
                       |> Seq.iter (fun x-> sb |> createHeaders(x.Name, x.Example, x.Type.ToRamlType(), x.Minimum, x.Maximum, x.Description))

        let setParameters (sb:StringBuilder, route:RouteModel) = 
            match route.QueryParameters = null || route.QueryParameters.Count = 0 with
            | true  -> ()
            | false -> sb.AppendFormat("    queryParameters: {0}", Environment.NewLine) |> ignore
                       route.QueryParameters
                       |> Seq.iter( fun x-> sb |> createQueryParameters(x.Name, x.Type.ToRamlType(), x.IsRequired, x.Description, x.Example) )

        let setResponses (sb:StringBuilder, route:RouteModel) =
            match route.Responses = null || 
                route.Responses.Count <= 0 ||
                route.Responses.All(fun p -> String.IsNullOrEmpty(p.ContentType)) with
            | true  -> ()
            | false -> sb.AppendFormat("    responses:{0}", Environment.NewLine) |> ignore
                       route.Responses
                       |> Seq.sortBy(fun x -> x.StatusCode)
                       |> Seq.iter (fun x -> sb |> createResponse((int)x.StatusCode, x.Description, x.ContentType, x.Schema, x.Example))

        let setUriParameters (sb:StringBuilder, route:RouteModel) = 
            match route.UriParameters = null || route.UriParameters.Count <= 0 with 
            | true -> ()
            |false -> sb.AppendFormat("  uriParameters: {0}", Environment.NewLine) |> ignore
                      route.UriParameters
                      |> Seq.iter( fun parameters -> 
                                        sb.AppendFormat("      {0}: {1}", parameters.Name, Environment.NewLine)
                                          .AppendFormat("        type: {0}{1}", parameters.Type.ToRamlType(), Environment.NewLine)
                                          .AppendFormat("        required: {0}{1}", parameters.IsRequired.ToString().ToLower(), Environment.NewLine)
                                          .AppendFormat("        description: {0}{1}", parameters.Description, Environment.NewLine)
                                          .AppendFormat("        example: |{1}            {0}{1}", parameters.Example, Environment.NewLine) |> ignore)

        let setRoutes (sb:StringBuilder, route:RouteModel) =
            match not(hasVisitedRoutes.ContainsKey(route.UrlTemplate)) || not(hasVisitedRoutes.[route.UrlTemplate]) with
            | true  -> setUriParameters(sb, route)
                       hasVisitedRoutes.Add(route.UrlTemplate, true)
                       ()
            | false -> ()

            setHttpVerb(sb, route)
            setDescription(sb, route)
            setRequest(sb, route)
            setHeaders(sb, route)
            setParameters(sb, route)
            setResponses(sb, route)
            ()

        let setResources (resource, verb, sb:StringBuilder) = 
            sb.Append("/")
              .Append(resource.ToString()) 
              .AppendLine(":") |> ignore
            hasVisitedRoutes.Clear()
            verb
            |> Seq.iter (fun x -> setRoutes (sb, x) )
            ()
        

        let SetRamlBody (sb:StringBuilder) = 
            match this.Routes = null || this.Routes.Count = 0 with
            | true  -> sb
            | false -> (this.Routes
                        |> Seq.sortBy( fun p -> p.UrlTemplate )
                        |> Seq.groupBy( fun p -> p.UrlTemplate)
                        |> Seq.iter( fun (key, item) ->  setResources(key, item, sb))

                        sb)

        let sb = new StringBuilder()
        (sb
         |> SetRamlRoot
         |> SetRamlBody).ToString()
