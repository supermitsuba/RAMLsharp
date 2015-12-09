using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Schema;

namespace RAMLSharp.Attributes
{
    /// <summary>
    /// This attribute is used to tell RAMLSharp what responses from the API should look like, per status code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ResponseBodyAttribute : Attribute
    {
        private Type _responseType;

        /// <summary>
        /// This is the status code of the response.  Ex: 200, 400, 500, etc.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// This is the content type returned from the response.  Ex:  application/json, etc.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// This is an example of how the payload would look like.
        /// </summary>
        public string Example { get; set; }
        /// <summary>
        /// This is a description of what the response would be.  Say if you receive a 404, what does it mean in your scenario.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This is the schema of the payload returned.
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// This is an Type of the payload.
        /// </summary>
        public Type ResponseType
        {
          get { return _responseType; }
          set
          {
            var j = new JsonSchemaGenerator();
            Schema = j.Generate(value).ToString();
            _responseType = value;
          }
        }
    }
}
