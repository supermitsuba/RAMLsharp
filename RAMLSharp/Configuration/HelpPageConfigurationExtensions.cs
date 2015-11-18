using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Description;

namespace RAMLSharp.Configuration
{
    [ExcludeFromCodeCoverage()] // This was auto generated code from Web API Help Pages
    public static class HelpPageConfigurationExtensions
    {
        private const string ApiModelPrefix = "MS_HelpPageApiModel_";

        /// <summary>
        /// Sets the documentation provider for help page.
        /// </summary>
        /// <param name="config">The <see cref="HttpConfiguration"/>.</param>
        /// <param name="documentationProvider">The documentation provider.</param>
        public static void SetDocumentationProvider(this HttpConfiguration config, IDocumentationProvider documentationProvider)
        {
            config.Services.Replace(typeof(IDocumentationProvider), documentationProvider);
        }
    }
}
