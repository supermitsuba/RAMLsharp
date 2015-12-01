using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;

namespace RAMLSharp.Sample.RamlSharp.Controllers
{
    /// <summary>
    /// The controller that will handle requests for raml sharp page.
	///
	/// To read in all of the XML documentation, you will need to do these 2 steps:
	/// 1) Add this line to WebApiConfig file, Register function
	///   a)  config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));
	/// 2) Go to the properties page of your web api project, goto build tab, and click on XML documentation
	///   a)  MAKE SURE THE PATH MATCHES FROM STEP 1 and 2
	///
	/// Also make sure to change the settings below to match your API's settings.
    /// </summary>
    public class HelpController : ApiController
    {
        /// <summary>
        /// Gets Raml
        /// </summary>
        /// <returns>Returns a string of RAML</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/RAML"), HttpGet]
        public HttpResponseMessage Raml()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            
            var r = new RAMLMapper(this);
			// Please change the line below.
            var data = r.WebApiToRamlModel(new Uri("http://www.google.com"), "Test API", "1", "application/json", "This is a test")
                        .ToString();


            result.Content = new StringContent(data);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Raml+yaml");
            result.Content.Headers.ContentLength = data.Length;
            return result;
        }
    }
}