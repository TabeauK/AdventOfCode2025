namespace Solutions

open System

module Day5 =

    type Branch =
        { maxLeft: Int128
          minRight: Int128
          left: Node
          right: Node }

    and Leaf = { min: Int128; max: Int128 }

    and Node =
        | Branch of Branch
        | Leaf of Leaf

    let insertToLeaf (node: Leaf) (min, max) : Node =
        match (node.min > max, node.max < min, node.max >= max, node.min <= min) with
        | (false, false, true, true) -> Leaf node // subset
        | (false, false, false, true) -> Leaf { min = node.min; max = max } // right extension
        | (false, false, true, false) -> Leaf { min = min; max = node.max } // left extension
        | (false, false, false, false) -> Leaf { min = min; max = max } // bigger set
        | (true, _, _, _) -> // smaller range
            Branch
                { minRight = node.min
                  maxLeft = max
                  left = Leaf { min = min; max = max }
                  right = Leaf node }
        | (_, true, _, _) -> // bigger range
            Branch
                { minRight = min
                  maxLeft = node.max
                  left = Leaf node
                  right = Leaf { min = min; max = max } }

    let rec insertToBranch (node: Branch) (min, max) : Node =
        match (node.minRight > max, node.maxLeft > max, node.maxLeft < min, node.minRight < min) with
        | (true, false, _, _) ->
            Branch
                { minRight = node.minRight
                  maxLeft = max
                  left = insert node.left (min, max)
                  right = node.right }
        | (true, true, _, _) ->
            Branch
                { minRight = node.minRight
                  maxLeft = node.maxLeft
                  left = insert node.left (min, max)
                  right = node.right }
        | (_, _, true, false) ->
            Branch
                { minRight = min
                  maxLeft = node.maxLeft
                  left = node.left
                  right = insert node.right (min, max) }
        | (_, _, true, true) ->
            Branch
                { minRight = node.minRight
                  maxLeft = node.maxLeft
                  left = node.left
                  right = insert node.right (min, max) }
        | (false, _, false, _) ->
            Branch
                { minRight = node.minRight
                  maxLeft = Int128.op_CheckedDecrement node.minRight
                  left = insert node.left (min, (Int128.op_CheckedDecrement node.minRight))
                  right = insert node.right (node.minRight, max) }

    and insert node (min, max) =
        match max >= min with
        | false -> node
        | true ->
            match node with
            | Leaf leaf -> insertToLeaf leaf (min, max)
            | Branch branch -> insertToBranch branch (min, max)

    let rec check node value =
        match node with
        | Leaf leaf -> value >= leaf.min && value <= leaf.max
        | Branch branch ->
            match (branch.minRight <= value, branch.maxLeft >= value) with
            | (false, false) -> false
            | (true, _) -> check branch.right value
            | (_, true) -> check branch.left value

    let splitByDash (input: String) =
        let s = input.Trim().Split "-"
        Int128.Parse(s[0]), Int128.Parse(s[1])

    let parse input =
        let splitIndex = input |> List.findIndex (fun (x: string) -> x.Trim() = "")
        let emptyTree = Leaf { min = Int128.Zero; max = Int128.Zero }

        let tree =
            input[.. splitIndex - 1] |> List.map splitByDash |> List.fold insert emptyTree

        let values =
            input[splitIndex + 1 ..]
            |> List.map (fun x -> x.Trim())
            |> List.map Int128.Parse

        (tree, values)

    let rec countAllRanges node =
        match node with
        | Leaf leaf -> leaf.max - leaf.min + Int128.One
        | Branch branch -> countAllRanges branch.left + countAllRanges branch.right

    let selectFirst (x, _) = x

    let run (node, values) =
        let checker = node |> check
        values |> List.filter checker |> List.distinct |> List.length

    let solveA input = input |> parse |> run |> string

    let solveB input =
        input
        |> parse
        |> selectFirst
        |> countAllRanges
        |> Int128.op_CheckedDecrement
        |> string
