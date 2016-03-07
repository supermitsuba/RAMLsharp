using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RamlSharp.Convention.Controllers
{
    public class SampleController : ApiController
    {
		[HttpGet()]
		public string Get()
		{
			return "Sample";
		}
    }
}
