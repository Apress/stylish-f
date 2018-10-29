module Exercise09_01 =

    let add a b = a + b

    let applyAndPrint f a b =
        let r = f a b
        printfn "%i" r

    // "5"
    applyAndPrint add 2 3

module Exercise09_02 =

    let counter start =
        let mutable current = start
        fun () -> 
            let this = current
            current <- current + 1
            this

    let demo() =

        let c1 = counter 0
        let c2 = counter 100

        for _ in 0..4 do
            printfn "c1: %i" (c1())
            printfn "c2: %i" (c2())

module Exercise09_03 =

    let featureScale a b xMin xMax x =
        a + ((x - xMin) * (b - a)) / (xMax - xMin)

    let scale (data : seq<float>) =
        let minX = data |> Seq.min
        let maxX = data |> Seq.max
        // let zeroOneScale = ...
        data
        |> Seq.map (fun x -> featureScale 0. 1. minX maxX x)
        // |> Seq.map zeroOneScale

    // seq [0.0; 0.5; 1.0]
    let demo() =
        [100.; 150.; 200.]
        |> scale

module Exercise09_04 =

    let pipeline =
        [ fun x -> x * 2.
          fun x -> x * x
          fun x -> x - 99.9 ]

    let applyAll (p : (float -> float) list) =
        // Replace this:
        raise <| System.NotImplementedException()

    let demo() =
        let x = 100. |> applyAll pipeline
        // 39900.1
        printfn "%f" x
    
