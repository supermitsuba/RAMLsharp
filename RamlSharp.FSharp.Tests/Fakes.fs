module Fakes

open System
open System.Collections.ObjectModel
open System.Diagnostics.CodeAnalysis
open System.Web.Http.Description

[<ExcludeFromCodeCoverage>]
type FakeComplex(field1, field2, field3) =
    member val Field1 : string = field1   with get, set
    member val Field2 : int = field2   with get, set
    member val Field3 : DateTime = field3   with get, set