namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay3 () =

    
    let testInput = 
       """
       1
       """

    [<TestMethod>]
    member this.TestDay3A () =
        let inn = seq<string> (testInput.Split '\n')
        let out = Day1.solveA inn |> Seq.head
        Assert.IsTrue((out = "1"));

    [<TestMethod>]
    member this.TestDay3B () =
        let inn = seq<string> (testInput.Split '\n')
        let out = Day1.solveB inn |> Seq.head
        Assert.IsTrue((out = "1"));
