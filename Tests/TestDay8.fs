namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay8 () =

    
    let testInput = 
       """
       1
       """

    [<TestMethod>]
    member this.TestDay8A () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s) -> s.Trim() <> "")

        let out = Day8.solveA input |> Seq.head

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay8B () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s:string) -> s.Trim() <> "")

        let out = Day8.solveB input |> Seq.head

        Assert.AreEqual("1", out)
