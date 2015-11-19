using System.Collections.Generic;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This model represents a given URL resource.
    /// </summary>
    public class RouteModel
    {
        /// <summary>
        /// This is the template string for a uri. Ex:  /api/test/{testId}
        /// </summary>
        public string UrlTemplate { get; set; }
        /// <summary>
        /// This is the Http Verb of the resource.  Ex:  GET, PUT, POST, DELETE, HEAD, etc.
        /// </summary>
        public string Verb { get; set; }
        /// <summary>
        /// The description of what that resource does.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// List of all the headers on the given resource.
        /// </summary>
        public IList<RequestHeaderModel> Headers { get; set; }
        /// <summary>
        /// List of all Uri parameters on the resource.
        /// </summary>
        public IList<RequestUriParameterModel> UriParameters { get; set; }
        /// <summary>
        /// List of all the body parameters on a given resource.
        /// </summary>
        public IList<RequestBodyParameterModel> BodyParameters { get; set; }
        /// <summary>
        /// List of all possible responses for a given resource.
        /// </summary>
        public IList<ResponseModel> Responses { get; set; }
        /// <summary>
        /// The content type of a requested resource.  Ex:  application/json
        /// </summary>
        public string RequestContentType { get; set; }
    }
}
