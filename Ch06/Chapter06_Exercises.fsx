module Exercise06_01 =

    open System

    type MeterValue =
    | Standard of int
    | Economy7 of Day:int * Night:int

    type MeterReading =
        { ReadingDate : DateTime
          MeterValue : MeterValue }

module Exercise06_02 =

    let fruits =
        [ "Apples", 3
          "Oranges", 4
          "Bananas", 2 ]

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    for (name, count) in fruits do
        printfn "There are %i %s" count name

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    fruits
    |> List.iter (fun (name, count) ->
        printfn "There are %i %s" count name)

module Exercise06_03 =

    open System
    open System.Text.RegularExpressions

    let zipCodes = [
        "90210"
        "94043"
        "94043-0138"
        "10013"
        "90210-3124"
        "1OO13" ]
        
    let (|USZipCode|_|) s =
        let m = Regex.Match(s, @"^(\d{5})$")
        if m.Success then
            USZipCode s |> Some
        else
            None

    let (|USZipPlus4Code|_|) s =
        raise <| NotImplementedException()
                
    zipCodes
    |> List.iter (fun z ->
        match z with
        | USZipCode c ->
            printfn "A normal zip code: %s" c
        | USZipPlus4Code(code, suffix) ->
            printfn "A Zip+4 code: prefix %s, suffix %s" code suffix
        | _ as n ->
            printfn "Not a zip code: %s" n)

