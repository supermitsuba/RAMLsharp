module RAMLSharp.Extensions

open System

type Type with
    member x.ToRamlType(typeValue) = 
        ""

    member x.IsComplexModel(typeValue) = 
        true

    member x.GetForRealType(typeValue) = 
        "".GetType()
        
