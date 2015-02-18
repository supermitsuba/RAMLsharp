RAMLSharp
=========

This is a tool for Web API, in .net, to allow you to build documentation, using RAML.

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

First, serve up a route for your RAML File:
```
[ApiExplorerSettings(IgnoreApi=true)]
[Route("api/raml"), HttpGet]
public HttpResponseMessage RAML()
{
    var result = new HttpResponseMessage(HttpStatusCode.OK);
            
    var r = new RAMLMapper(this);
    var data = r.WebApiToRAMLModel(new Uri("http://www.google.com"), "Test API", "1", "application/json", "This is a test")
                .ToString();

    result.Content = new StringContent(data);
    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/raml+yaml");
    result.Content.Headers.ContentLength = data.Length;
    return result;
}
```

Next, you need to add this line to your WebApiConfig.cs:

```
public static void Register(HttpConfiguration config)
{
    // Web API configuration and services

    config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));
}
```

This line will read in the xml documentation to describe your API.  You can enable that in your project properties.  Make sure you note where you will save that xml file so you can put that into your xml documentation path.

Lastly, add some XML documentation to your API calls:

![](https://raw.githubusercontent.com/QuickenLoans/RAMLsharp/master/images/sampleController.png)

This will allow you to take the RAML generated and view your API in something like API Designer (https://github.com/mulesoft/api-designer):

![](https://raw.githubusercontent.com/QuickenLoans/RAMLsharp/master/images/sampleRAML.png)


##How to Contribute

If you would like to contribute, please feel free to do a pull request.

Special thanks for contributing:
Jorden Lowe
Dominick Aleardi
