using System.Net;

namespace RAMLSharp.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Example { get; set; }
        public string Description { get; set; }
    }
}
