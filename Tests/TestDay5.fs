namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay5() =


    let testInput =
        """3-5
        10-14
        16-20
        12-18

        1
        5
        8
        11
        16
        32"""

    [<TestMethod>]
    member this.TestDay5A() =
        let input = testInput.Split '\n' |> Array.toList

        let out = Day5.solveA input

        Assert.AreEqual("3", out)

    [<TestMethod>]
    member this.TestDay5B() =
        let input = testInput.Split '\n' |> Array.toList

        let out = Day5.solveB input

        Assert.AreEqual("14", out) // 353524850827629 TOO LOW
