using System;

namespace RAMLSharp.Models
{
    public class RequestBodyParameterModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type @Type { get; set; }
        public bool IsRequired { get; set; }
        public string Example { get; set; }
    }
}
