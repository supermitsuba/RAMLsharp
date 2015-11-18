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

        // GET api/RAML
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
            var data = r.WebApiToRamlModel(new Uri("http://www.google.com"), "Test API", "1", "application/json", "This is a test")
                        .ToString();


            result.Content = new StringContent(data);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Raml+yaml");
            result.Content.Headers.ContentLength = data.Length;
            return result;
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
    }
}
