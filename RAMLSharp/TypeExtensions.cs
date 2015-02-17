using System;

namespace RAMLSharp
{
    public static class TypeExtensions
    {
        public static string ToRamlType(this Type typeValue)
        {
            switch (Type.GetTypeCode(typeValue))
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
                default:
                    return "string";
            }
        }
    }
}
