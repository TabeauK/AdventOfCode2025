namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay12 () =

    
    let testInput = 
       """
       1
       """

    [<TestMethod>]
    member this.TestDay12A () =
        let inn = seq<string> (testInput.Split '\n')
        let out = Day1.solveA inn |> Seq.head
        Assert.IsTrue((out = "1"));

    [<TestMethod>]
    member this.TestDay12B () =
        let inn = seq<string> (testInput.Split '\n')
        let out = Day1.solveB inn |> Seq.head
        Assert.IsTrue((out = "1"));
