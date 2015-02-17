using System.Collections.Generic;
using System.Web.Http;

namespace RAMLSharp.Sample.Controllers
{
    /// <summary>
    /// my derp
    /// </summary>
    public class DerpController : ApiController
    {
        // GET api/derp
        /// <summary>
        /// deeee
        /// </summary>
        /// <returns>rrrp</returns>
        [Route("api/derp"), HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/derp/5
        [Route("api/derp/{id}"), HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/derp
        public void Post([FromBody]string value)
        {
        }

        // PUT api/derp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/derp/5
        public void Delete(int id)
        {
        }
    }
}
