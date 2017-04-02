using System;
using System.Collections.Generic;
using RAMLSharp.Extensions;
using System.Linq;
using System.Text;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This is the main object that is parsed from the API descriptors in the ASP.net Web API Help pages API.  We use this object to convert it into RAML.
    /// </summary>
    public class RAMLModel
    {
        /// <summary>
        /// The title of the API.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The base URL of the API.
        /// </summary>
        public Uri BaseUri { get; set; }
        /// <summary>
        /// The Version of the API
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The default media types used to submit a request and response.
        /// </summary>
        public string DefaultMediaType { get; set; }
        /// <summary>
        /// The description of your API.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A list of routes, or in an API's case, a list of resources in the API.
        /// </summary>
        public IList<RouteModel> Routes { get; set; }
        
        /// <summary>
        /// This is used to output RAML from the RAMLModel.
        /// </summary>
        /// <returns>Returns a raml string of the model.</returns>
        public override string ToString()
        {
            var RAML = new StringBuilder();
            var UriScheme = BaseUri == null ? "HTTP" : BaseUri.Scheme.ToUpper();
            RAML.Append("#%RAML 1.0" + Environment.NewLine) 
              .Append("---" + Environment.NewLine)
              .AppendFormat("title: {0}{1}", this.Title , Environment.NewLine)
              .AppendFormat("baseUri: {0}{1}", this.BaseUri , Environment.NewLine)
              .AppendFormat("protocols: [{0}]{1}", UriScheme , Environment.NewLine)
              .CreateVersion(Version)
              .CreateDefaultMediaType(DefaultMediaType)
              .CreateDocumentation(Description);
            RAML = SetRamlBody(RAML);
            return RAML.ToString();
        }
        
        #region private methods
        private readonly string _newLine = Environment.NewLine;
        
        private Dictionary<string, bool> hasVisitedRoutes;
        private StringBuilder SetRamlBody(StringBuilder RAML)
        {
            if (Routes == null || Routes.Count == 0) return RAML;

            var routeGrouping = Routes.OrderBy(p => p.UrlTemplate)
                                      .GroupBy(p => p.UrlTemplate, p => p, (key, item) => new { Resource = key, Verbs = item });

            foreach (var urls in routeGrouping)
            {
                RAML = SetResources(RAML, urls);

                hasVisitedRoutes = new Dictionary<string, bool>();
                RAML = urls.Verbs.Aggregate(RAML, SetRoutes);
            }

            return RAML;
        }

        private StringBuilder SetResources(StringBuilder RAML, dynamic urls)
        {
            RAML.Append("/")
                .Append(urls.Resource)
                .AppendLine(":");
            return RAML;
        }

        private StringBuilder SetRoutes(StringBuilder RAML, RouteModel route)
        {
            if (!hasVisitedRoutes.ContainsKey(route.UrlTemplate) || !hasVisitedRoutes[route.UrlTemplate])
            {
                RAML = SetUriParameters(RAML, route);
                hasVisitedRoutes.Add(route.UrlTemplate, true);
            }

            RAML = SetHttpVerb(RAML, route);
            RAML = SetDescription(RAML, route);
            RAML = SetRequest(RAML, route);
            RAML = SetHeaders(RAML, route);
            RAML = SetParameters(RAML, route);
            RAML = SetResponses(RAML, route);
            return RAML;
        }

        
        
        private StringBuilder SetHttpVerb(StringBuilder RAML, RouteModel route)
        {
            RAML.AppendFormat("  {0}:{1}", route.Verb, _newLine);
            return RAML;
        }

        private StringBuilder SetDescription(StringBuilder RAML, RouteModel route)
        {
            RAML.AppendFormat("    description: {0}{1}", route.Description, _newLine);
            return RAML;
        }

        private StringBuilder SetRequest(StringBuilder RAML, RouteModel route)
        {
            if (route.BodyParameters == null || route.BodyParameters.Count == 0 ) return RAML;
            RAML.CreateBody(route.RequestContentType);

            foreach (var r in route.BodyParameters)
            {
                RAML.CreateBodyParameters(r.Name, r.Description, r.Type.ToRamlType(), r.IsRequired.ToString(), r.Example);
            }
            return RAML;
        }

        private StringBuilder SetHeaders(StringBuilder RAML, RouteModel route)
        {
            if (route.Headers == null || 
                route.Headers.Count <= 0 ||
                route.Headers.All(p => String.IsNullOrEmpty(p.Name))) return RAML;

            RAML.AppendFormat("    headers:{0}", _newLine);
            return route.Headers.Aggregate(RAML, SetHeaderDetails);
        }

        private StringBuilder SetHeaderDetails(StringBuilder RAML, RequestHeaderModel header)
        {
            return RAML.CreateHeaders(header.Name, header.Example, header.Type.ToRamlType(), header.Minimum, header.Maximum, header.Description);
        }

        private StringBuilder SetParameters(StringBuilder RAML, RouteModel route)
        {
            if (route.QueryParameters == null || route.QueryParameters.Count == 0) return RAML;
            
            RAML.AppendFormat("    queryParameters: {0}", _newLine);
            foreach (var parameters in route.QueryParameters)
            {
                RAML.CreateQueryParameters(parameters.Name, parameters.Description, parameters.Type.ToRamlType(), parameters.IsRequired.ToString(), parameters.Example);
            }
            return RAML;
        }

        private StringBuilder SetUriParameters(StringBuilder RAML, RouteModel route)
        {
            if (route.UriParameters == null || route.UriParameters.Count <= 0) return RAML;

            RAML.AppendFormat("  uriParameters: {0}", _newLine);
            foreach (var parameters in route.UriParameters)
            {
                RAML.CreateUriParameters(parameters.Name, parameters.Description, parameters.Type.ToRamlType(), parameters.IsRequired.ToString(), parameters.Example);
            }
            return RAML;
        }

        private StringBuilder SetResponses(StringBuilder RAML, RouteModel route)
        {
            if (route.Responses == null || 
                route.Responses.Count <= 0 ||
                route.Responses.All(p => String.IsNullOrEmpty(p.ContentType))) return RAML;
           
            RAML.AppendFormat("    responses:{0}", _newLine);
            foreach (var response in route.Responses)
            {
                RAML.CreateResponse((int)response.StatusCode, response.Description, response.ContentType, response.Schema, response.Example);
            }
            return RAML;
        }

        #endregion

    }
}