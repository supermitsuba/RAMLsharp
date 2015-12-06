module RAMLSharp.Extensions

open System
open System.Diagnostics.CodeAnalysis
open System.Web.Http
open System.Web.Http.Description

type Type with
    member x.ToRamlType(typeValue) = 
        ""

    member x.IsComplexModel(typeValue) = 
        true

    member x.GetForRealType(typeValue) = 
        "".GetType()
