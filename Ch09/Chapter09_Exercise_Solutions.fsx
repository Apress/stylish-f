module Exercise09_01 =

    let add a b = a + b
    let multiply a b = a * b

    let applyAndPrint f a b =
        let r = f a b
        printfn "%i" r

    // "5"
    applyAndPrint add 2 3
    // "6"
    applyAndPrint multiply 2 3
    // "-1"
    applyAndPrint (fun x y -> x - y) 2 3
    // "-1"
    applyAndPrint (-) 2 3

module Exercise09_02 =

    let rangeCounter first last =
        let mutable current = first
        fun () ->
            let this = current
            let next = current + 1
            current <-
                if next <= last then
                    next
                else
                    first
            this

    // r1: 3 r2: 6
    // r1: 4 r2: 7
    // r1: 5 r2: 8
    // r1: 6 r2: 9
    // r1: 3 r2: 10
    // r1: 4 r2: 11
    // ...
    // r1: 3 r2: 8
    let demo() =
        let r1 = rangeCounter 3 6
        let r2 = rangeCounter 6 11

        for _ in 0..20 do
            printfn "r1: %i r2: %i" (r1()) (r2())

module Exercise09_03 =

    let featureScale a b xMin xMax x =
        a + ((x - xMin) * (b - a)) / (xMax - xMin)

    let scale (data : seq<float>) =
        let minX = data |> Seq.min
        let maxX = data |> Seq.max
        let zeroOneScale = featureScale 0. 1. minX maxX
        data
        |> Seq.map zeroOneScale

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
        p |> List.reduce (>>)

    // Alternatively:
    // let applyAll =
    //     List.reduce (>>)

    let demo() =
        let x = 100. |> applyAll pipeline
        // 39900.1
        printfn "%f" x




                


