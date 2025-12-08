namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay8() =


    let testInput =
        """
162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689
       """

    [<TestMethod>]
    member this.TestDay8RunA() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day8.runConnections 10 input |> (fun (x, y) -> x)

        Assert.AreEqual(11, List.length out)

    [<TestMethod>]
    member this.TestDay8A() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day8.runConnections 10 input |> Day8.resultA

        Assert.AreEqual(40, out)

    [<TestMethod>]
    member this.TestDay8B() =
        let input =
            testInput.Split '\n'
            |> Array.toList
            |> List.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day8.solveB input

        Assert.AreEqual("25272", out)
