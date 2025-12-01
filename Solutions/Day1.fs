namespace Solutions

open System

module Day1 =

    [<Struct>]
    type State =
        { position: int
          counter: int
          counterB: int }

    type Direction =
        | Right
        | Left

    let initialState =
        { position = 50
          counter = 0
          counterB = 0 }

    let newPosition old direction distance =
        match direction with
        | Right -> (old + distance) % 100
        | Left -> (old - (distance % 100) + 100) % 100

    let countZeros position =
        match position with
        | 0 -> 1
        | _ -> 0

    let countPastZeroes oldPosition newPosition direction =
        match (oldPosition = 0, newPosition = 0, direction) with
        | (true, _, _) -> 0
        | (_, true, _) -> 1
        | (false, false, Left) ->
            match newPosition > oldPosition with
            | true -> 1
            | false -> 0
        | (false, false, Right) ->
            match newPosition < oldPosition with
            | true -> 1
            | false -> 0

    let rotate state (inst: string) =
        let instruction = inst.Trim()

        let direction =
            match instruction[0] with
            | 'R' -> Right
            | 'L' -> Left
            | other -> raise (Exception($"incorrect direction {other}"))

        let distance =
            match Int32.TryParse instruction[1..] with
            | (true, number) -> number
            | (false, _) -> raise (ArgumentException("distance not a number"))

        let newPosition = newPosition state.position direction distance

        { position = newPosition
          counter = state.counter + countZeros newPosition
          counterB =
            state.counterB
            + distance / 100
            + countPastZeroes state.position newPosition direction }

    [<TailCall>]
    let rec runInstructions state (instructions: List<string>) =
        match List.tryHead instructions with
        | None -> state
        | Some head -> instructions |> List.tail |> (head |> string |> rotate state |> runInstructions)

    let getCounter state = string state.counter

    let getCounterB state = string state.counterB

    let run = initialState |> runInstructions

    let solveA input =
        seq { Seq.toList input |> run |> getCounter }

    let solveB input =
        seq { Seq.toList input |> run |> getCounterB }
