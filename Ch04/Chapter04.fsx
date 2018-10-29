module Chapter04_01 =

    type House = { Address : string; Price : decimal }

    let houses = 
        [|
            { Address = "1 Accacia Avenue"; Price = 250_000m }
            { Address = "2 Bradley Street"; Price = 380_000m }
            { Address = "1 Carlton Road"; Price = 98_000m }
        |]

    let cheapHouses = 
        houses |> Array.filter (fun h -> h.Price < 100_000m)

    // [|{Address = "1 Carlton Road"; Price = 98000M;}|]
    printfn "%A" cheapHouses
    
// The code for Listings 4-3 through 4-9 is in the Chapter04_Exercises.fsx file.

// From Listing 4-2, used in several other modules below:
module Houses =

    type House = { Address : string; Price : decimal }
    type PriceBand = | Cheap | Medium | Expensive

    /// Make an array of 'count' random houses.
    let getHouses count =
        let random = System.Random(Seed = 1)
        Array.init count (fun i -> 
            { Address = sprintf "%i Stochastic Street" (i+1)
              Price = random.Next(50_000, 500_000) |> decimal })

    let random = System.Random(Seed = 1)
    
    /// Try to get the distance to the nearest school.
    /// (Results are simulated)
    let trySchoolDistance (house : House) =
        // Because we simulate results, the house
        // parameter isn’t actually used.
        let dist = random.Next(10) |> double
        if dist < 8. then
            Some dist
        else
            None

    // Return a price band based on price.
    let priceBand (price : decimal) =
        if price < 100_000m then 
            Cheap
        else if price < 200_000m then 
            Medium
        else 
            Expensive

module Chapter04_10 =

    let averageValue (values : decimal[]) =
        if values.Length = 0 then
            0.m
        else
            values |> Array.average

    // 370.m
    let ex1 = [|10.m; 100.m; 1000.m|] |> averageValue

    // 0.m
    let ex2 = [||] |> averageValue

module Chapter04_11 =

    let inline averageOrZero (a : 'T[]) =
        if a.Length = 0 then
            LanguagePrimitives.GenericZero<'T>
        else
            a |> Array.average

    // 370.m
    let ex3 = [|10.m; 100.m; 1000.m|] |> averageOrZero
    // 370.f
    let ex3f = [|10.f; 100.f; 1000.f|] |> averageOrZero

    // 0.m
    let ex4 = [||] |> averageOrZero<decimal> 
    // 0.f
    let ex4f = [||] |> averageOrZero<float32> 

module Chapter04_12 =

    module Array = 

        let inline averageOr (dflt : 'T) (a : 'T[]) =
            if a.Length = 0 then
                dflt
            else
                a |> Array.average

    // 370.m
    let ex5 = [|10.m; 100.m; 1000.m|] |> Array.averageOr 0.m
    // 370.f
    let ex5f = [|10.f; 100.f; 1000.f|] |> Array.averageOr 0.f

    // 0.m
    let ex6 = [||] |> Array.averageOr 0.m 
    // 0.f
    let ex6f = [||] |> Array.averageOr 0.f

module Chapter04_13 =

    module Array =

        let inline tryAverage (a : 'T[]) = 
            if a.Length = 0 then
                None
            else
                a |> Array.average |> Some

    // "The average was 370.000000"
    match [|10.m; 100.m; 1000.m|] |> Array.tryAverage with
    | Some av -> printfn "The average was %f" av
    | None -> printfn "There was no average."

    // "There was no average."
    match [||] |> Array.tryAverage with
    | Some av -> printfn "The average was %f" av
    | None -> printfn "There was no average."

module Chapter04_14 =

    module Array =

        let inline tryAverage (a : 'T[]) = 
            if a.Length = 0 then
                None
            else
                a |> Array.average |> Some

    // "The average was 370.000000"
    match [|10.m; 100.m; 1000.m|] |> Array.tryAverage with
    | Some av -> printfn "The average was %f" av
    | None -> printfn "There was no average."

    // "There was no average."
    match [||] |> Array.tryAverage with
    | Some av -> printfn "The average was %f" av
    | None -> printfn "There was no average."

module Chapter04_15 =

    let novelWords = Set ["The";"the";"quick";"brown";"Fox";"fox"]

    // set ["brown"; "fox"; "quick"; "the"]
    let lowerWords =
        novelWords
        |> Set.map (fun w -> w.ToLowerInvariant())

module Chapter04_16 =

    open Houses

    // Function which creates some random houses as a sequence:
    let getHousesSeq count =
        let random = System.Random(Seed = 1)
        Seq.init count (fun i -> 
            { Address = sprintf "%i Stochastic Street" (i+1)
              Price = random.Next(50_000, 500_000) |> decimal })

    // Convert a sequence of houses into an array, so that we
    // can use Array.partition to divide them into affordable and
    // unaffordable. (There is no Seq.partition.)
    let affordable, unaffordable = 
        getHousesSeq 20
        |> Array.ofSeq
        |> Array.partition (fun h -> h.Price < 150_000m)

module Chapter04_17 =

    module Array = 
        let inline tryAverage (a : 'T[]) = 
            if a.Length = 0 then
                None
            else
                a |> Array.average |> Some
    
    module UsingChoose =

        open Houses

        // Calculate the average known distance to school
        // in a sample of 20 houses.
        let averageDistanceToSchool = 
            getHouses 20
            |> Array.map trySchoolDistance
            |> Array.filter (fun d -> d.IsSome)
            |> Array.map (fun d -> d.Value)
            |> Array.tryAverage

        // As previous function, but use Array.choose instead
        // of map, filter and map.
        let averageDistanceToSchool2 = 
            getHouses 20
            |> Array.choose trySchoolDistance
            |> Array.tryAverage

module Chapter04_18 =

    module LongLambdaFunction =

        open Houses

        // Get houses with their price bands the long-winded way:
        let housesWithBands =
            getHouses 20
            |> Array.map (fun h ->
                let band = 
                    if h.Price < 100_000m then 
                        Cheap
                    else if h.Price < 200_000m then 
                        Medium
                    else 
                        Expensive
                h, band)

        // Most of the code above could be pulled into a priceBand function:
        // (Here we use the one that is already defined in the Houses module.)
        let housesWithBands2 = 
            getHouses 20
            |> Array.map (fun h ->
                h, h.Price |> priceBand)

module Chapter04_19 =

    module HousePriceReport =

        open Houses

        let bandOrder = function
        | Cheap -> 0 | Medium -> 1 | Expensive -> 2

        // A report of price bands and the houses that fall into them:
        getHouses 20
        |> Seq.groupBy (fun h -> h.Price |> priceBand)
        |> Seq.sortBy (fun (band, _) -> band |> bandOrder)
        |> Seq.iter (fun (band, houses) -> 
            printfn "---- %A ----" band
            houses 
            |> Seq.iter (fun h -> printfn "%s - %f" h.Address h.Price))

        // Like the previous report, but using an explicit type to
        // minimise use of tuples:
        type PriceBandGroup = {
            PriceBand : PriceBand
            Houses : seq<House> }

        getHouses 20
        |> Seq.groupBy (fun h -> h.Price |> priceBand)
        |> Seq.map (fun (band, houses) -> 
            { PriceBand = band; Houses = houses })
        |> Seq.sortBy (fun group -> group.PriceBand |> bandOrder)
        |> Seq.iter (fun group -> 
            printfn "---- %A ----" group.PriceBand
            group.Houses 
            |> Seq.iter (fun h -> printfn "%s - %f" h.Address h.Price))



