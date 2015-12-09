using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Description;

namespace RAMLSharp.Test.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeApiDescription : ApiDescription
    {
        public FakeApiDescription(Collection<ApiParameterDescription> descriptions)
        {
            foreach (var d in descriptions)
            {
                this.ParameterDescriptions.Add(d);
            }
        }
    }
}
