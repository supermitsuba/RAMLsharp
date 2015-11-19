using System;

namespace RAMLSharp.Models
{
    /// <summary>
    /// This model represents the headers from the RequestHeadersAttribute.
    /// </summary>
    public class RequestHeaderModel
    {
        /// <summary>
        /// The name of the header. Ex. Accept
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// An example of the header values.
        /// </summary>
        public string Example { get; set; }
        /// <summary>
        /// The description of what the header does for you.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The .net type that the header value is.  This is later converted to a RAML type.
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// If the header is required, then it is true, otherwise false.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// The minimum value of the header if it is of type number or integer
        /// </summary>
        public int Minimum { get; set; }
        /// <summary>
        /// The maximum value of the header if it is of type number or integer
        /// </summary>
        public int Maximum { get; set; }


        /// <summary>
        /// Only for string.  A regex expression of a pattern that the header value must follow.  Ex: phone number would be '^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$'
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Only for string.  A minimum length of text.
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Only for string.  A maximum length of text.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Whether the header value can be repeated.  True or false.
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// The default value for the header.
        /// </summary>
        public string DefaultValue { get; set; }

    }
}
