module Chapter02_01 =

    open System

    let convertMilesYards (milesPointYards : float) : float =
        raise <| NotImplementedException()

module Chapter02_02 =

    let convertMilesYards (milesPointYards : float) : float =
        let wholeMiles = milesPointYards |> floor
        let fraction = milesPointYards - float(wholeMiles)
        wholeMiles + (fraction / 0.1760)

    // val decimalMiles : float = 1.5
    let decimalMiles = 1.0880 |> convertMilesYards
    // val decimalMiles : float = 2.0
    let decimalMilesBad = 1.1760 |> convertMilesYards

module Chapter02_03 = 

    open System

    let convertMilesYards (milesPointYards : float) : float =
        let wholeMiles = milesPointYards |> floor
        let fraction = milesPointYards - float(wholeMiles)
        if fraction > 0.1759 then
            raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
        wholeMiles + (fraction / 0.1760)

    // System.ArgumentOutOfRangeException: Fractional part must be <= 0.1759
    // Parameter name: milesYards
    let decimalMiles = 1.1760 |> convertMilesYards

module Chapter02_04 = 

    open System

    let milesYardsToDecimalMiles (milesPointYards : float) : float =
        let wholeMiles = milesPointYards  |> floor
        let fraction = milesPointYards - float(wholeMiles)
        if fraction > 0.1759 then
            raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
        wholeMiles + (fraction / 0.1760)

// Listing 2-5 is in C# and so is not included in the provided examples.

module Chapter02_06 =

    type MilesYards = MilesYards of int * int

module Chapter02_07 =

    open System

    type MilesYards = MilesYards of wholeMiles : int * yards : int

    let create (milesPointYards : float) : MilesYards =
        let wholeMiles = milesPointYards |> floor |> int
        let fraction = milesPointYards - float(wholeMiles)
        if fraction > 0.1759 then
            raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
        let yards = fraction * 10_000. |> round |> int
        MilesYards(wholeMiles, yards)

module Chapter02_08 =

    open System

    type MilesYards = MilesYards of wholeMiles : int * yards : int

    let create (milesPointYards  : float) : MilesYards =
        let wholeMiles = milesPointYards |> floor |> int
        let fraction = milesPointYards - float(wholeMiles)
        if fraction > 0.1759 then
            raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
        let yards = fraction * 10_000. |> round |> int
        MilesYards(wholeMiles, yards)

    let milesYardsToDecimalMiles (milesYards : MilesYards) : float =
        match milesYards with
        | MilesYards(wholeMiles, yards) ->
            (float wholeMiles) + ((float yards) / 1760.)

module Chapter02_09 =

    open System

    module MilesYards = 

        type MilesYards = MilesYards of wholeMiles : int * yards : int

        let fromMilesPointYards (milesPointYards : float) : MilesYards =
            let wholeMiles = milesPointYards |> floor |> int
            let fraction = milesPointYards - float(wholeMiles)
            if fraction > 0.1759 then
                raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
            let yards = fraction * 10_000. |> round |> int
            MilesYards(wholeMiles, yards)

        let toDecimalMiles (milesYards : MilesYards) : float =
            match milesYards with
            | MilesYards(wholeMiles, yards) ->
                (float wholeMiles) + ((float yards) / 1760.)

module Chapter02_10 =

    open System

    module MilesYards = 

        type T = MilesYards of wholeMiles : int * yards : int

        let fromMilesPointYards (milesPointYards : float) : T =
            let wholeMiles = milesPointYards |> floor |> int
            let fraction = milesPointYards - float(wholeMiles)
            if fraction > 0.1759 then
                raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
            let yards = fraction * 10_000. |> round |> int
            MilesYards(wholeMiles, yards)

        let toDecimalMiles (milesYards : T) : float =
            match milesYards with
            | MilesYards(wholeMiles, yards) ->
                (float wholeMiles) + ((float yards) / 1760.)

module Chapter02_11 =

    module MilesYards = 

        type MilesYards = 
            private MilesYards of wholeMiles : int * yards : int

module Chapter02_12 =

    open System

    module MilesYards = 

        type MilesYards = 
            private MilesYards of wholeMiles : int * yards : int

        let fromMilesPointYards (milesPointYards : float) : MilesYards =
            let wholeMiles = milesPointYards |> floor |> int
            let fraction = milesPointYards - float(wholeMiles)
            if fraction > 0.1759 then
                raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
            let yards = fraction * 10_000. |> round |> int
            MilesYards(wholeMiles, yards)

        let toDecimalMiles (milesYards : MilesYards) : float =
            match milesYards with
            | MilesYards(wholeMiles, yards) ->
                (float wholeMiles) + ((float yards) / 1760.)

module Chapter02_13 =

    open System

    module MilesYards = 

        type MilesYards = 
            private MilesYards of wholeMiles : int * yards : int

        let fromMilesPointYards (milesPointYards : float) : MilesYards =
            let wholeMiles = milesPointYards |> floor |> int
            let fraction = milesPointYards - float(wholeMiles)
            if fraction > 0.1759 then
                raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
            let yards = fraction * 10_000. |> round |> int
            MilesYards(wholeMiles, yards)

        let toDecimalMiles (MilesYards(wholeMiles, yards)) : float =
            (float wholeMiles) + ((float yards) / 1760.)

module Chapter02_14 =

    open System

    module MilesYards = 

        let private (~~) = float

        type MilesYards = 
            private MilesYards of wholeMiles : int * yards : int

        let fromMilesPointYards (milesPointYards : float) : MilesYards =
            let wholeMiles = milesPointYards |> floor |> int
            let fraction = milesPointYards - float(wholeMiles)
            if fraction > 0.1759 then
                raise <| ArgumentOutOfRangeException("milesPointYards", "Fractional part must be <= 0.1759")
            let yards = fraction * 10_000. |> round |> int
            MilesYards(wholeMiles, yards)

        let toDecimalMiles (MilesYards(wholeMiles, yards)) : float =
            ~~wholeMiles + (~~ yards / 1760.)
