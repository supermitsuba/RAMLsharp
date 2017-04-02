using System;
using System.Text;

namespace RAMLSharp.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringBuilderExtension
    {
        public static StringBuilder CreateVersion(this StringBuilder sb, string version)
        {
            if(string.IsNullOrEmpty(version))
            {
                sb.AppendFormat("version: {0}{1}", version , Environment.NewLine);
            }
            return sb;
        }

        public static StringBuilder CreateDefaultMediaType(this StringBuilder sb, string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                sb.AppendFormat("mediaType: {0}{1}", value , Environment.NewLine);
            }
            return sb;
        }

        public static StringBuilder CreateDocumentation(this StringBuilder sb, string description)
        {
            if(string.IsNullOrEmpty(description))
            {
                sb.AppendFormat("documentation: {0}", Environment.NewLine)
                    .AppendFormat("  - title: The Description of the API{0}", Environment.NewLine)
                    .AppendFormat("    content: |{0}", Environment.NewLine)
                    .AppendFormat("      {0}{1}", description , Environment.NewLine);
            }
            return sb;
        }

        public static StringBuilder CreateBody(this StringBuilder sb, string requestedContentType)
        {
            if(string.IsNullOrEmpty(requestedContentType))
            {
                sb.AppendFormat("    body:{0}", Environment.NewLine)
                    .AppendFormat("      {0}: {1}", requestedContentType,  Environment.NewLine)
                    .AppendFormat("        formParameters: {0}", Environment.NewLine);
            }
            return sb;
        }

        public static StringBuilder CreateBodyParameters(this StringBuilder sb, string name, string description, string ramlType, string isRequired, string example)
        {
            sb.AppendFormat("          {0}: {1}", name, Environment.NewLine)
                .AppendFormat("            description  : {0}{1}", description, Environment.NewLine)
                .AppendFormat("            type  : {0}{1}", ramlType, Environment.NewLine)
                .AppendFormat("            required  : {0}{1}", isRequired, Environment.NewLine)
                .AppendFormat("            example  : {0}{1}", example, Environment.NewLine);
            return sb;
        }

        public static StringBuilder CreateHeaders(this StringBuilder sb, string name, string example, string ramlType, int min, int max, string description)
        {
            sb.AppendFormat("      {0}: {1}", name, Environment.NewLine)
                .AppendFormat("        displayName: {0}{1}", name, Environment.NewLine);

            if(!string.IsNullOrEmpty(example))
            {
                sb.AppendFormat("        example: {0}{1}", example, Environment.NewLine);
            }

            if(!string.IsNullOrEmpty(ramlType))
            {
                sb.AppendFormat("        type: {0}{1}", ramlType, Environment.NewLine);
            }

            if(ramlType == "number" || ramlType == "integer")
            {
                sb.AppendFormat("        minimum: {0}{1}", min, Environment.NewLine)
                  .AppendFormat("        maximum: {0}{1}", max, Environment.NewLine);
            }

            if(!string.IsNullOrEmpty(description))
            {
                sb.AppendFormat("        description: {0}{1}", description, Environment.NewLine);
            }
            return sb;
        }

        public static StringBuilder CreateQueryParameters(this StringBuilder sb, string name, string description, string ramlType, string isRequired, string example)
        {
            sb.AppendFormat("        {0}: {1}", name, Environment.NewLine)
                .AppendFormat("          type: {0}{1}", ramlType, Environment.NewLine )
                .AppendFormat("          required: {0}{1}", isRequired.ToString().ToLower(), Environment.NewLine )
                .AppendFormat("          description: {0}{1}", description, Environment.NewLine )
                .AppendFormat("          example: |{1}            {0}{1}", example, Environment.NewLine );
            return sb;
        }

        public static StringBuilder CreateUriParameters(this StringBuilder sb, string name, string description, string ramlType, string isRequired, string example)
        {
            sb.AppendFormat("      {0}: {1}", name, Environment.NewLine)
                .AppendFormat("        type: {0}{1}", ramlType, Environment.NewLine)
                .AppendFormat("        required: {0}{1}", isRequired, Environment.NewLine)
                .AppendFormat("        description: {0}{1}", description, Environment.NewLine)
                .AppendFormat("        example: |{1}            {0}{1}", example, Environment.NewLine);
            return sb;
        }

        public static StringBuilder CreateResponse(this StringBuilder sb, int statusCode, string description, string contentType, string schema, string example)
        {
            sb.AppendFormat("      {0}:{1}", statusCode, Environment.NewLine);
            if(!string.IsNullOrEmpty(description))
            {
                sb.AppendFormat("        description: |{1}          {0}{1}", description, Environment.NewLine);
            }

            sb.AppendFormat("        body:{0}", Environment.NewLine)
                .AppendFormat("          {0}: {1}", contentType, Environment.NewLine);

            if(!string.IsNullOrEmpty(schema))
            {
                sb.AppendFormat("            schema: {0}{1}", schema.Replace("\n", ""), Environment.NewLine);
            }

            if(!string.IsNullOrEmpty(example))
            {
                sb.AppendFormat("            example: |{1}                {0}{1}", example.Replace("\n", ""), Environment.NewLine);
            }

            return sb;
        }
    }
}