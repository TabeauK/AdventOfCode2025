namespace Tests

open System
open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay4 () =

    
    let testInput = 
       """
       1
       """

    [<TestMethod>]
    member this.TestDay4A () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s) -> s.Trim() <> "")

        let out = Day4.solveA input |> Seq.head

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay4B () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s:string) -> s.Trim() <> "")

        let out = Day4.solveB input |> Seq.head

        Assert.AreEqual("1", out)
