namespace Tests

open System
open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay1() =


    let testInput =
        """
        L68
        L30
        R48
        L5
        R60
        L55
        L1
        L99
        R14
        L82
       """

    [<TestMethod>]
    member this.TestRotateLeft() =
        let instruction = "L10"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(40, newState.position)

    [<TestMethod>]
    member this.TestRotateRight() =
        let instruction = "R10"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(60, newState.position)

    [<TestMethod>]
    member this.TestRotateToZero() =
        let instruction = "L10"

        let state: Day1.State =
            { position = 10
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(0, newState.position)
        Assert.AreEqual(1, newState.counter)

    [<TestMethod>]
    member this.TestRotatePast0FromRight() =
        let instruction = "L1"

        let state: Day1.State =
            { position = 0
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(99, newState.position)

    [<TestMethod>]
    member this.TestRotatePast0FromLeft() =
        let instruction = "R1"

        let state: Day1.State =
            { position = 99
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(0, newState.position)

    [<TestMethod>]
    member this.TestRotateIncorrectDirection() =
        let instruction = "A1"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        Assert.Throws(fun () -> instruction |> Day1.rotate state |> ignore) |> ignore

    [<TestMethod>]
    member this.TestRotateIncorrectDistance() =
        let instruction = "Lone"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        Assert.Throws(fun () -> instruction |> Day1.rotate state |> ignore) |> ignore

    [<TestMethod>]
    member this.TestRotatePast0ByBigNumber() =
        let instruction = "R100000000"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(50, newState.position)
        Assert.AreEqual(1000000, newState.counterB)

    [<TestMethod>]
    member this.TestRotateByBigNumberAndLandOnZero() =
        let instruction = "R100000050"

        let state: Day1.State =
            { position = 50
              counter = 0
              counterB = 0 }

        let newState: Day1.State = Day1.rotate state instruction

        Assert.AreEqual(0, newState.position)
        Assert.AreEqual(1, newState.counter)
        Assert.AreEqual(1000001, newState.counterB)

    [<TestMethod>]
    member this.TestDay1A() =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s) -> s.Trim() <> "")

        let out = Day1.solveA input |> Seq.head

        Assert.AreEqual("3", out)

    [<TestMethod>]
    member this.TestDay1B() =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s: string) -> s.Trim() <> "")

        let out = Day1.solveB input |> Seq.head

        Assert.AreEqual("6", out)
