namespace Solutions

open System

open Microsoft.Z3

module Day10b =
    let augment endState transformation =
        [ 0 .. List.length endState - 1 ]
        |> List.map (fun i ->
            match List.contains i transformation with
            | true -> 1.0
            | false -> 0.0)

    let gauss (endState: List<int>, transformations) =
        use ctx = new Microsoft.Z3.Context()
        use opt = () |> ctx.MkOptimize

        let variables =
            List.init (Seq.length transformations) (fun x -> $"p{x}" |> ctx.MkIntConst :> ArithExpr)

        variables |> List.map (fun x -> ctx.MkGe(x, ctx.MkInt 0) |> opt.Add) |> ignore

        [ 0 .. List.length endState - 1 ]
        |> List.map (fun i ->
            (let vars =
                transformations
                |> List.zip variables
                |> List.filter (fun (_, t) -> List.contains i t)
                |> List.map (fun (c, _) -> c)

             if List.length vars > 0 then
                 ctx.MkEq(ctx.MkAdd vars, endState[i] |> ctx.MkInt) |> opt.Add))
        |> ignore

        variables |> ctx.MkAdd |> opt.MkMinimize |> ignore

        () |> opt.Check |> ignore

        variables |> List.sumBy (fun x -> opt.Model.Eval(x, true) :?> IntNum |> _.Int)

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

    let run inputs =
        inputs |> List.map parse |> List.map gauss |> List.sum


module Day10 =
    type State = List<bool>

    type Node = { currentState: State; counter: int }

    type BFSState =
        { queue: List<Node>
          visited: Set<State> }

    let apply node transformation =
        [ 0 .. List.length node - 1 ]
        |> List.map (fun (i) ->
            match List.contains i transformation with
            | false -> node[i]
            | true -> node[i] |> not)


    let isVisited visited node = visited |> Set.contains node

    let rec runBFS endState transformations bfsState =
        let node = List.head bfsState.queue
        let applyToNode = node.currentState |> apply
        let canBeConsidered = bfsState.visited |> isVisited >> not

        let newStates =
            transformations
            |> List.map applyToNode
            |> List.filter canBeConsidered
            |> List.distinct

        let newNodes =
            newStates
            |> List.map (fun (state) ->
                { counter = node.counter + 1
                  currentState = state })

        match newStates |> List.tryFind (fun x -> endState = x) with
        | Some _ -> node.counter + 1
        | None ->
            runBFS
                endState
                transformations
                { queue = newNodes |> List.append (bfsState.queue |> List.tail)
                  visited = newStates |> Set.ofList |> Set.union bfsState.visited }

    let parse (input: string) =
        let entries = input.Split " " |> Array.toList

        let endStateRaw =
            entries |> List.find (fun x -> x[0] = '[' && x[String.length x - 1] = ']')

        let endState =
            endStateRaw.TrimStart('[').TrimEnd(']').ToCharArray()
            |> Array.map (fun x -> x = '#')
            |> Array.toList

        let transformations =
            entries
            |> List.filter (fun x -> x[0] = '(' && x[String.length x - 1] = ')')
            |> List.map (fun x -> x.TrimStart('(').TrimEnd(')'))
            |> List.map (fun x -> x.Split ',' |> Array.toList |> List.map Int32.Parse)

        (endState, transformations)

    let bfsStartState len =
        let startState =
            { currentState = List.replicate len false
              counter = 0 }

        { queue = [ startState ]
          visited = set [] }

    let runner (endState, transformations) =
        endState |> List.length |> bfsStartState |> runBFS endState transformations

    let run inputs =
        inputs |> List.map parse |> List.map runner |> List.sum

    let solveA input = input |> run |> string

    let solveB input = input |> Day10b.run |> string

