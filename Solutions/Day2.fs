namespace Solutions

open System

module Day2 =

    // Structs
    [<Struct>]
    type RangeOfPatterns =
        { min: string
          max: string
          length: int
          repetitions: int }

    [<Struct>]
    type Range = { min: string; max: string }

    // General helpers
    let SplitByDash (input: string) = input.Split '-'

    let IsDivisible nominator denominator = nominator % denominator = 0

    let Replicate replications number =
        number |> string |> String.replicate replications |> Int128.Parse

    let FullRange size =
        { min = "1" + String.replicate size "0"
          max = String.replicate size "9" }

    // Range constructors
    let Ranges values =
        match Array.length values = 2 with
        | false -> ArgumentException("Range is in wrong format") |> raise
        | _ -> ()

        let min = values[0]
        let max = values[1]

        let partialRanges =
            [ { min = min
                max = String.replicate (String.length min) "9" }
              { min = "1" + String.replicate (String.length max - 1) "0"
                max = max } ]

        match String.length max - String.length min with
        | value when value = 0 -> [ { min = min; max = max } ]
        | value when value = 1 -> partialRanges
        | value when value > 1 ->
            List.append ([ String.length min + 1 .. String.length max - 1 ] |> List.map FullRange) partialRanges
        | _ -> ArgumentException("Min bigger than Max") |> raise

    let ParseRanges (input: string) =
        input.Trim().Split ','
        |> Array.map SplitByDash
        |> Array.map Ranges
        |> Array.toList


    // RangeOfPatterns collection
    let UpperRangeOfPatterns range (length, repetition) =
        let pattern = range.max.Substring(0, length)
        let possibleMax = String.replicate repetition pattern

        match
            (Int128.Parse possibleMax >= Int128.Parse range.min)
            && (Int128.Parse possibleMax <= Int128.Parse range.max)
        with
        | false -> None
        | true ->
            Some
                { min = pattern
                  max = pattern
                  length = length
                  repetitions = repetition }

    let MiddleRangeOfPatterns range (length, repetition) =
        let max =
            range.max.Substring(0, length) |> Int128.Parse |> Int128.op_CheckedDecrement

        let min =
            range.min.Substring(0, length) |> Int128.Parse |> Int128.op_CheckedIncrement

        match min <= max with
        | false -> None
        | true ->
            Some
                { min = min |> string
                  max = max |> string
                  length = length
                  repetitions = repetition }

    let LowerRangeOfPatterns range (length, repetition) =
        let pattern = range.min.Substring(0, length)
        let possibleMin = String.replicate repetition pattern

        match
            (Int128.Parse possibleMin >= Int128.Parse range.min)
            && (Int128.Parse possibleMin <= Int128.Parse range.max)
        with
        | false -> None
        | true ->
            Some
                { min = pattern
                  max = pattern
                  length = length
                  repetitions = repetition }

    let CollectRanges range length =
        let maxLength = String.length range.max
        let patternSize = (length, maxLength / length)

        let collection =
            match MiddleRangeOfPatterns range patternSize with
            | None -> []
            | Some value -> [ value ]

        let partialCollection =
            match LowerRangeOfPatterns range patternSize with
            | None -> collection
            | Some value -> List.append collection [ value ]

        let fullCollection =
            match UpperRangeOfPatterns range patternSize with
            | None -> partialCollection
            | Some value -> List.append partialCollection [ value ]

        fullCollection

    let CollectAllRangesOfPatterns rangeLengths range =
        let Collect = CollectRanges range

        rangeLengths |> List.map Collect |> List.fold (@) List.empty

    // Operations on ranges
    let AllPatterns rangeOfPatterns =
        let Replicate = Replicate rangeOfPatterns.repetitions
        let min = Int128.Parse rangeOfPatterns.min
        let max = Int128.Parse rangeOfPatterns.max
        [ min..max ] |> List.map Replicate |> Set.ofList

    let FindPatternsInRange lenghts range =
        let RangesOfPatterns =
            range.max |> String.length |> lenghts |> CollectAllRangesOfPatterns

        range
        |> RangesOfPatterns
        |> List.map AllPatterns
        |> List.fold Set.union Set.empty

    let Run lenghts =
        let FindPatternInRange = lenghts |> FindPatternsInRange

        ParseRanges
        >> List.fold (@) List.empty
        >> List.map FindPatternInRange
        >> List.fold Set.union Set.empty
        >> Set.fold (+) Int128.Zero
        >> string


    // Part specific code
    let lengthsForA maxLength =
        match maxLength % 2 = 0 with
        | true -> [ maxLength / 2 ]
        | false -> []

    let lengthsForB maxLength =
        let IsDivisbleByMax = IsDivisible maxLength
        [ 1 .. maxLength - 1 ] |> List.filter IsDivisbleByMax

    let RunA = lengthsForA |> Run

    let RunB = lengthsForB |> Run

    // Solvers
    let solveA input = input |> Seq.head |> RunA

    let solveB input = input |> Seq.head |> RunB
