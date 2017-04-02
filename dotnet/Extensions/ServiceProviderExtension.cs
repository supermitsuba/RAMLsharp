using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace RAMLSharp.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceProviderExtension
    {
        public static IServiceProvider AddRamlGenerator(this IServiceProvider provider)
        {
            //IApiDescriptionGroupCollectionProvider
            var apiDescriptions = provider.GetService(typeof(IApiDescriptionGroupCollectionProvider)) as IApiDescriptionGroupCollectionProvider;
            RAMLMapper mapper = new RAMLMapper(apiDescriptions);
            //mapper.WebApiToRamlModel()
            return provider;
        }

        public static IServiceProvider AddRamlEndpoint(this IServiceProvider provider, Func<string> route)
        {
            return provider;
        }
    }
}