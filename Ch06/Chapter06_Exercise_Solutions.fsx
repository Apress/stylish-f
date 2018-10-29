module Exercise06_01_Pass1 =

    open System

    type MeterValue =
    | Standard of int
    | Economy7 of Day:int * Night:int

    type MeterReading =
        { ReadingDate : DateTime
          MeterValue : MeterValue }

    let formatReading (reading : MeterReading) =
        match reading with
        | { ReadingDate = readingDate
            MeterValue = Standard reading } ->
            sprintf "Your reading on: %s was %07i"
                (readingDate.ToShortDateString()) reading
        | { ReadingDate = readingDate
            MeterValue = Economy7(Day=day; Night=night) } ->
            sprintf "Your readings on: %s were Day: %07i Night: %07i"
                (readingDate.ToShortDateString()) day night

    let reading1 = { ReadingDate = DateTime(2019, 3, 23)
                     MeterValue = Standard 12982 }

    let reading2 = { ReadingDate = DateTime(2019, 2, 24)
                     MeterValue = Economy7(Day=3432, Night=98218) }

    reading1 |> formatReading
    reading2 |> formatReading

module Exercise06_01_Pass2 =

    open System

    type MeterValue =
    | Standard of int
    | Economy7 of Day:int * Night:int

    type MeterReading =
        { ReadingDate : DateTime
          MeterValue : MeterValue }

    let formatReading { ReadingDate = date; MeterValue = meterValue }  = 
        let dateString = date.ToShortDateString() 
        match meterValue with 
        | Standard reading -> 
            sprintf "Your reading on: %s was %07i" 
                dateString reading 
        | Economy7(Day=day; Night=night) -> 
            sprintf "Your readings on: %s were Day: %07i Night: %07i" 
                dateString day night

    let reading1 = { ReadingDate = DateTime(2019, 3, 23)
                     MeterValue = Standard 12982 }

    let reading2 = { ReadingDate = DateTime(2019, 2, 24)
                     MeterValue = Economy7(Day=3432, Night=98218) }

    reading1 |> formatReading
    reading2 |> formatReading

module Exercise06_02 =

    type FruitBatch = {
        Name : string
        Count : int }

    let fruits =
        [ { Name="Apples"; Count=3 }
          { Name="Oranges"; Count=4 }
          { Name="Bananas"; Count=2 } ]

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    for { Name=name; Count=count } in fruits do
        printfn "There are %i %s" count name

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    fruits
    |> List.iter (fun { Name=name; Count=count } ->
        printfn "There are %i %s" count name)

module Exercise06_03 =

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
        let m = Regex.Match(s, @"^(\d{5})\-(\d{4})$")
        if m.Success then
            USZipPlus4Code(m.Groups.[1].Value, 
                           m.Groups.[2].Value)
            |> Some
        else
            None
                
    zipCodes
    |> List.iter (fun z ->
        match z with
        | USZipCode c ->
            printfn "A normal zip code: %s" c
        | USZipPlus4Code(code, suffix) ->
            printfn "A Zip+4 code: prefix %s, suffix %s" code suffix
        | _ as n ->
            printfn "Not a zip code: %s" n)

