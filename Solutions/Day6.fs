namespace Solutions

open System

module Day6 =
    type Accumulator =
        { current: Int128
          operator: Int128 * Int128 -> Int128 }

    let makeAccumulators =
        List.map (fun x ->
            match x with
            | '*' ->
                { current = Int128.One
                  operator = Int128.op_CheckedMultiply }
            | '+' ->
                { current = Int128.Zero
                  operator = Int128.op_CheckedAddition }
            | _ -> ArgumentException("unknown operator") |> raise)

    let applyAccumulator (accumulator, value: string) =
        { operator = accumulator.operator
          current = accumulator.operator (accumulator.current, (value.Trim() |> Int128.Parse)) }

    let getNumberFromAccumulator acc = acc.current

    let addPadding str = str + "  "

    let parse (inputs: List<string>) =
        let lastLine = inputs[List.length inputs - 1] |> addPadding
        let lastElement = String.length lastLine - 1

        let indexesWithoutEnd =
            [ 0..lastElement ] |> List.filter (fun x -> lastLine[x] <> ' ')

        let indexesWithEnd = List.append indexesWithoutEnd [ lastElement ]

        let acc = indexesWithoutEnd |> List.map (fun x -> lastLine[x]) |> makeAccumulators

        let data =
            inputs[.. List.length inputs - 2]
            |> List.map addPadding
            |> List.map (fun line ->
                (indexesWithEnd
                 |> List.pairwise
                 |> List.map (fun (start, finish) -> line[start .. finish - 2])))

        acc, data

    let runA (acc, data) =
        data
        |> List.fold (fun accumulators values -> (List.zip accumulators values |> List.map applyAccumulator)) acc
        |> List.map getNumberFromAccumulator
        |> List.sum

    let rec transpose =
        function
        | (_ :: _) :: _ as M -> List.map List.head M :: transpose (List.map List.tail M)
        | _ -> []

    let collectNumbers (data: List<string>) =
        data
        |> List.map (fun x -> x.ToCharArray() |> Array.toList) // List of chars
        |> List.map List.rev
        |> transpose
        |> List.map (List.fold (fun x y -> string x + string y) "")

    let runB (acc, data) =
        data
        |> transpose
        |> List.map collectNumbers
        |> List.zip acc
        |> List.map (fun (acc, list) -> list |> List.fold (fun x y -> applyAccumulator (x, y)) acc)
        |> List.map getNumberFromAccumulator
        |> List.sum

    let solveA input = input |> parse |> runA |> string

    let solveB input = input |> parse |> runB |> string
