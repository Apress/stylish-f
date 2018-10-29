
   module Exercise11_01 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

       let passThroughRejects func input =
           match input with
           | Success x -> Success x
           | Failure f -> func f |> Failure

   module Exercise11_02 =

           open System

           type Message = 
               { FileName : string
                 Content : float[] }

           type Reading =
               { TimeStamp : DateTimeOffset
                 Data : float[] }

           let example =
               [|
                   { FileName = "2019-02-23T02:00:00-05:00"
                     Content = [|1.0; 2.0; 3.0; 4.0|] }
                   { FileName = "2019-02-23T02:00:10-05:00"
                     Content = [|5.0; 6.0; 7.0; 8.0|] }
                   { FileName = "error"
                     Content = [||] }
                   { FileName = "2019-02-23T02:00:20-05:00"
                     Content = [|1.0; 2.0; 3.0; Double.NaN|] }
               |]

           let log s = printfn "Logging: %s" s

           type MessageError =
               | InvalidFileName of fileName:string
               | DataContainsNaN of fileName:string * index:int

           let getReading message =
               match DateTimeOffset.TryParse(message.FileName) with
               | true, dt -> 
                   let reading = { TimeStamp = dt; Data = message.Content }
                   // DONE
                   Ok (message.FileName, reading)
               | false, _ -> 
                   // DONE
                   Error (InvalidFileName message.FileName)

           let validateData(fileName, reading) =
               let nanIndex = 
                   reading.Data
                   |> Array.tryFindIndex (Double.IsNaN)
               match nanIndex with
               | Some i -> 
                   // DONE
                   Error (DataContainsNaN(fileName, i))
               | None -> 
                   // DONE
                   Ok reading

           let logError (e : MessageError) =
               // DONE
               match e with
               | InvalidFileName fn ->
                   log (sprintf "Invalid file name: %s" fn)
               | DataContainsNaN (fn, i) ->
                   log (sprintf "Data contains NaN at position: %i in file: %s" i fn)

           open Result

           let processMessage =
               getReading
               >> bind validateData
               >> mapError logError

           let processData data =
               data
               |> Array.map processMessage
               |> Array.choose (fun result -> 
                   match result with
                   | Ok reading -> reading |> Some
                   | Error _ -> None)
       
           let demo() =
               example
               |> processData
               |> Array.iter (printfn "%A")
    