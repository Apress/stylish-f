module Exercise10_01 =

    open System

    module Random =

        let private random = System.Random()

        let string() =
            let len = random.Next(0, 10)
            Array.init len (fun _ -> random.Next(0, 255) |> char)
            |> String

    module Server =

        let AsyncGetString (id : int) = 
            // id is unused
            async {
                do! Async.Sleep(500)
                return Random.string()
            }

    module Consumer =

        let AsyncGetData (count : int) =
            async {
                let! strings = 
                    Array.init count (fun i -> Server.AsyncGetString i)
                    |> Async.Parallel

                return
                    strings
                    |> Array.sort
            }

    let demo() =

        let sw = System.Diagnostics.Stopwatch()
        sw.Start()

        Consumer.AsyncGetData 10
        |> Async.RunSynchronously
        |> Array.iter (printfn "%s")

        printfn "That took %ims" sw.ElapsedMilliseconds

module Exercise10_02 =

    open System

    module Random =

        let private random = System.Random()

        let string() =
            let len = random.Next(0, 10)
            Array.init len (fun _ -> random.Next(0, 255) |> char)
            |> String

    module Server =

        let AsyncGetString (id : int) = 
            // id is unused
            async {
                do! Async.Sleep(500)
                return Random.string()
            }

    module Consumer =

        let GetDataAsync (count : int) =
            async {
                let! strings = 
                    Array.init count (fun i -> Server.AsyncGetString i)
                    |> Async.Parallel

                return
                    strings
                    |> Array.sort
            } |> Async.StartAsTask

    let demo() =

        let sw = System.Diagnostics.Stopwatch()
        sw.Start()

        Consumer.GetDataAsync 10
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> Array.iter (printfn "%s")

        printfn "That took %ims" sw.ElapsedMilliseconds