module RAMLSharp.Extensions

open System
open System.Runtime.CompilerServices

[<Extension()>] 
type TypeExtension =

    [<Extension()>] 
    static member ToRamlType(typeValue) = 
        match Type.GetTypeCode(typeValue) with
        | TypeCode.Byte 
        | TypeCode.SByte
        | TypeCode.UInt16
        | TypeCode.UInt32
        | TypeCode.UInt64
        | TypeCode.Int16
        | TypeCode.Int32
        | TypeCode.Int64    -> "integer"
        | TypeCode.Decimal
        | TypeCode.Double
        | TypeCode.Single   -> "number"
        | TypeCode.Boolean  -> "boolean"
        | TypeCode.DateTime -> "date"
        | _                 -> "string"

    [<Extension()>] 
    static member IsComplexModel(typeValue:Type) = 
        let checkType (t) =
            match Type.GetTypeCode(t) with
            | TypeCode.Byte 
            | TypeCode.SByte
            | TypeCode.UInt16
            | TypeCode.UInt32
            | TypeCode.UInt64
            | TypeCode.Int16
            | TypeCode.Int32
            | TypeCode.Int64    
            | TypeCode.Decimal
            | TypeCode.Double
            | TypeCode.Single   
            | TypeCode.Boolean  
            | TypeCode.DateTime 
            | TypeCode.String   -> false
            | _                 -> true

        match typeValue.GetTypeInfo().IsGenericType && (typeValue.GetGenericTypeDefinition() = typedefof<Nullable<_>>) with
        | true  -> checkType(Nullable.GetUnderlyingType(typeValue))
        | false -> checkType(typeValue)

    [<Extension()>] 
    static member GetForRealType(typeValue:Type) = 
        match typeValue.GetTypeInfo().IsGenericType && (typeValue.GetGenericTypeDefinition() = typedefof<Nullable<_>>) with
        | true  -> Nullable.GetUnderlyingType(typeValue)
        | false -> typeValue
