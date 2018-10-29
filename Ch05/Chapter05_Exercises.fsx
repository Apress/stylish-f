module Exercise05_02 =

    open System
        
    let extremes (s : seq<float>) =
        let mutable min = Double.MaxValue
        let mutable max = Double.MinValue
        for item in s do
            if item < min then
                min <- item
            if item > max then
                max <- item
        min, max

    // (-5.0, 11.1)
    [| 1.0; 2.3; 11.1; -5. |]    
    |> extremes

