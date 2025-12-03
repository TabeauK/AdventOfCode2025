namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay5() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay5A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day5.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay5B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day5.solveB input

        Assert.AreEqual("1", out)
