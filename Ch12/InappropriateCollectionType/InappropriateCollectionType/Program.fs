open System
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes

// From Listing 12-2:
module Harness = 

    [<MemoryDiagnoser>]
    type Harness() =
   
        [<Benchmark>]
        member __.Old() =
            Dummy.slowFunction()

        [<Benchmark>]
        member __.New() =
            Dummy.fastFunction()

// From Listing 12-6:
// module Harness = 

//    [<MemoryDiagnoser>]
//    type Harness() =

//        let r = Random()
//        let list = List.init 1_000_000 (fun _ -> r.NextDouble())
//        // From Listing 12-11:
//        let array = list |> Array.ofList
   
//        [<Benchmark>]
//        member __.Old() =
//            list
//            |> InappropriateCollectionType.Old.sample 1000 
//            |> ignore

//        [<Benchmark>]
//        member __.New() =
//            list
//            |> InappropriateCollectionType.New.sample 1000 
//            |> ignore

//        // From Listing 12-11
//        //[<Benchmark>]
//        //member __.New() =
//        //    array
//        //    |> InappropriateCollectionType.New.sample 1000 
//        //    |> ignore

//        // From Listing 12-14:
//        //[<Benchmark>]
//        //member __.New() =
//        //    list
//        //    |> InappropriateCollectionType.New.sample 1000 
//        //    |> Array.ofSeq
//        //    |> ignore

// From Listing 12-2:
[<EntryPoint>]
let main _ = 

    BenchmarkRunner.Run<Harness.Harness>() 
    |> printfn "%A"

    printfn "Press enter or click 'Stop' to exit"
    Console.ReadLine() |> ignore
    0