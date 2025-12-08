namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay6() =


    let testInput =
        """123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  """

    [<TestMethod>]
    member this.TestDay6A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day6.solveA input

        Assert.AreEqual("4277556", out)

    [<TestMethod>]
    member this.TestDay6B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day6.solveB input

        Assert.AreEqual("3263827", out)
