using System;

namespace RAMLSharp.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class RequestHeadersAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type @Type { get; set; }
        public bool IsRequired { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string Example { get; set; }
    }
}
