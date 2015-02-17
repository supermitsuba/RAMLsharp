using System;
using System.Reflection;

namespace RAMLSharp.Configuration
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}