namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay9() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay9A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day9.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay9B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day9.solveB input

        Assert.AreEqual("1", out)
