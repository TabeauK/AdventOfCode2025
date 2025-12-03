namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay8() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay8A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day8.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay8B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day8.solveB input

        Assert.AreEqual("1", out)
