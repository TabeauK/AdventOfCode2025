namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay9() =


    let testInput =
        """
7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3
       """

    [<TestMethod>]
    member this.TestDay9A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day9.solveA input

        Assert.AreEqual("50", out)

    [<TestMethod>]
    member this.TestDay9B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day9.solveB input

        Assert.AreEqual("24", out) // 1416375744 too low
