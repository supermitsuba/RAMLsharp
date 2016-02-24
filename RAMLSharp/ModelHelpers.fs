module ModelHelpers
open System.Text
open System

let createVersion version (sb:StringBuilder) =
    match version with 
    | null -> sb
    | _    -> sb.AppendFormat("version: {0}{1}", version , Environment.NewLine)

let createDefaultMediaType value (sb:StringBuilder) =
    match value with 
    | null -> sb 
    | _    -> sb.AppendFormat("mediaType: {0}{1}", value , Environment.NewLine)

let createDocumentation description (sb:StringBuilder) =
    match description with 
    | null -> sb 
    | _    -> sb.AppendFormat("documentation: {0}", Environment.NewLine)
                .AppendFormat("  - title: The Description of the API{0}", Environment.NewLine)
                .AppendFormat("    content: |{0}", Environment.NewLine)
                .AppendFormat("      {0}{1}", description , Environment.NewLine) 

let createBody requestedContentType (sb:StringBuilder)  = 
    sb.AppendFormat("    body:{0}", Environment.NewLine)
        .AppendFormat("      {0}: {1}", requestedContentType,  Environment.NewLine)
        .AppendFormat("        formParameters: {0}", Environment.NewLine)  |> ignore

let createBodyParameters (name,description,ramlType,isRequired,example) (sb:StringBuilder) = 
    sb.AppendFormat("          {0}: {1}", name, Environment.NewLine)
        .AppendFormat("            description  : {0}{1}", description, Environment.NewLine)
        .AppendFormat("            type  : {0}{1}", ramlType, Environment.NewLine)
        .AppendFormat("            required  : {0}{1}", isRequired, Environment.NewLine)
        .AppendFormat("            example  : {0}{1}", example, Environment.NewLine) |> ignore                         
            
let createHeaders(name, example, ramlType, min, max, description) (sb:StringBuilder) = 
    sb.AppendFormat("      {0}: {1}", name, Environment.NewLine)
        .AppendFormat("        displayName: {0}{1}", name, Environment.NewLine) |> ignore
    match not(String.IsNullOrEmpty(example)) with 
    | true -> sb.AppendFormat("        example: {0}{1}", example, Environment.NewLine) |> ignore 
    | false -> ()
    match not(ramlType = null) with 
    | true -> sb.AppendFormat("        type: {0}{1}", ramlType, Environment.NewLine) |> ignore
    | false -> ()
    match not(ramlType <> "number" && ramlType <> "integer") with 
    | true -> sb.AppendFormat("        minimum: {0}{1}", min, Environment.NewLine)
                .AppendFormat("        maximum: {0}{1}", max, Environment.NewLine) |> ignore
    | false -> ()
    match not(String.IsNullOrEmpty(description)) with 
    | true -> sb.AppendFormat("        description: {0}{1}", description, Environment.NewLine) |> ignore
    | false -> () 

let createQueryParameters(name, ramlType, isRequired, description, example)(sb:StringBuilder) = 
    sb.AppendFormat("        {0}: {1}", name, Environment.NewLine)
      .AppendFormat("          type: {0}{1}", ramlType, Environment.NewLine )
      .AppendFormat("          required: {0}{1}", isRequired.ToString().ToLower(), Environment.NewLine )
      .AppendFormat("          description: {0}{1}", description, Environment.NewLine )
      .AppendFormat("          example: |{1}            {0}{1}", example, Environment.NewLine ) |> ignore

let createUriParameters(name, ramlType, isRequired, description, example)(sb:StringBuilder) =
    sb.AppendFormat("      {0}: {1}", name, Environment.NewLine)
      .AppendFormat("        type: {0}{1}", ramlType, Environment.NewLine)
      .AppendFormat("        required: {0}{1}", isRequired, Environment.NewLine)
      .AppendFormat("        description: {0}{1}", description, Environment.NewLine)
      .AppendFormat("        example: |{1}            {0}{1}", example, Environment.NewLine) |> ignore

let createResponse(statusCode, description, contentType, schema, example)(sb:StringBuilder) =  
    sb.AppendFormat("      {0}:{1}", statusCode, Environment.NewLine) |> ignore
    match not(String.IsNullOrEmpty(description)) with 
    | true -> sb.AppendFormat("        description: |{1}          {0}{1}", description, Environment.NewLine) |> ignore
    | false -> ()
    sb.AppendFormat("        body:{0}", Environment.NewLine)
      .AppendFormat("          {0}: {1}", contentType, Environment.NewLine) |> ignore
    match not(String.IsNullOrEmpty(schema)) with 
    | true ->  sb.AppendFormat("            schema: {0}{1}", schema.Replace("\n", ""), Environment.NewLine) |> ignore
    | false -> ()
    match not(String.IsNullOrEmpty(example)) with 
    | true -> sb.AppendFormat("            example: |{1}                {0}{1}", example.Replace("\n", ""), Environment.NewLine) |> ignore
    | false -> ()
