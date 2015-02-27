using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using RAMLSharp.Attributes;
using RAMLSharp.Models;

namespace RAMLSharp
{
    public class RAMLMapper
    {
        private readonly IEnumerable<ApiDescription> _apiDescriptions;

        public RAMLMapper(ApiController controller)
        {
            _apiDescriptions = controller.Configuration.Services.GetApiExplorer().ApiDescriptions;
        }

        public RAMLMapper(IEnumerable<ApiDescription> descriptions)
        {
            _apiDescriptions = descriptions ?? new List<ApiDescription>();
        }

        public RAMLModel WebApiToRamlModel(Uri baseUri, string title, string version, string defaultMediaTypes, string description)
        {
            var model = new RAMLModel
            {
                BaseUri = baseUri, //new Uri("http://www.google.com"),
                Title = title, //"Super web api",
                Version = version, //"1"
                DefaultMediaType = defaultMediaTypes,
                Description = description,
                Routes = new List<RouteModel>()
            };

            foreach (var api in _apiDescriptions)
            {
                IEnumerable<RequestHeadersAttribute> headers = null;
                IEnumerable<ResponseBodyAttribute> responseBody = null;

                if(api.ActionDescriptor != null)
                {
                    headers = api.ActionDescriptor.GetCustomAttributes<RequestHeadersAttribute>();
                    responseBody = api.ActionDescriptor.GetCustomAttributes<ResponseBodyAttribute>();
                }

                var routeModel = new RouteModel
                {
                    Verb = api.HttpMethod.Method.ToLower(),
                    UrlTemplate = api.RelativePath,
                    Headers = GetHeaders(headers),
                    //QueryParameters = GetQueryParameters(api),
                    BodyParameters = GetBodyParameters(api),
                    UriParameters = GetUriParameters(api),
                    Responses = GetResponseBodies(responseBody),
                    Description = api.Documentation
                };

                if (routeModel.Verb == "put" || routeModel.Verb == "post") 
                {
                    routeModel.RequestContentType = "application/x-www-form-urlencoded:";
                }

                model.Routes.Add(routeModel);
            }

            return model;
        }


        #region Private Functions.  These are used to parse the data.

        private static IList<ResponseModel> GetResponseBodies(IEnumerable<ResponseBodyAttribute> attributes)
        {
            var responseModel = new List<ResponseModel>();
            if (attributes != null)
            {
                responseModel = attributes.Select(a => new ResponseModel
                {
                    ContentType = a.ContentType,
                    Example = File.Exists(a.Example) ? File.ReadAllText(a.Example) : a.Example,
                    StatusCode = a.StatusCode,
                    Description = a.Description,
                    Schema = a.Schema
                }).ToList();
            }

            return responseModel;
        }

        private static IList<RequestBodyParameterModel> GetBodyParameters(ApiDescription description)
        {
            return description.ParameterDescriptions
                .Where(r => r.Source == ApiParameterSource.FromBody)
                .Select(q => new RequestBodyParameterModel
                {
                    Name = q.Name, 
                    Description = q.Documentation, 
                    IsRequired = q.ParameterDescriptor.IsOptional, 
                    Type = q.ParameterDescriptor.ParameterType,
                    Example = q.ParameterDescriptor.DefaultValue == null ? "" : q.ParameterDescriptor.DefaultValue.ToString()
                })
                .ToList();
        }

        private static IList<RequestUriParameterModel> GetUriParameters(ApiDescription description)
        {
            return description.ParameterDescriptions
                    .Where(r => r.Source == ApiParameterSource.FromUri)
                    .Select(q => new RequestUriParameterModel
                    {
                        Name = q.Name,
                        Description = q.Documentation,
                        IsRequired = q.ParameterDescriptor.IsOptional,
                        Type = q.ParameterDescriptor.ParameterType,
                        Example = q.ParameterDescriptor.DefaultValue == null ? "" : q.ParameterDescriptor.DefaultValue.ToString()
                    })
                    .ToList();
        }

        private static IList<RequestHeaderModel> GetHeaders(IEnumerable<RequestHeadersAttribute> attributes)
        {
            var requestHeaderModel = new List<RequestHeaderModel>();
            if (attributes != null)
            {
                requestHeaderModel = attributes.Select(
                       h =>
                       {
                           var newModel = new RequestHeaderModel
                           {
                               Name = h.Name,
                               Example = h.Example,
                               Description = h.Description,
                               @Type = h.Type,
                               IsRequired = h.IsRequired,
                               Maximum = h.Maximum,
                               Minimum = h.Minimum
                           };

                           return newModel;
                       }
                   ).ToList();
            }

            return requestHeaderModel;
        }

        #endregion
    }
}