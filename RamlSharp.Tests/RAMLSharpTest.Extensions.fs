module RAMLSharpTest.Extensions
open System
open NUnit.Framework
open RAMLSharp
open RAMLSharp.Extensions
open System.Diagnostics.CodeAnalysis

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Int32 should return integer``() =
    Assert.AreEqual(typeof<Int32>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on SByte should return integer``() =
    Assert.AreEqual(typeof<SByte>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on UInt16 should return integer``() =
    Assert.AreEqual(typeof<UInt16>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on UInt32 should return integer``() =
    Assert.AreEqual(typeof<UInt32>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on UInt64 should return integer``() =
    Assert.AreEqual(typeof<UInt64>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Int16 should return integer``() =
    Assert.AreEqual(typeof<Int16>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Int64 should return integer``() =
    Assert.AreEqual(typeof<Int64>.ToRamlType(), "integer", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Decimal should return number``() =
    Assert.AreEqual(typeof<Decimal>.ToRamlType(), "number", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Double should return number``() =
    Assert.AreEqual(typeof<Double>.ToRamlType(), "number", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Single should return number``() =
    Assert.AreEqual(typeof<Single>.ToRamlType(), "number", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on boolean should return boolean``() =
    Assert.AreEqual(typeof<Boolean>.ToRamlType(), "boolean", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on DateTime should return date``() =
    Assert.AreEqual(typeof<DateTime>.ToRamlType(), "date", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on string should return string``() =
    Assert.AreEqual(typeof<string>.ToRamlType(), "string", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``ToRamlType on Object should return string``() =
    Assert.AreEqual(typeof<Object>.ToRamlType(), "string", "The return string should be 'integer'.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``String is not a complex object model``() =
    Assert.AreEqual(typeof<string>.IsComplexModel(), false, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Extension_Tests is a complex object model``() =
    Assert.AreEqual(typeof<RAMLMapper>.IsComplexModel(), true, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Nullable Int is not a complex object model``() =
    let value = typeof<Nullable<int>>;
    Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Nullable Long is not a complex object model``() =
    let value = typeof<Nullable<Int64>>;
    Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Nullable DateTime is not a complex object model``() =
    let value = typeof<Nullable<DateTime>>;
    Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Nullable bool is not a complex object model``() =
    let value = typeof<Nullable<bool>>;
    Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

[<Test>]
[<ExcludeFromCodeCoverage>]
let ``Nullable decimal is not a complex object model``() =
    let value = typeof<Nullable<decimal>>;
    Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")