RAMLSharp
=========

This is a tool for ASP.net Web API that will allow you to build RAML documentation.

**Newly added, RAMLSharp is now in F#**

##Purpose
The purpose of the tool is to allow a developer to code RAML documentation, using Attributes and Asp.net Web API Help Pages.  This allows you to create RAML documentation using help pages behind the scenes.  The workflow would be:

1.  Add attributes to each route
2.  Add some XML documentation
3.  Make an endpoint
4.  Configure XML documentation
5.  Generate RAML
6.  Enjoy RAML

##Attributes
Here is a list of all the attributes so far:

1.  RequestHeaders - This is for the description of Headers on the Request.
3.  ResponseBody - This is the response body that the request may look like.
4.  All other information is filled in by XML Documentation.

If there is a feature that is missing, check out the How to Contribute section.

##Setup

1)  First, add the nuget package from https://www.nuget.org/packages/RamlSharp/:

```
PM> Install-Package RamlSharp
```

2)  To get XML Documentation, you need to add this line to your WebApiConfig.cs:

```csharp
public static void Register(HttpConfiguration config)
{
    // Web API configuration and services

    config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));
}
```

3)  Enable XML documentation under the project properties of the Web API project.  Click on the build tab, and make sure the path matches to step 2.

4)  Add the RAML endpoint to host your RAML.  You can visit your http://{Your API BaseUrl}/api/RAML

```csharp
// GET api/RAML
/// <summary>
/// Gets Raml
/// </summary>
/// <returns>Returns a string of RAML</returns>
[Route("api/RAML"), HttpGet]
public HttpResponseMessage Raml()
{
  var result = new HttpResponseMessage(HttpStatusCode.OK);
  var r = new RAMLMapper(this);
  var data = r.WebApiToRamlModel(new Uri("http://www.google.com"), "Test API", "1", "application/json", "This is a test")
              .ToString();
  result.Content = new StringContent(data);
  result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Raml+yaml");
  result.Content.Headers.ContentLength = data.Length;
  return result;
}
```

5)  Lastly, add some XML documentation to your API calls:

```csharp
/// <summary>
/// This inserts a test
/// </summary>
/// <param name="testCase">The test</param>
[RequestHeaders(Name = "Accept", 
    Example = "application/json", 
    IsRequired = true,
    Type = typeof(string), 
    Description = "This is the content type we want from the server"
)]
[ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description="This is the standard request back.")]
[ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
[ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
[Route("api/tests"), HttpPost]
public void Post([FromBody]string testCase)
{
    //Inserts a test file
}
```
(You can check out more examples from the RAMLSharp.Sample project in the solution.)

This will allow you to take the RAML generated and view your API in something like API Designer (https://github.com/mulesoft/api-designer):

![](https://raw.githubusercontent.com/QuickenLoans/RAMLsharp/master/images/sampleRAML.png)


##How to Contribute

If you would like to contribute, please feel free to do a pull request.

Special thanks for contributing:

Jorden Lowe

Dominick Aleardi

Viacheslav Barashkov
