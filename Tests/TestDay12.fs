namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay12() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay12A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day12.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay12B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day12.solveB input

        Assert.AreEqual("1", out)
