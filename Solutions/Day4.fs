namespace Solutions

module Day4 =

    let selectFirstT (first, _) = first

    let selectFirst (first, _, _) = first

    let selectSecond (_, second, _) = second

    let selectThird (_, _, third) = third

    let indexes table =
        [ 0 .. List.length table - 1 ]
        |> List.map (fun i -> [ 0 .. String.length table[i] - 1 ] |> List.map (fun j -> (i, j)))
        |> List.fold List.append List.empty

    let filetrMarkers (input: List<string>) (i, j) =
        match input[i][j] with
        | '@' -> true
        | _ -> false

    let countNeighbors (positions: Set<(int * int)>) (row, column) =
        [ (-1, -1); (0, -1); (1, -1); (-1, 0); (1, 0); (-1, 1); (0, 1); (1, 1) ]
        |> List.map (fun (rowDiff, columnDiff) -> (row + rowDiff, column + columnDiff))
        |> List.filter positions.Contains

    let allPositions input =
        let filter = input |> filetrMarkers
        input |> indexes |> List.filter filter |> Set.ofList

    let isRemovable positions current =
        let neighbors = countNeighbors positions current

        match List.length neighbors >= 4 with
        | true -> (false, Set.empty, current)
        | false -> (true, Set.ofList neighbors, current)

    let getRemovable positions valuesToCheck =
        let outcome =
            valuesToCheck |> Set.map (positions |> isRemovable) |> Set.filter selectFirst

        let toRemove = outcome |> Set.map selectThird

        let toCheck =
            outcome
            |> Set.map selectSecond
            |> Set.fold Set.union Set.empty
            |> Set.filter (toRemove.Contains >> not)

        (toRemove, toCheck)

    let getAllRemovable positions =
        getRemovable positions positions |> selectFirstT |> Set.count

    let rec remove counter valuesToCheck positions =
        let toRemove, toCheck = getRemovable positions valuesToCheck

        match Set.isEmpty toRemove with
        | true -> counter
        | false ->
            (let updatedCounter = counter + Set.count toRemove
             let updatedPositions = positions |> Set.filter (toRemove.Contains >> not)
             remove updatedCounter toCheck updatedPositions)

    let removeAll positions = remove 0 positions positions

    let solveA input =
        input |> allPositions |> getAllRemovable |> string

    let solveB input =
        input |> allPositions |> removeAll |> string
