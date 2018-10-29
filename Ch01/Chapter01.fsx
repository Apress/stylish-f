module Chapter01_01 = 

    let checkRate rate = 
        if rate < 0. then 0. else rate

    let checkAmount(amount : byref<float>) =
        if amount < 0. then
            amount <- 0.

    let addInterest (interestType : int, amt : float, rate : float, y : int) =
        let rate = checkRate rate
        let mutable amt = amt
        checkAmount(&amt)
        let mutable intType = interestType
        if intType <= 0 then intType <- 1
        if intType = 1 then
            let yAmt = amt * rate / 100.
            amt + yAmt * (float y)
        else
            amt * System.Math.Pow(1. + (rate/100.), float y)
        
// Listing 1-2 is in C# and so is not included in the provided examples.
