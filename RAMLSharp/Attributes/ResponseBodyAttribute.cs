using System;
using System.Net;

namespace RAMLSharp.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ResponseBodyAttribute : Attribute
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Example { get; set; }
        public string Description { get; set; }
        public string Schema { get; set; }
    }
}
