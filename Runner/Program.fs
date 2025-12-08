// For more information see https://aka.ms/fsharp-console-apps
open Solutions
open System.Diagnostics
open System.IO

let start part path =
    let stopWatch = Stopwatch.StartNew()
    let content = $"inputs/{path}.txt" |> File.ReadLines
    printfn "Starting execution of %s part %s" path part
    content |> Seq.toList, stopWatch

let solve solver (content, stopWatch) = (solver content), stopWatch

let finish (output, stopWatch: Stopwatch) =
    printfn "The result is: %s" output
    printfn "The solution was calculated in %s" (stopWatch.Elapsed.ToString())


let bigSeparator () =
    printfn "-----------------------------------------------------------------"
    printfn ""
    printfn ""

let smallSeparator () =
    printfn "--------------------------------------"

[<EntryPoint>]
let main (argv) =
    let stopWatch: System.Diagnostics.Stopwatch =
        System.Diagnostics.Stopwatch.StartNew()

    printfn "Starting execution of Advent Of Code 2025"
    printfn "-----------------------------------------------------------------"
    printfn ""
    //"Day1" |> start "a" |> solve Day1.solveA |> finish |> smallSeparator |> ignore
    //"Day1" |> start "b" |> solve Day1.solveB |> finish |> bigSeparator |> ignore
    "Day2" |> start "a" |> solve Day2.solveA |> finish |> smallSeparator |> ignore
    "Day2" |> start "b" |> solve Day2.solveB |> finish |> bigSeparator |> ignore
    "Day3" |> start "a" |> solve Day3.solveA |> finish |> smallSeparator |> ignore
    "Day3" |> start "b" |> solve Day3.solveB |> finish |> bigSeparator |> ignore
    "Day4" |> start "a" |> solve Day4.solveA |> finish |> smallSeparator |> ignore
    "Day4" |> start "b" |> solve Day4.solveB |> finish |> bigSeparator |> ignore
    "Day5" |> start "a" |> solve Day5.solveA |> finish |> smallSeparator |> ignore
    "Day5" |> start "b" |> solve Day5.solveB |> finish |> bigSeparator |> ignore
    "Day6" |> start "a" |> solve Day6.solveA |> finish |> smallSeparator |> ignore
    "Day6" |> start "b" |> solve Day6.solveB |> finish |> bigSeparator |> ignore
    "Day7" |> start "a" |> solve Day7.solveA |> finish |> smallSeparator |> ignore
    "Day7" |> start "b" |> solve Day7.solveB |> finish |> bigSeparator |> ignore
    "Day8" |> start "a" |> solve Day8.solveA |> finish |> smallSeparator |> ignore
    "Day8" |> start "b" |> solve Day8.solveB |> finish |> bigSeparator |> ignore
    "Day9" |> start "a" |> solve Day9.solveA |> finish |> smallSeparator |> ignore
    "Day9" |> start "b" |> solve Day9.solveB |> finish |> bigSeparator |> ignore
    "Day10" |> start "a" |> solve Day10.solveA |> finish |> smallSeparator |> ignore
    "Day10" |> start "b" |> solve Day10.solveB |> finish |> bigSeparator |> ignore
    "Day11" |> start "a" |> solve Day11.solveA |> finish |> smallSeparator |> ignore
    "Day11" |> start "b" |> solve Day11.solveB |> finish |> bigSeparator |> ignore
    "Day12" |> start "a" |> solve Day12.solveA |> finish |> smallSeparator |> ignore
    "Day12" |> start "b" |> solve Day12.solveB |> finish |> bigSeparator |> ignore
    printfn "The whole execution was calculated in %s" (stopWatch.Elapsed.ToString())
    0
