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


