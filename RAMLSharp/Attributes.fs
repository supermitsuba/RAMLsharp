namespace RAMLSharp.Attributes

open System
open System.Net
open Newtonsoft.Json.Schema

/// <summary>
/// This is used to describe headers that your Web API may be looking for.
/// </summary>
[<AttributeUsage(AttributeTargets.Method, AllowMultiple=true)>]
type RequestHeadersAttribute (name: string option, description: string option, ``type``: Type option, isRequired: bool option, example: string option, schema: string option, responseType: Type option, minimum: int option, maximum: int option) =
    inherit System.Attribute()
    let mutable name = name
    let mutable description = description
    let mutable ``type`` = ``type``
    let mutable isRequired = isRequired
    let mutable example = example
    let mutable minimum = minimum
    let mutable maximum = maximum
    let mutable responseType = responseType
    let mutable schema = schema

    new() = RequestHeadersAttribute(None, None, None, None, None, None, None, None, None)

    /// <summary>
    /// This is the name of the header.  Ex: Accept
    /// </summary>
    member x.Name 
        with get() = match name with | Some i -> i | None -> ""
        and set value = name <- Some value         
    /// <summary>
    /// This is the description of your header and why you need it.
    /// </summary>
    member x.Description 
        with get() = match description with | Some i -> i | None -> ""
        and set value = description <- Some value  
    /// <summary>
    /// This is the .net type of the header value.  This is automatically converted later to RAML type.
    /// </summary>
    member x.Type 
        with get() = match ``type`` with | Some i -> i | None -> null
        and set value = ``type`` <- Some value  
    /// <summary>
    /// Is this header required to execute the API.
    /// </summary>
    member x.IsRequired
        with get() = match isRequired with | Some i -> i | None -> false
        and set value = isRequired <- Some value   
    /// <summary>
    /// This is an example of what the values are in the API.
    /// </summary>
    member x.Example
        with get() = match example with | Some i -> i | None -> ""
        and set value = example <- Some value   
    
    /// <summary>
    /// This is the schema of the payload returned.
    /// </summary>
    member x.Schema 
        with get() = match schema with | Some i -> i | None -> ""
        and set value = schema <- Some value  

    /// <summary>
    /// This is an Type of the payload.
    /// </summary>
    member x.ResponseType 
        with get() = match responseType with | Some i -> i | None -> null
        and set value = let j = new JsonSchemaGenerator() 
                        x.Schema <- j.Generate(value).ToString()
                        responseType <- Some value  

    /// <summary>
    /// This is for number or integer fields.  It specifies an acceptable minimum number.
    /// </summary>
    member x.Minimum
        with get() = match minimum with | Some i -> i | None -> 0
        and set value = minimum <- Some value 
    /// <summary>
    /// This is for number or integer fields.  It specifies an acceptable maximum number.
    /// </summary>
    member x.Maximum 
        with get() = match maximum with | Some i -> i | None -> Int32.MaxValue
        and set value = maximum <- Some value 

/// <summary>
/// This attribute is used to tell RAMLSharp what responses from the API should look like, per status code.
/// </summary>
[<AttributeUsage(AttributeTargets.Method, AllowMultiple = true)>]
type ResponseBodyAttribute(statusCode: HttpStatusCode option, contentType: string option, example: string option, description: string option, schema: string option) =
    inherit System.Attribute()

    let mutable statusCode = statusCode
    let mutable contentType = contentType
    let mutable example = example
    let mutable description = description
    let mutable schema = schema

    new () = ResponseBodyAttribute(None, None, None, None, None)

    /// <summary>
    /// This is the status code of the response.  Ex: 200, 400, 500, etc.
    /// </summary>
    member x.StatusCode
        with get() = match statusCode with | Some i -> i | None -> HttpStatusCode.OK
        and set value = statusCode <- Some value 
    /// <summary>
    /// This is the content type returned from the response.  Ex:  application/json, etc.
    /// </summary>
    member x.ContentType
        with get() = match contentType with | Some i -> i | None -> ""
        and set value = contentType <- Some value 
    /// <summary>
    /// This is an example of how the payload would look like.
    /// </summary>
    member x.Example
        with get() = match example with | Some i -> i | None -> ""
        and set value = example <- Some value 
    /// <summary>
    /// This is a description of what the response would be.  Say if you receive a 404, what does it mean in your scenario.
    /// </summary>
    member x.Description
        with get() = match description with | Some i -> i | None -> ""
        and set value = description <- Some value 
    /// <summary>
    /// This is the schema of the payload returned.
    /// </summary>
    member x.Schema
        with get() = match schema with | Some i -> i | None -> ""
        and set value = schema <- Some value 