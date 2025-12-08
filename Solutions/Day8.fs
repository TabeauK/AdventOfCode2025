namespace Solutions

open System

module Day8 =
    type node =
        { name: string
          X: Int32
          Y: Int32
          Z: Int32 }

    type connection =
        { A: string
          B: string
          Xsquare: int
          distance: double }

    let distance A B =
        Math.Pow(A.X - B.X |> float, 2)
        + Math.Pow(A.Y - B.Y |> float, 2)
        + Math.Pow(A.Z - B.Z |> float, 2)
        |> Math.Sqrt


    let rec makeConnections nodes =
        match List.tryHead nodes with
        | None -> set []
        | Some head ->
            nodes.Tail
            |> List.fold
                (fun set node ->
                    Set.add
                        ({ A = head.name
                           B = node.name
                           Xsquare = head.X * node.X
                           distance = distance head node })
                        set)
                Set.empty
            |> Set.union (makeConnections nodes.Tail)

    let parse (inputs: List<string>) =
        let stopWatch: System.Diagnostics.Stopwatch =
            System.Diagnostics.Stopwatch.StartNew()

        let nodes =
            inputs
            |> List.map (fun x -> x.Split ",")
            |> List.zip [ 0 .. List.length inputs - 1 ]
            |> List.map (fun (name, numbers) ->
                { name = string name
                  X = Int32.Parse numbers[0]
                  Y = Int32.Parse numbers[1]
                  Z = Int32.Parse numbers[2] })

        let connections =
            nodes |> makeConnections |> Set.toList |> List.sortBy (fun x -> x.distance)

        let cliques = nodes |> List.map (fun x -> set [ x.name ])
        printfn "whhwihwdhcidwbc %s" (stopWatch.Elapsed.ToString())

        connections, cliques

    let take number (connections, cliques) =
        connections |> List.take number, cliques

    let rec joinCliques (connections: List<connection>, cliques: List<Set<string>>) =
        match List.tryHead connections with
        | None -> cliques, 0
        | Some head ->
            (let a = cliques |> List.findIndex (fun c -> c.Contains head.A)
             let setA = cliques[a]
             let updated = cliques |> List.removeAt a
             let b = updated |> List.tryFindIndex (fun c -> c.Contains head.B)

             match b with
             | None -> joinCliques (connections.Tail, cliques)
             | Some b ->
                 (let setB = updated[b]
                  let out = updated |> List.removeAt b |> List.append [ Set.union setA setB ]

                  match List.length out = 1 with
                  | true -> out, head.Xsquare
                  | false -> joinCliques (connections.Tail, out)))


    let resultA (cliques: List<Set<string>>, _) =
        cliques
        |> List.sortByDescending (fun x -> x.Count)
        |> List.take 3
        |> List.map (fun x -> x.Count)
        |> List.fold (fun x y -> x * y) 1

    let resultB (_: List<Set<string>>, X: int) = X

    let runConnections number = parse >> take number >> joinCliques

    let runAllConnections = parse >> joinCliques

    let solveA input =
        input |> runConnections 1000 |> resultA |> string

    let solveB input =
        input |> runAllConnections |> resultB |> string
