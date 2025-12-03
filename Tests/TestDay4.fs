namespace Tests

open System
open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay4() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay4A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day4.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay4B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day4.solveB input

        Assert.AreEqual("1", out)
