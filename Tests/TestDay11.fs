namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay11() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay11A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day11.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay11B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day11.solveB input

        Assert.AreEqual("1", out)
