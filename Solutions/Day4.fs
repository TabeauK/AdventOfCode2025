namespace Solutions

open System

module Day4 =
    let ParseInt (s: string) =
        match Int32.TryParse s with
        | (true, x) -> x 
        | (false, _) -> 0

    let solveA input =
        seq<string> { input |> Seq.map ParseInt |> Seq.sum |> string}

    let solveB input =
        seq<string> { input |> Seq.map ParseInt |> Seq.sum |> string}
