namespace RAMLSharp.Models

open System
open System.Collections.Generic
open System.Text
open System.Linq
open System.Net

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
    override this.ToString() = ""
