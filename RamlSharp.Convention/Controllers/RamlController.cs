using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using RAMLSharp;
using System.Net.Http.Headers;

namespace RamlSharp.Convention.Controllers
{
    public class RamlController : ApiController
    {
		[ApiExplorerSettings(IgnoreApi=true)]
		[HttpGet]
		public HttpResponseMessage Get()
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
