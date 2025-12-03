namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay6() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay6A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day6.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay6B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day6.solveB input

        Assert.AreEqual("1", out)
