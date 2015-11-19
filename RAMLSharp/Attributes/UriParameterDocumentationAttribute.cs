using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMLSharp.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class UriParameterDocumentationAttribute : Attribute
    {
        /// <summary>
        /// This is the name of the parameter.  Ex: Accept
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// This is the description of your parameter and why you need it.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This is the .net type of the parameter value.  This is automatically converted later to RAML type.
        /// </summary>
        public Type @Type { get; set; }
        /// <summary>
        /// Is this parameter required to execute the API.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// This is for number or integer fields.  It specifies an acceptable minimum number.
        /// </summary>
        public int Minimum { get; set; }
        /// <summary>
        /// This is for number or integer fields.  It specifies an acceptable maximum number.
        /// </summary>
        public int Maximum { get; set; }
        /// <summary>
        /// This is an example of what the values are in the API.
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// Only for string.  A regex expression of a pattern that the parameter value must follow.  Ex: phone number would be '^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$'
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
        /// Whether the parameter value can be repeated.  True or false.
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// The default value for the parameter.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
