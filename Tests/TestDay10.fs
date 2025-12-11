namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay10() =


    let testInput =
        """
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7} 
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2} 
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5} 
       """

    [<TestMethod>]
    member this.TestDay10A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day10.solveA input

        Assert.AreEqual("7", out)

    [<TestMethod>]
    member this.TestDay10B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day10.solveB input

        Assert.AreEqual("33", out)
