using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;

namespace RAMLSharp.Sample.Controllers
{
    public class RamlController : ApiController
    {
		[ApiExplorerSettings(IgnoreApi=true)]
		[Route("api/raml"), HttpGet]
		public HttpResponseMessage RAML()
		{
			var result = new HttpResponseMessage(HttpStatusCode.OK);

			var r = new RAMLMapper(this);
			var data = r.WebApiToRamlModel (new Uri ("http://www.google.com"), "Test API", "1", "application/json", "This is a test");

			result.Content = new StringContent(data.ToString());
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/raml+yaml");
			result.Content.Headers.ContentLength = data.ToString().Length;
			return result;
		}
    }
}
