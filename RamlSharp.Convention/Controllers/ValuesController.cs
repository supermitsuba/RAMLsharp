using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RamlSharp.Convention.Controllers
{
	public class ValuesController : ApiController
    {
		
		[HttpGet()]
		public string Get()
		{
			return "hello";
		}
    }
}