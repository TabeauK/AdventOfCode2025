namespace Tests

open Solutions
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDay10 () =

    
    let testInput = 
       """
       1
       """

    [<TestMethod>]
    member this.TestDay10A () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s) -> s.Trim() <> "")

        let out = Day10.solveA input |> Seq.head

        Assert.AreEqual("1", out)

    [<TestMethod>]
    member this.TestDay10B () =
        let inn = seq<string> (testInput.Split '\n')
        let input = inn |> Seq.filter (fun (s:string) -> s.Trim() <> "")

        let out = Day10.solveB input |> Seq.head

        Assert.AreEqual("1", out)
