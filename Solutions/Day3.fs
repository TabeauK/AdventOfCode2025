namespace Solutions

open System

module Day3 =

    [<Struct>]
    type Place = { index: int; value: char }

    let concatenateTuple (x, y) = string x.value + string y.value

    let rec Get2Biggest s offset (first, second) =
        let i = offset + 1

        match List.tryHead s with
        | None -> (first, second)
        | Some current ->
            Get2Biggest
                (List.tail s)
                i
                (match (current > first.value, current > second.value, s |> List.tail |> List.isEmpty) with
                 | (true, _, false) ->
                     ({ index = i; value = current },
                      { index = i + 1
                        value = s |> List.tail |> List.head })
                 | (true, true, true) -> (first, { index = i; value = current })
                 | (true, false, true) -> (first, second)
                 | (false, true, _) -> (first, { index = i; value = current })
                 | (false, false, _) -> (first, second))

    let InitRecursion (input: string) =
        let s = input.Trim()
        let first = { index = 0; value = s[0] }
        let second = { index = 1; value = s[1] }
        Get2Biggest (s[1..].ToCharArray() |> Array.toList) 0 (first, second)

    let FindBiggest12 (s: string) =
        let input = s.Trim()
        let (first, second) = InitRecursion(input.Substring(0, String.length input - 10))

        let firstoffest = second.index + 1

        let (third, forth) =
            InitRecursion(input.Substring(firstoffest, String.length input - 8 - firstoffest))

        let secondoffest = firstoffest + forth.index + 1

        let (fifth, sixth) =
            InitRecursion(input.Substring(secondoffest, String.length input - 6 - secondoffest))

        let thirdoffest = secondoffest + sixth.index + 1

        let (seventh, eighth) =
            InitRecursion(input.Substring(thirdoffest, String.length input - 4 - thirdoffest))

        let forthoffest = thirdoffest + eighth.index + 1

        let (nineth, tenth) =
            InitRecursion(input.Substring(forthoffest, String.length input - 2 - forthoffest))

        let fifthoffest = forthoffest + tenth.index + 1

        let (eleventh, twelveth) =
            InitRecursion(input.Substring(fifthoffest, String.length input - fifthoffest))

        concatenateTuple (first, second)
        + concatenateTuple (third, forth)
        + concatenateTuple (fifth, sixth)
        + concatenateTuple (seventh, eighth)
        + concatenateTuple (nineth, tenth)
        + concatenateTuple (eleventh, twelveth)

    let solveA input =
        input
        |> List.map InitRecursion
        |> List.map concatenateTuple
        |> List.map Int32.Parse
        |> List.sum
        |> string


    let solveB input =
        input |> List.map FindBiggest12 |> List.map Int128.Parse |> List.sum |> string
