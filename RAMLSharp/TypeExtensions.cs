using System;

namespace RAMLSharp
{
    /// <summary>
    /// Useful extensions for Types
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Used to convert .net type into a RAML type.  Raml 0.8 has limited primitive types:  integer, number, boolean, date, string and file.
        /// </summary>
        /// <param name="typeValue">The .net type to convert</param>
        /// <returns>The RAML type of the .net type</returns>
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

        /// <summary>
        /// This function determines if the .net object is complex, which means that if it does not fit a RAML primitive type, then it is complex.
        /// </summary>
        /// <param name="typeValue">The .net type to compare</param>
        /// <returns>True if it is complex, false if it is a primitive</returns>
        public static bool IsComplexModel(this Type typeValue)
        {
            Type value = typeValue;

            if (value.IsGenericType && value.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                value = Nullable.GetUnderlyingType(value);
            }

            switch (Type.GetTypeCode(value))
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

        /// <summary>
        /// If the type is Null, then it gives back a weird type that sometimes is a null and sometimes not.  this clarifies that ambiguity.
        /// </summary>
        /// <param name="typeValue">The type to get the Real type from.</param>
        /// <returns>Returns the real type, even if the thing is nullable</returns>
        public static Type GetForRealType(this Type typeValue)
        {
            Type value = typeValue;

            if (value.IsGenericType && value.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                value = Nullable.GetUnderlyingType(value);
            }

            return value;
        }
    }
}
