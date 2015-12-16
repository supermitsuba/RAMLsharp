using System.Web;
using System.Web.Http;
using RAMLSharp.Configuration;

namespace RAMLSharp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes public static void Register(HttpConfiguration config)
			var xml = new RAMLSharp.Configuration.XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml"));
			config.SetDocumentationProvider(xml);

            config.MapHttpAttributeRoutes();
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
             */
        }
    }
}
