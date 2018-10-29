module ShortTermObjects

// From Listings 12-21, 12-22:
type Point3d(x : float, y : float, z : float) =
    member __.X = x
    member __.Y = y
    member __.Z = z
    member val Description = "" with get, set
    member this.DistanceFrom(that : Point3d) =
        (that.X - this.X) ** 2. +
        (that.Y - this.Y) ** 2. +
        (that.Z - this.Z) ** 2.
        |> sqrt
    override this.ToString() =
        sprintf "X: %f, Y: %f, Z: %f" this.X this.Y this.Z

type Float3 = (float * float * float)

module Old =

    let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =
        let here = Point3d(here)
        coords
        |> Array.map Point3d
        |> Array.filter (fun there -> 
            there.DistanceFrom(here) <= radius)
        |> Array.map (fun p3d -> p3d.X, p3d.Y, p3d.Z)


module New =

    let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =
        let here = Point3d(here)
        coords
        |> Array.map Point3d
        |> Array.filter (fun there -> 
            there.DistanceFrom(here) <= radius)
        |> Array.map (fun p3d -> p3d.X, p3d.Y, p3d.Z)

// Method |     Mean |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
//------- |---------:|----------:|----------:|----------:|----------:|
//    Old | 268.9 ms | 8500.0000 | 4500.0000 | 1000.0000 |  53.56 MB |
//    New | 282.8 ms | 8500.0000 | 4500.0000 | 1000.0000 |  53.56 MB |

    // From Listing 12-25:
    //let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =
    //    let here = Point3d(here)
    //    coords
    //    |> Seq.map Point3d
    //    |> Seq.filter (fun there -> 
    //        there.DistanceFrom(here) <= radius)
    //    |> Seq.map (fun p3d -> p3d.X, p3d.Y, p3d.Z)
    //    |> Seq.toArray

// Method |     Mean |      Gen 0 |     Gen 1 | Allocated |
//------- |---------:|-----------:|----------:|----------:|
//    Old | 259.1 ms |  8000.0000 | 4000.0000 |  53.55 MB |
//    New | 213.7 ms | 22666.6667 |         - |  45.82 MB |

    // From Listing 12-27:
    //let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =

    //    let distance (p1 : float*float*float) (p2: float*float*float) =
    //        let x1, y1, z1 = p1
    //        let x2, y2, z2 = p2
    //        (x1 - x2) ** 2. +
    //        (y1 - y2) ** 2. +
    //        (z1 - z2) ** 2.
    //        |> sqrt

    //    coords
    //    |> Array.filter (fun there ->
    //        distance here there <= radius)

// Method |     Mean |     Gen 0 |     Gen 1 |   Allocated |
//------- |---------:|----------:|----------:|------------:|
//    Old | 238.7 ms | 8000.0000 | 4000.0000 | 54838.86 KB |
//    New | 151.0 ms |         - |         - |   126.33 KB |

    // From Listing 12-29:
    //let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =

    //    let distance x1 y1 z1 x2 y2 z2 =
    //        (x1 - x2) ** 2. +
    //        (y1 - y2) ** 2. +
    //        (z1 - z2) ** 2.
    //        |> sqrt

    //    let x1, y1, z1 = here

    //    coords
    //    |> Array.filter (fun (x2, y2, z2) ->
    //        distance x1 y1 z1 x2 y2 z2 <= radius)

// Method |     Mean |     Gen 0 |     Gen 1 |   Allocated |
//------- |---------:|----------:|----------:|------------:|
//    Old | 242.9 ms | 8000.0000 | 4000.0000 | 54838.86 KB |
//    New | 156.2 ms |         - |         - |   126.33 KB |

    // From Listing 12-31:
    //let withinRadius
    //    (radius : float) 
    //    (here : struct(float*float*float)) 
    //    (coords : struct(float*float*float)[]) =

    //    let distance p1 p2 =
    //        let struct(x1, y1, z1) = p1
    //        let struct(x2, y2, z2) = p2
    //        (x1 - x2) ** 2. +
    //        (y1 - y2) ** 2. +
    //        (z1 - z2) ** 2.
    //        |> sqrt

    //    coords
    //    |> Array.filter (fun there ->
    //        distance here there <= radius)

// Method |     Mean |     Gen 0 |     Gen 1 |    Gen 2 |   Allocated |
//------- |---------:|----------:|----------:|---------:|------------:|
//    Old | 287.7 ms | 8000.0000 | 3500.0000 | 500.0000 | 54838.87 KB |
//    New | 153.1 ms |         - |         - |        - |   134.64 KB |

    // From Listing 12-34:
    //let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =

    //    let distance x1 y1 z1 x2 y2 z2 =
    //        pown (x1 - x2) 2 +
    //        pown (y1 - y2) 2 +
    //        pown (z1 - z2) 2
    //        |> sqrt

    //    let x1, y1, z1 = here

    //    coords
    //    |> Array.filter (fun (x2, y2, z2) ->
    //        distance x1 y1 z1 x2 y2 z2 <= radius)

// Method |      Mean |     Gen 0 |     Gen 1 |     Gen 2 |   Allocated |
//------- |----------:|----------:|----------:|----------:|------------:|
//    Old | 281.60 ms | 8500.0000 | 4500.0000 | 1000.0000 | 54841.41 KB |
//    New |  16.08 ms |         - |         - |         - |   126.32 KB |

    // From Listing 12-37:
    //let withinRadius (radius : float) (here : Float3) (coords : Float3[]) =

    //    let distance x1 y1 z1 x2 y2 z2 =
    //        let dx = x1 - x2
    //        let dy = y1 - y2
    //        let dz = z1 - z2
    //        dx * dx +
    //        dy * dy +
    //        dz * dz
    //        |> sqrt

    //    let x1, y1, z1 = here

    //    coords
    //    |> Array.filter (fun (x2, y2, z2) ->
    //        distance x1 y1 z1 x2 y2 z2 <= radius)

// Method |       Mean |     Gen 0 |     Gen 1 |     Gen 2 |   Allocated |
//------- |-----------:|----------:|----------:|----------:|------------:|
//    Old | 270.396 ms | 8500.0000 | 4500.0000 | 1000.0000 | 54841.41 KB |
//    New |   5.555 ms |         - |         - |         - |   126.32 KB |



