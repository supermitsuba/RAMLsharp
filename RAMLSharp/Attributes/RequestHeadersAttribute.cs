using System;

namespace RAMLSharp.Attributes
{
    /// <summary>
    /// This is used to describe headers that your Web API may be looking for.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class RequestHeadersAttribute : Attribute
    {
        /// <summary>
        /// This is the name of the header.  Ex: Accept
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// This is the description of your header and why you need it.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This is the .net type of the header value.  This is automatically converted later to RAML type.
        /// </summary>
        public Type @Type { get; set; }
        /// <summary>
        /// Is this header required to execute the API.
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
    }
}
