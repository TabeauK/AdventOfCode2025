namespace Solutions

open System

module Day11 =
    let parse (input: string) =
        let v = input.Split ':' |> Array.head |> _.Trim()

        let n =
            input.Split ':'
            |> Array.tail
            |> Array.head
            |> _.Split(" ")
            |> Array.map _.Trim()
            |> Array.filter (fun s -> s <> "")
            |> Array.toList

        (v, n)

    let collect = List.fold (fun map (v, n) -> Map.add v n map) Map.empty

    let rec topologicalOrder visited node map =
        match (Set.contains node visited, (Map.containsKey node map |> not)) with
        | (true, _) -> ([], visited)
        | (false, true) -> ([ node ], visited |> Set.add node)
        | (false, false) ->
            (let newVisited = visited |> Set.add node

             let (newOrder, newVisits) =
                 map[node]
                 |> List.filter (fun x -> Set.contains x visited |> not)
                 |> List.fold
                     (fun (order, visits) neighbor ->
                         let (newOrder, newVisits) = topologicalOrder visits neighbor map
                         List.append newOrder order, newVisits)
                     ([], newVisited)

             (node :: newOrder, newVisits))

    let collectData start inputs =
        let connections = inputs |> List.map parse |> collect
        let (order, _) = connections |> topologicalOrder Set.empty start
        (connections, order)

    let findPaths from too (connections, order) =
        let newOrder =
            [ order |> List.findIndex ((=) from) .. List.length order - 1 ]
            |> List.map (fun i -> order[i])

        let cost =
            newOrder
            |> List.tail
            |> List.fold (fun cost v -> Map.add v Int128.Zero cost) (Map [ from, Int128.One ])

        let completedCosts =
            newOrder
            |> List.fold
                (fun cost v ->
                    match Map.tryFind v connections with
                    | Some neighbors -> neighbors |> List.fold (fun cost n -> Map.add n (cost[n] + cost[v]) cost) cost
                    | None -> cost)
                cost

        match Map.tryFind too completedCosts with
        | None -> Int128.Zero
        | Some value -> value

    let run (connections, order) =
        let find x y = findPaths x y (connections, order)

        find "svr" "fft" * find "fft" "dac" * find "dac" "out"
        + find "svr" "dac" * find "dac" "fft" * find "fft" "out"

    let solveA input =
        input |> collectData "you" |> findPaths "you" "out" |> string

    let solveB input = input |> collectData "svr" |> run |> string
