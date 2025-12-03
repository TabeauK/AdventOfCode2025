namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay10() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay10A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day10.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay10B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day10.solveB input

        Assert.AreEqual("1", out)
