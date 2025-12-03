namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay7() =


    let testInput =
        """
       1
       """

    [<TestMethod>]
    member this.TestDay7A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day7.solveA input

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay7B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day7.solveB input

        Assert.AreEqual("1", out)
