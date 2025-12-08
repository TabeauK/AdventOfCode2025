namespace Solutions

open System

module Day7 =
    type beam =
        { row: int
          column: int
          possibilities: Int128 }

    let indexes table =
        [ 0 .. List.length table - 1 ]
        |> List.map (fun i -> [ 0 .. String.length table[i] - 1 ] |> List.map (fun j -> (i, j)))
        |> List.fold List.append List.empty

    let parse inputs =
        let start = inputs |> indexes |> List.find (fun (i, j) -> inputs[i][j] = 'S')

        let splitters =
            inputs
            |> indexes
            |> List.filter (fun (i, j) -> inputs[i][j] = '^')
            |> Set.ofList

        (start, splitters, List.length inputs)


    let mergeBeams (beams: List<beam>) =
        beams
        |> List.groupBy (fun x -> (x.row, x.column))
        |> List.map (fun ((row, column), beams) ->
            { row = row
              column = column
              possibilities = (beams |> List.fold (fun acc beam -> beam.possibilities + acc) 0) })

    let rec goOneRow splitters (beams: List<beam>) =
        let newRow =
            beams
            |> List.map (fun beam ->
                (match Set.contains (beam.row + 1, beam.column) splitters with
                 | false ->
                     [ { row = beam.row + 1
                         column = beam.column
                         possibilities = beam.possibilities } ]
                 | true ->
                     [ { row = beam.row + 1
                         column = beam.column + 1
                         possibilities = beam.possibilities }
                       { row = beam.row + 1
                         column = beam.column - 1
                         possibilities = beam.possibilities } ]))
            |> List.fold List.append List.empty

        let splits = List.length newRow - List.length beams
        (mergeBeams newRow, splits)

    and goManyRowsA len splitters (beams: List<beam>, counter) =
        match len with
        | 0 -> counter
        | _ -> (goOneRow splitters beams |> goManyRowsA (len - 1) splitters) + counter

    and goManyRowsB len splitters (beams: List<beam>, counter) =
        match len with
        | 0 -> beams |> List.map (fun b -> b.possibilities) |> List.sum
        | _ -> goOneRow splitters beams |> goManyRowsB (len - 1) splitters

    let run goManyRows ((startRow, startColumn), splitters, len) =
        let beams =
            [ { row = startRow
                column = startColumn
                possibilities = Int128.One } ]

        goManyRows len splitters (beams, 0)

    let solveA input =
        input |> parse |> run goManyRowsA |> string

    let solveB input =
        input |> parse |> run goManyRowsB |> string
