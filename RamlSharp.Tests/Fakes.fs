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

[<ExcludeFromCodeCoverage>]
type FakeInheritedComplex(field1, field2, field3, field4) =
    inherit FakeComplex(field1, field2, field3) 

    member val Field4 : string = field4   with get, set

[<ExcludeFromCodeCoverage>]
type FakeNestedComplex(field5, field6) =
    member val Field5 : string = field5   with get, set
    member val Field6 : FakeComplex = field6   with get, set