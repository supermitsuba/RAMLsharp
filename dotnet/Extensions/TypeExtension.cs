using System;
using System.Reflection;

namespace RAMLSharp.Extensions
{
    public static class TypeExtension
    {
        public static string ToRamlType(this Type typeValue)
        {    
            switch(Type.GetTypeCode(typeValue))
            {
                case TypeCode.Byte: 
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return "integer";
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return "number";
                case TypeCode.Boolean:
                    return "boolean";
                case TypeCode.DateTime:
                    return "date";
                case TypeCode.String:
                    return "string";
                default:
                    return "string";
            }
        }

        public static bool IsComplexModel(this Type typeValue)
        {
            return IsNetType(GetForRealType(typeValue));
        }

        public static Type GetForRealType(this Type typeValue)
        {
            if( typeValue.GetTypeInfo().IsGenericType 
                && (typeValue.GetGenericTypeDefinition() == typeof(Nullable<>)) ) 
            {
                return Nullable.GetUnderlyingType(typeValue);
            }
            else 
            {
                return typeValue;
            }
        }

        private static bool IsNetType(this Type typeValue) 
        {
            switch(Type.GetTypeCode(typeValue))
            {
                case TypeCode.Byte: 
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.Boolean:
                case TypeCode.DateTime:
                case TypeCode.String:
                    return false;
                default:
                    return true;
            }
        }
    }
}
