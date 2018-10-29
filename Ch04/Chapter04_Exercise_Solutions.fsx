// From Listing 4-2:
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

module Exercise04_01 = 

    open Houses

    let housePrices =
        getHouses 20
        |> Array.map (fun h ->
            sprintf "Address: %s - Price: %f" h.Address h.Price)

module Exercise04_02 = 

    open Houses

    let averagePrice =
        getHouses 20
        |> Array.averageBy (fun h -> h.Price)

module Exercise04_03 = 
    
    open Houses

    let expensive =
        getHouses 20
        |> Array.filter (fun h -> h.Price > 250_000m)

module Exercise04_04 = 

    open Houses

    let housesNearSchools =
        getHouses 20
        |> Array.choose (fun h ->
            // See also the "Missing Data" chapter
            match h |> trySchoolDistance with
            | Some d -> Some(h, d)
            | None -> None)

module Exercise04_05 = 
    
    open Houses

    getHouses 20
    |> Array.filter (fun h -> h.Price > 100_000m)
    |> Array.iter (fun h -> 
        printfn "Address: %s Price: %f" h.Address h.Price)

module Exercise04_06 = 
    
    open Houses

    getHouses 20
    |> Array.filter (fun h -> h.Price > 100_000m)
    |> Array.sortByDescending (fun h -> h.Price)
    |> Array.iter (fun h -> 
        printfn "Address: %s Price: %f" h.Address h.Price)

module Exercise04_07 =
 
    open Houses

    let averageOver200K = 
        getHouses 20
        |> Array.filter (fun h -> h.Price > 200_000m)
        |> Array.averageBy (fun h -> h.Price)

module Exercise04_08 = 

    open Houses

    let cheapHouseWithKnownSchoolDistance =
        getHouses 20
        |> Array.filter (fun h -> h.Price < 100_000m)
        |> Array.pick (fun h ->
            match h |> trySchoolDistance with
            | Some d -> Some(h, d)
            | None -> None)

module Exercise04_09 = 

    open Houses

    let housesByBand =
        getHouses 20
         |> Array.groupBy (fun h -> priceBand h.Price)
         |> Array.map (fun group ->
            let band, houses = group
            band, houses |> Array.sortBy (fun h -> h.Price))

    let housesByBand2 =
        getHouses 20
         |> Array.groupBy (fun h -> priceBand h.Price)
         |> Array.map (fun (band, houses) ->
            band, houses |> Array.sortBy (fun h -> h.Price))

module Array = 

    let inline tryAverageBy f (a : 'T[]) = 
        if a.Length = 0 then
            None
        else
            a |> Array.averageBy f |> Some

module Exercise04_10 = 
    
    open Houses

    let averageOver200K = 
        getHouses 20
        |> Array.filter (fun h -> h.Price > 200_000m)
        |> Array.tryAverageBy (fun h -> h.Price)

module Exercise04_11 = 

    open Houses

    let cheapHouseWithKnownSchoolDistance =
        getHouses 20
        |> Array.filter (fun h -> h.Price < 100_000m)
        |> Array.tryPick (fun h ->
            match h |> trySchoolDistance with
            | Some d -> Some(h, d)
            | None -> None)










