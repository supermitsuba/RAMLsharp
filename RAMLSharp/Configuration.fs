namespace RAMLSharp.Configuration

open System
open System.Reflection
open System.Runtime.CompilerServices
open System.Diagnostics.CodeAnalysis
open System.Globalization
open System.Linq
open System.Reflection
open System.Web.Http
open System.Web.Http.Controllers
open System.Web.Http.Description
open System.Xml.XPath

type IModelDocumentationProvider=
    abstract member GetDocumentation: MemberInfo -> string 
    abstract member GetDocumentation: Type -> string

[<ExcludeFromCodeCoverage()>]
[<Extension()>] 
type HttpConfigurationExtension =
    //let ApiModelPrefix = "MS_HelpPageApiModel_"

    /// <summary>
    /// Sets the documentation provider for help page.
    /// </summary>
    /// <param name="config">The <see cref="HttpConfiguration"/>.</param>
    /// <param name="documentationProvider">The documentation provider.</param>
    [<Extension()>]
    static member SetDocumentationProvider(config:HttpConfiguration , documentationProvider:IDocumentationProvider ) =
        config.Services.Replace(typeof<IDocumentationProvider>, documentationProvider)
        ()

/// <summary>
/// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
/// </summary>
[<ExcludeFromCodeCoverage()>] // This was auto generated code from Web API Help Pages
type XmlDocumentationProvider (documentPath:string) =

    let _documentNavigator:XPathNavigator = 
        match documentPath with
        | null -> raise( new ArgumentNullException("documentPath") )
                  null
        | _ -> let xpath = new XPathDocument(documentPath)
               xpath.CreateNavigator()

    let TypeExpression = "/doc/members/member[@name='T:{0}']"
    let MethodExpression = "/doc/members/member[@name='M:{0}']"
    let PropertyExpression = "/doc/members/member[@name='P:{0}']"
    let FieldExpression = "/doc/members/member[@name='F:{0}']"
    let ParameterExpression = "param[@name='{0}']"

    let rec GetTypeName(``type``:Type) =         
        let mutable name = ``type``.FullName

        match ``type``.IsGenericType with
        | false -> ()
        | true -> let genericType = ``type``.GetGenericTypeDefinition()
                  let genericArguments = ``type``.GetGenericArguments()

                  // Trim the generic parameter counts from the name
                  let genericTypeName = genericType.FullName.Substring(0, genericType.FullName.IndexOf('`'))

                  let argumentTypeNames:string[] = genericArguments.Select(fun t -> GetTypeName(t)).ToArray()
                  name <- String.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", genericTypeName, String.Join(",", argumentTypeNames))

        match ``type``.IsNested with
        | false -> ()
        | true -> name <- name.Replace("+", ".")

        name

    let GetTypeNode(``type``:Type)=
        let controllerTypeName = GetTypeName(``type``)
        let selectExpression = String.Format(CultureInfo.InvariantCulture, TypeExpression, controllerTypeName);
        _documentNavigator.SelectSingleNode(selectExpression)

    let GetTagValue(parentNode:XPathNavigator, tagName:string) = 
        match parentNode with
        | null -> null
        | _    -> let node = parentNode.SelectSingleNode(tagName)
                  match node with
                  | null -> null
                  | _ -> node.Value.Trim()

    let GetMemberName(``method``:MethodInfo) =
            let name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(``method``.DeclaringType), ``method``.Name)
            let parameters = ``method``.GetParameters();
            match parameters.Length with
            | 0 -> name
            | _ -> let parameterTypeNames = parameters.Select(fun param -> GetTypeName(param.ParameterType)).ToArray()
                   name + String.Format(CultureInfo.InvariantCulture, "({0})", String.Join(",", parameterTypeNames))

    let GetMethodNode(actionDescriptor: HttpActionDescriptor) = 
        match actionDescriptor with
        | :? ReflectedHttpActionDescriptor as s -> let selectExpression = String.Format(CultureInfo.InvariantCulture, MethodExpression, GetMemberName(s.MethodInfo))
                                                   _documentNavigator.SelectSingleNode(selectExpression)
        | _ -> null
            

    interface IDocumentationProvider with
        member this.GetDocumentation(controllerDescriptor:HttpControllerDescriptor) =
            let typeNode = GetTypeNode(controllerDescriptor.ControllerType)
            GetTagValue(typeNode, "summary")

        member this.GetDocumentation(actionDescriptor:HttpActionDescriptor) =
            let methodNode = GetMethodNode(actionDescriptor);
            GetTagValue(methodNode, "summary")

        member this.GetDocumentation (parameterDescriptor:HttpParameterDescriptor) = 
            match parameterDescriptor with
            | :? ReflectedHttpParameterDescriptor as r -> let methodNode = GetMethodNode(r.ActionDescriptor)
                                                          match methodNode with 
                                                          | null -> null
                                                          | _ -> let parameterName = r.ParameterInfo.Name
                                                                 let parameterNode = methodNode.SelectSingleNode(String.Format(CultureInfo.InvariantCulture, ParameterExpression, parameterName))
                                                                 match parameterNode with 
                                                                 | null -> null
                                                                 | _ -> parameterNode.Value.Trim()
            | _ -> null

        member this.GetResponseDocumentation (actionDescriptor:HttpActionDescriptor) = 
            let methodNode = GetMethodNode(actionDescriptor);
            GetTagValue(methodNode, "returns")

    interface IModelDocumentationProvider with
        member this.GetDocumentation ( ``member`` :MemberInfo) =
            let memberName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(``member``.DeclaringType), ``member``.Name)
            let expression = match ``member``.MemberType = MemberTypes.Field with
                             | true -> FieldExpression 
                             | false -> PropertyExpression
            let selectExpression = String.Format(CultureInfo.InvariantCulture, expression, memberName)
            let propertyNode = _documentNavigator.SelectSingleNode(selectExpression)
            GetTagValue(propertyNode, "summary")

        member this.GetDocumentation ( ``type`` :Type) =
            let typeNode = GetTypeNode( ``type`` )
            GetTagValue(typeNode, "summary")