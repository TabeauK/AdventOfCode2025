namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay2() =

    let testInput =
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"

    [<TestMethod>]
    member this.TestRangesLength() =
        let input = "1-2,3-4,5-6"

        let out = input |> Day2.ParseRanges

        Assert.AreEqual(3, List.length out)

    [<TestMethod>]
    member this.TestRangesLength2() =
        let input = "1000000-100000000000"

        let out = input |> Day2.ParseRanges |> List.head

        Assert.AreEqual(6, List.length out)

    [<TestMethod>]
    member this.TestDay2A() =
        let out = [ testInput ] |> Day2.solveA

        Assert.AreEqual("1227775554", out)

    [<TestMethod>]
    member this.TestDay2B() =
        let out = [ testInput ] |> Day2.solveB

        Assert.AreEqual("4174379265", out)
