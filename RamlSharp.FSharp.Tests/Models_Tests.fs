namespace RamlSharp.FSharp.Tests
open System
open NUnit.Framework
open System.Diagnostics.CodeAnalysis

[<TestFixture>]
[<ExcludeFromCodeCoverage>]
type Models_Test() = 

    [<SetUp>]
    member x.Init() =
        ()

    [<Test>]
    member x.TestCase() =
        Assert.IsTrue(true)

