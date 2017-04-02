using System;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This model represents what a request body may look like.
    /// </summary>
    public class RequestBodyParameterModel
    {
        /// <summary>
        /// The name of a body you can submit to the API
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of what the request body payload does.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The data type of the payload.  This is later converted to RAML types.
        /// </summary>
        public Type @Type { get; set; }
        /// <summary>
        /// Is a body required would be true, otherwise false.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// An example of the request body payload.
        /// </summary>
        public string Example { get; set; }
    }
}