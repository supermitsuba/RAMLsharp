namespace RamlSharp.FSharp.Tests
open System
open NUnit.Framework
open RAMLSharp.Extensions

[<TestFixture>]
type Extension_Tests() = 

    [<Test>]
    member x.TypeExtensions_Int32_ReturnsInteger() =
        Assert.AreEqual(typeof<Int32>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_SByte_ReturnsInteger() =
        Assert.AreEqual(typeof<SByte>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_UInt16_ReturnsInteger() =
        Assert.AreEqual(typeof<UInt16>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_UInt32_ReturnsInteger() =
        Assert.AreEqual(typeof<UInt32>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_UInt64_ReturnsInteger() =
        Assert.AreEqual(typeof<UInt64>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Int16_ReturnsInteger() =
        Assert.AreEqual(typeof<Int16>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Int64_ReturnsInteger() =
        Assert.AreEqual(typeof<Int64>.ToRamlType(), "integer", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Decimal_ReturnsNumber() =
        Assert.AreEqual(typeof<Decimal>.ToRamlType(), "number", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Double_ReturnsNumber() =
        Assert.AreEqual(typeof<Double>.ToRamlType(), "number", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Single_ReturnsNumber() =
        Assert.AreEqual(typeof<Single>.ToRamlType(), "number", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_Boolean_ReturnsBoolean() =
        Assert.AreEqual(typeof<Boolean>.ToRamlType(), "boolean", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_DateTime_ReturnsDate() =
        Assert.AreEqual(typeof<DateTime>.ToRamlType(), "date", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_string_ReturnsString() =
        Assert.AreEqual(typeof<string>.ToRamlType(), "string", "The return string should be 'integer'.")

    [<Test>]
    member x.TypeExtensions_object_ReturnsString() =
        Assert.AreEqual(typeof<Object>.ToRamlType(), "string", "The return string should be 'integer'.")

    [<Test>]
    member x.IsComplexModel_string_ReturnsFalse() =
        Assert.AreEqual(typeof<string>.IsComplexModel(), false, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_TypeExtensionsTest_ReturnsTrue() =
        Assert.AreEqual(typeof<Extension_Tests>.IsComplexModel(), true, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_NullableInt_ReturnsFalse() =
        let value = typeof<Nullable<int>>;
        Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_NullableLong_ReturnsFalse() =
        let value = typeof<Nullable<Int64>>;
        Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_NullableDateTime_ReturnsFalse() =
        let value = typeof<Nullable<int>>;
        Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_NullableBool_ReturnsFalse() =
        let value = typeof<Nullable<bool>>;
        Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")

    [<Test>]
    member x.IsComplexModel_NullableDecimal_ReturnsFalse() =
        let value = typeof<Nullable<decimal>>;
        Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.")