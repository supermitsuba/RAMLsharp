using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;
using RAMLSharp.Attributes;
using RAMLSharp.Sample.Models;

namespace RAMLSharp.Sample.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// This gets the tests
        /// </summary>
        /// <example>Example of get</example>
        /// <returns>Returns a list of values!</returns>
        [RequestHeaders(Name = "Accept", Example = "application/json", IsRequired = true, Type = typeof(string), Description = "This is the content type we want from the server")]
        [ResponseBody(StatusCode=HttpStatusCode.OK, ContentType = "application/json", Example = "['value1', 'value2']", Schema = "values")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [Route("api/tests"), HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }
        
        /// <summary>
        /// This inserts a test
        /// </summary>
        /// <param name="testCase">The test</param>
        [RequestHeaders(Name = "Accept", 
            Example = "application/json", 
            IsRequired = true,
            Minimum = -1,
            Maximum = -2,
            Type = typeof(int), 
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

        /// <summary>
        /// updates the test
        /// </summary>
        /// <param name="id">id of the test</param>
        /// <param name="testCase">The test</param>
        [Route("api/tests/{id}"), HttpPut]
        public void Put(int id, [FromBody]string testCase)
        {
            //Update a test
        }

        /// <summary>
        /// Delete a test
        /// </summary>
        /// <param name="id">With this id</param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/tests/{id}"), HttpDelete]
        public void Delete(int id)
        {
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetSomething([FromUri]BunchOfFields searchRequest)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                 Content=new StringContent("doing something")
            };
        }

        [Route("list/{id}")]
        [HttpGet]
        public HttpResponseMessage GetSomething1([FromUri]BunchOfFields searchRequest, [FromUri] string id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("doing something")
            };
        }

        [Route("list/{id}")]
        [HttpPost]
        public HttpResponseMessage GetSomething2([FromUri]BunchOfFields searchRequest, [FromUri] string id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("doing something")
            };
        }

        [Route("list2")]
        [HttpGet]
        public HttpResponseMessage GetSomething([FromUri]long? searchRequest)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("doing something")
            };
        }
    }
}
