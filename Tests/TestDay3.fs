namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay3() =

    let testInput =
        """
        987654321111111
        811111111111119
        234234234234278
        818181911112111
        """

    [<TestMethod>]
    member this.TestDay3A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day3.solveA input

        Assert.AreEqual("357", out)

    [<TestMethod>]
    member this.TestDay3B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day3.solveB input

        Assert.AreEqual("3121910778619", out)
