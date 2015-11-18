using System.Net;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This model is a representation of a response.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// The status code of a response.  Ex:  200, 300, 400, 500, etc.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// The content type of a response.  Ex:  application/json
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// The example payload of a response.
        /// </summary>
        public string Example { get; set; }
        /// <summary>
        /// The description of what is contained in a response.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The schema of the payload for the response.
        /// </summary>
        public string Schema { get; set; }
    }
}
