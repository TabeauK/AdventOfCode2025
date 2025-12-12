namespace Solutions

open System

module Day12 =
    type shape =
        { area: int
          index: int }

    type region =
        { X: int
          Y: int
          requirments: Map<int, int> }

    let getChunck (inputs: List<string>) =
        let chunk = inputs |> List.takeWhile (fun y -> y.Trim() <> "")

        match (List.isEmpty inputs, List.length chunk = List.length inputs) with
        | (true, _) -> None
        | (false, true) -> Some(chunk, [])
        | (false, false) -> Some(chunk, inputs[List.length chunk + 1 ..])

    let parseShape (inputs: List<string>) =
        let index = inputs |> List.head |> _.Split(':') |> Array.head |> Int32.Parse

        let table =
            [ 0 .. List.length inputs.Tail - 1 ]
            |> List.map (fun row ->
                [ 0 .. String.length inputs.Tail[0] - 1 ]
                |> List.map (fun column -> (row, column)))
            |> List.fold (@) List.empty

        let vectors =
            table |> List.filter (fun (row, column) -> inputs.Tail[row][column] = '#')

        { area = List.length vectors
          index = index }

    let parseRegion (line: string) =
        let dimentions =
            line |> _.Split(':') |> Array.head |> _.Split('x') |> Array.map Int32.Parse

        let requirments =
            line
            |> _.Split(':')
            |> Array.tail
            |> Array.head
            |> _.Split(' ')
            |> Array.toList
            |> List.map (_.Trim())
            |> List.filter (fun x -> x <> "")
            |> List.map Int32.Parse

        { X = dimentions |> Array.head
          Y = dimentions |> Array.tail |> Array.head
          requirments = requirments |> List.zip [ 0 .. List.length requirments - 1 ] |> Map.ofList }

    let parse (inputs: List<string>) =
        let chunks = inputs |> List.unfold getChunck
        let shapes = chunks[0 .. List.length chunks - 2] |> List.map parseShape
        let regions = chunks |> List.last |> List.map parseRegion
        (shapes, regions)

    let canFit shapes region =
        let required = shapes |> List.map (fun x -> x.area * region.requirments[x.index]) |> List.fold (+) 0
        region.X * region.Y >= required

    let run inputs =
        let (shapes, regions) = inputs |> parse
        regions |> List.filter (canFit shapes) |> List.length

    let solveA input =
        input |> run|> string

    let solveB input =
        input |> run |> string
