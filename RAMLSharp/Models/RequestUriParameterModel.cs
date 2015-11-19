﻿using System;

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

        /// <summary>
        /// This is for number or integer fields.  It specifies an acceptable minimum number.
        /// </summary>
        public int Minimum { get; set; }
        /// <summary>
        /// This is for number or integer fields.  It specifies an acceptable maximum number.
        /// </summary>
        public int Maximum { get; set; }

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
