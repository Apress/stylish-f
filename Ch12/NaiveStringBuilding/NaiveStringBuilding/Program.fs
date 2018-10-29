open System
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes

// From Listing 12-40:
module Harness = 

    [<MemoryDiagnoser>]
    type Harness() =

        let data =
            Array2D.init 500 500 (fun x y ->
                x * y |> float)

        [<Benchmark>]
        member __.Old() =
            data
            |> NaiveStringBuilding.Old.buildCsv
            |> ignore

        [<Benchmark>]
        member __.New() =
            data
            |> NaiveStringBuilding.New.buildCsv
            |> ignore


// From Listing 12-2:
[<EntryPoint>]
let main _ = 

    BenchmarkRunner.Run<Harness.Harness>() 
    |> printfn "%A"

    Console.ReadKey() |> ignore
    0