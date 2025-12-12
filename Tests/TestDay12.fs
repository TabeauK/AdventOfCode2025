namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay12() =


    let testInput =
        """0:
###
##.
##.

1:
###
##.
.##

2:
.##
###
##.

3:
##.
###
##.

4:
###
#..
###

5:
###
.#.
###

4x4: 0 0 0 0 2 0
12x5: 1 0 1 0 2 2
12x5: 1 0 1 0 3 2"""

    [<TestMethod>]
    member this.TestDay12A() =
        let input =
            testInput.Split '\n'
            |> Array.toList

        let out = Day12.solveA input

        Assert.AreEqual("3", out)

    [<TestMethod>]
    member this.TestDay12B() =
        let input =
            testInput.Split '\n'
            |> Array.toList

        let out = Day12.solveB input

        Assert.AreEqual("3", out)
