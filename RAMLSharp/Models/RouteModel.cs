using System.Collections.Generic;

namespace RAMLSharp.Models
{
    public class RouteModel
    {
        public string UrlTemplate { get; set; }
        public string Verb { get; set; }
        public string Description { get; set; }
        public IList<RequestHeaderModel> Headers { get; set; }
        public IList<RequestQueryParameterModel> QueryParameters { get; set; }
        public IList<RequestUriParameterModel> UriParameters { get; set; }
        public IList<RequestBodyParameterModel> BodyParameters { get; set; }
        public IList<ResponseModel> Responses { get; set; }
        public string RequestContentType { get; set; }
    }
}
