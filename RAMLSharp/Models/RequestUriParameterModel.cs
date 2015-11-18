using System;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This model represents a request uri parameter.
    /// </summary>
    public class RequestUriParameterModel
    {
        /// <summary>
        /// The name of the URI parameter.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of what the URI parameter.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The .net type of the URI parameter.  This is later converted to RAML type.
        /// </summary>
        public Type @Type { get; set; }
        /// <summary>
        /// If the URI parameter is required, true, otherwise false.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// Example of values for the URI parameter.
        /// </summary>
        public string Example { get; set; }
    }
}
