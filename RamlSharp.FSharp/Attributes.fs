namespace RAMLSharp.Attributes

open System
open System.Net

/// <summary>
/// This is used to describe headers that your Web API may be looking for.
/// </summary>
[<AttributeUsage(AttributeTargets.Method, AllowMultiple=true)>]
type RequestHeadersAttribute(name, description, ``type``, isRequired, example, minimum, maximum) =
    inherit System.Attribute()
    /// <summary>
    /// This is the name of the header.  Ex: Accept
    /// </summary>
    member val Name : string = name   with get, set
    /// <summary>
    /// This is the description of your header and why you need it.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// This is the .net type of the header value.  This is automatically converted later to RAML type.
    /// </summary>
    member val Type : Type = ``type``  with get, set
    /// <summary>
    /// Is this header required to execute the API.
    /// </summary>
    member val IsRequired : bool = isRequired with get, set  
    /// <summary>
    /// This is an example of what the values are in the API.
    /// </summary>
    member val Example : string = example with get, set
    /// <summary>
    /// This is for number or integer fields.  It specifies an acceptable minimum number.
    /// </summary>
    member val Minimum : int = minimum with get, set
    /// <summary>
    /// This is for number or integer fields.  It specifies an acceptable maximum number.
    /// </summary>
    member val Maximum : int = maximum with get, set

/// <summary>
/// This attribute is used to tell RAMLSharp what responses from the API should look like, per status code.
/// </summary>
[<AttributeUsage(AttributeTargets.Method, AllowMultiple = true)>]
type ResponseBodyAttribute(statusCode, contentType, example, description, schema) =
    inherit System.Attribute()

    /// <summary>
    /// This is the status code of the response.  Ex: 200, 400, 500, etc.
    /// </summary>
    member val  StatusCode : HttpStatusCode = statusCode  with get, set
    /// <summary>
    /// This is the content type returned from the response.  Ex:  application/json, etc.
    /// </summary>
    member val ContentType : string = contentType with get, set
    /// <summary>
    /// This is an example of how the payload would look like.
    /// </summary>
    member val Example : string = example with get, set
    /// <summary>
    /// This is a description of what the response would be.  Say if you receive a 404, what does it mean in your scenario.
    /// </summary>
    member val Description : string = description  with get, set
    /// <summary>
    /// This is the schema of the payload returned.
    /// </summary>
    member val Schema : string = schema with get, set