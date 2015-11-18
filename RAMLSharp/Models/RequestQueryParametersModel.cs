using System;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This represents the query parameters for a given resource.
    /// </summary>
    public class RequestQueryParameterModel
    {
        /// <summary>
        /// The name of the query parameter.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of what the parameter does for you.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The .net type that the query string parameter.  This is later converted into a RAML type.
        /// </summary>
        public Type @Type { get; set; }
        /// <summary>
        /// If the query parameter is required then this will be true, else false.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// The example values that could be used for the query string parameter.
        /// </summary>
        public string Example { get; set; }
    }
}
