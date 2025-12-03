namespace Solutions

open System

module Day7 =
    let ParseInt (s: string) =
        match Int32.TryParse s with
        | (true, x) -> x 
        | (false, _) -> 0

    let solveA input =
        input |> List.map ParseInt |> List.sum |> string

    let solveB input =
        input |> List.map ParseInt |> List.sum |> string
