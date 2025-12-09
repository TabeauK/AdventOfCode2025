namespace Solutions

open System

module Day9 =
    type tile = { X: int; Y: int }

    let makeTiles (list: List<string>) =
        { X = list[0].Trim() |> Int32.Parse
          Y = list[1].Trim() |> Int32.Parse }

    let tiles = List.map (fun (x: string) -> x.Split ',' |> Array.toList |> makeTiles)

    type rectangle = { A: tile; C: tile; area: Int64 }

    let rec makeRectangles tiles =
        match List.tryHead tiles with
        | None -> set []
        | Some head ->
            tiles.Tail
            |> List.fold
                (fun set tile ->
                    Set.add
                        ({ A = head
                           C = tile
                           area = Int32.BigMul(1 + Math.Abs(head.X - tile.X), 1 + Math.Abs(head.Y - tile.Y)) })
                        set)
                Set.empty
            |> Set.union (makeRectangles tiles.Tail)

    let rectangles =
        makeRectangles >> Set.toList >> List.sortByDescending (fun x -> x.area)

    let maxArea (rectangles: List<rectangle>) = rectangles[0].area

    // lines
    let makeLines (tiles: List<tile>) =
        let pairs =
            tiles
            |> List.pairwise
            |> List.append [ (tiles[List.length tiles - 1], tiles.Head) ]

        let verticals =
            pairs
            |> List.filter (fun ((first, second)) -> first.X = second.X)
            |> List.map (fun ((first, second)) -> (first.X, min first.Y second.Y, max first.Y second.Y))

        let horizontals =
            pairs
            |> List.filter (fun ((first, second)) -> first.Y = second.Y)
            |> List.map (fun ((first, second)) -> (first.Y, min first.X second.X, max first.X second.X))

        (verticals, horizontals)

    let checkLines (verticals: List<int * int * int>, horizontals: List<int * int * int>) rectangle =
        let maxX = max rectangle.A.X rectangle.C.X
        let minX = min rectangle.A.X rectangle.C.X
        let maxY = max rectangle.A.Y rectangle.C.Y
        let minY = min rectangle.A.Y rectangle.C.Y

        let maxYlines =
            horizontals
            |> List.filter (fun (y, min, max) -> y < maxY && minX < max && maxX > min)

        let minYlines =
            horizontals
            |> List.filter (fun (y, min, max) -> y <= minY && minX < max && maxX > min)

        let maxXlines =
            verticals
            |> List.filter (fun (x, min, max) -> x < maxX && minY < max && maxY > min)

        let minXlines =
            verticals
            |> List.filter (fun (x, min, max) -> x <= minX && minY < max && maxY > min)

        maxYlines = minYlines && maxXlines = minXlines

    let run inputs =
        let tiles = inputs |> tiles
        let rectangles = tiles |> rectangles
        let checker = tiles |> makeLines |> checkLines
        let max = rectangles |> List.find checker
        max.area

    let solveA input =
        input |> tiles |> rectangles |> maxArea |> string

    let solveB input = input |> run |> string
