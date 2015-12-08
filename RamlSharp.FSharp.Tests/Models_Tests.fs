namespace RamlSharp.FSharp.Tests
open System
open NUnit.Framework

[<TestFixture>]
type Models_Test() = 

    [<SetUp>]
    member x.Init() =
        ()

    [<Test>]
    member x.TestCase() =
        Assert.IsTrue(true)

