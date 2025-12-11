namespace Solutions

open System

// ABANDONED FOR BEING TOO SLOW
module Day10abandoned =
    type State = List<int>

    type AStarState =
        { queue: Set<(int * State)>
          gcost: Map<State, List<State>> } // min cost from start to state / history

    let apply node transformation =
        [ 0 .. List.length node - 1 ]
        |> List.map (fun (i) ->
            match List.contains i transformation with
            | false -> node[i]
            | true -> node[i] + 1)

    let isUnderLimit endState node = List.forall2 (>=) endState node

    let heuristic = List.fold2 (fun acc goal current -> acc + goal - current) 0

    let rec runAStar endState transformations AStarState =
        let (_, currentState) = Set.minElement AStarState.queue // O(1)
        let canBeConsidered = endState |> isUnderLimit

        let newStates = // O(transformations*transformations)
            transformations
            |> List.map (currentState |> apply)
            |> List.filter canBeConsidered
            |> List.distinct

        let betterCostStates = // O(transformations)
            newStates
            |> List.filter (fun state ->
                (let stateCost =
                    match AStarState.gcost.TryFind state with
                    | None -> Int32.MaxValue
                    | Some v -> v.Length

                 stateCost > List.length AStarState.gcost[currentState] + 1))

        let newGCost = // O(transformations)
            betterCostStates
            |> List.fold (fun gcost x -> Map.add x (x :: AStarState.gcost[currentState]) gcost) AStarState.gcost

        let newQueue = // O(bfsState.queue)
            AStarState.queue
            |> Set.filter (fun (_, x) -> List.contains x betterCostStates |> not)
            |> Set.remove (Set.minElement AStarState.queue)

        match newStates |> List.tryFind (fun x -> endState = x) with
        | Some _ -> List.length AStarState.gcost[currentState] + 1
        | None ->
            runAStar
                endState
                transformations
                { queue =
                    betterCostStates
                    |> List.map (fun x -> ((List.length newGCost[x]) + (heuristic endState x), x))
                    |> Set.ofList
                    |> Set.union newQueue
                  gcost = newGCost }

    let parse (input: string) =
        let entries = input.Split " " |> Array.toList

        let endStateRaw =
            entries |> List.find (fun x -> x[0] = '{' && x[String.length x - 1] = '}')

        let endState =
            endStateRaw.TrimStart('{').TrimEnd('}').Split ','
            |> Array.toList
            |> List.map Int32.Parse

        let transformations =
            entries
            |> List.filter (fun x -> x[0] = '(' && x[String.length x - 1] = ')')
            |> List.map (fun x -> x.TrimStart('(').TrimEnd(')'))
            |> List.map (fun x -> x.Split ',' |> Array.toList |> List.map Int32.Parse)

        (endState, transformations)

    let bfsStartState initValue len =
        let start = List.replicate len initValue

        { queue = set [ (0, start) ]
          gcost = Map.empty |> Map.add start List.empty }

    let runner (endState, transformations) =
        endState |> List.length |> bfsStartState 0 |> runAStar endState transformations

    let run inputs =
        inputs |> List.map parse |> List.map runner |> List.sum

