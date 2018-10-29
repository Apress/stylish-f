
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
               // TODO Return an OK result containing a tuple of the 
               // message file name and the reading:
               raise <| NotImplementedException()
           | false, _ -> 
               // TODO Return an Error result containing an
               // InvalidFileName error, which itself contains
               // the message file name:
               raise <| NotImplementedException()

       let validateData(fileName, reading) =
           let nanIndex = 
               reading.Data
               |> Array.tryFindIndex (Double.IsNaN)
           match nanIndex with
           | Some i -> 
               // TODO Return an Error result containing an
               // DataContainsNaN error, which itself contains
               // the file name and error index:
               raise <| NotImplementedException()
           | None -> 
               // TODO Return an Ok result containing the reading:
               raise <| NotImplementedException()

       let logError (e : MessageError) =
           // TODO match on the MessageError cases
           // and call log with suitable information
           // for each case.
           raise <| NotImplementedException()

       // When all the TODOs are done, uncomment this code
       // and see if it works!

       //open Result

       //let processMessage =
       //    getReading
       //    >> bind validateData
       //    >> mapError logError

       //let processData data =
       //    data
       //    |> Array.map processMessage
       //    |> Array.choose (fun result -> 
       //        match result with
       //        | Ok reading -> reading |> Some
       //        | Error _ -> None)
       
       //let demo() =
       //    example
       //    |> processData
       //    |> Array.iter (printfn "%A")
