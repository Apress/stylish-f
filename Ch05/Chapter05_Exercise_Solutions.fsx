module Exercise05_01 =
        
    let clip ceiling (s : seq<_>) =
        s
        |> Seq.map (fun x -> min x ceiling)

    // seq [1.0; 2.3; 10.0; -5.0]
    [| 1.0; 2.3; 11.1; -5. |]    
    |> clip 10.

module Exercise05_02 =

    open System
        
    let extremes (s : seq<float>) =
        s |> Seq.max,
        s |> Seq.min

    // (-5.0, 11.1)
    [| 1.0; 2.3; 11.1; -5. |]    
    |> extremes

    // Performance test:
    open System.Diagnostics
        
    let r = System.Random()

    let big = Array.init 1_000_000 (fun _ -> r.NextDouble())
    let sw = Stopwatch()
    sw.Start()
    let min, max = big |> extremes
    sw.Stop()

    // 8ms
    printfn "min: %f max: %f - time: %ims" min max sw.ElapsedMilliseconds