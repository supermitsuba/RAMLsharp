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

        /// <summary>
        /// This constructor is the main constructor to pass in your controller and find out about your Web API.
        /// </summary>
        /// <param name="controller">The controller that is hosting your API.</param>
        public RAMLMapper(ApiController controller)
        {
            _apiDescriptions = controller.Configuration.Services.GetApiExplorer().ApiDescriptions;
        }

        /// <summary>
        /// This constructor is used to test.
        /// </summary>
        /// <param name="descriptions">A list of fake ApiDescriptions to generate a raml from.</param>
        public RAMLMapper(IEnumerable<ApiDescription> descriptions)
        {
            _apiDescriptions = descriptions ?? new List<ApiDescription>();
        }

        /// <summary>
        /// This method takes a few more pieces not described in your API and adds them to the RAML output.
        /// </summary>
        /// <param name="baseUri">The base URL of your API.</param>
        /// <param name="title">The title or name of your API.</param>
        /// <param name="version">The version of your API.</param>
        /// <param name="defaultMediaTypes">The default media types that your API supports.  Ex: application/json or application/xml</param>
        /// <param name="description">What is the purpose of your API.</param>
        /// <returns></returns>
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
                IEnumerable<RequestHeadersDocumentationAttribute> headers = null;
                IEnumerable<ResponseBodyDocumentationAttribute> responseBody = null;

                if (api.ActionDescriptor != null)
                {
                    headers = api.ActionDescriptor.GetCustomAttributes<RequestHeadersDocumentationAttribute>();
                    responseBody = api.ActionDescriptor.GetCustomAttributes<ResponseBodyDocumentationAttribute>();
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

        private static IList<ResponseModel> GetResponseBodies(IEnumerable<ResponseBodyDocumentationAttribute> attributes)
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
            var result = new List<RequestUriParameterModel>();

            var complexParameters = description.ParameterDescriptions
                    .Where(r => r.Source == ApiParameterSource.FromUri && r.ParameterDescriptor.ParameterType.IsComplexModel())
                    .Select(s => new
                    {
                        Properties = s.ParameterDescriptor.ParameterType.GetProperties(),
                        IsOptional = s.ParameterDescriptor.IsOptional,
                        Description = s.Documentation,
                        Example = s.ParameterDescriptor.DefaultValue == null ? "" : s.ParameterDescriptor.DefaultValue.ToString()
                    });

            foreach (var parameter in complexParameters)
            {
                var temp = parameter.Properties.Select(q => new RequestUriParameterModel
                {
                    Name = q.Name,
                    Description = parameter.Description,
                    IsRequired = parameter.IsOptional,
                    Type = q.PropertyType,
                    Example = parameter.Example,
                });

                result.AddRange(temp);
            };

            var notComplexParameters = description.ParameterDescriptions
                    .Where(r => r.Source == ApiParameterSource.FromUri && !r.ParameterDescriptor.ParameterType.IsComplexModel())
                    .Select(q => new RequestUriParameterModel
                    {
                        Name = q.Name,
                        Description = q.Documentation,
                        IsRequired = q.ParameterDescriptor.IsOptional,
                        Type = q.ParameterDescriptor.ParameterType,
                        Example = q.ParameterDescriptor.DefaultValue == null ? "" : q.ParameterDescriptor.DefaultValue.ToString()
                    });

            result.AddRange(notComplexParameters);

            return result;
        }

        private static IList<RequestHeaderModel> GetHeaders(IEnumerable<RequestHeadersDocumentationAttribute> attributes)
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
                               Minimum = h.Minimum,

                               DefaultValue = h.DefaultValue,
                               MaxLength = h.MaxLength,
                               MinLength = h.MinLength,
                               Pattern = h.Pattern,
                               Repeat = h.Repeat
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