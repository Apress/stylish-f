module Chapter03_02 =

    type Shape<'T> =
    | Square of height:'T
    | Rectangle of height:'T * width:'T
    | Circle of radius:'T

module Chapter03_03 =

    type Option<'T> =
    | Some of 'T
    | None

module Chapter03_04 =

    type Shape<'T> =
    | Square of height:'T
    | Rectangle of height:'T * width:'T
    | Circle of radius:'T
        
    let describe (shape : Shape<float>) =
        match shape with
        | Square h -> sprintf "Square of height %f" h
        | Rectangle(h, w) -> sprintf "Rectangle %f x %f" h w
        | Circle r -> sprintf "Circle of radius %f" r

    let goldenRect = Rectangle(1.0, 1.61803)
        
    // Rectangle 1.000000 x 1.618030
    printfn "%s" (describe goldenRect)

module Chapter03_05 =

    let myMiddleName = Some "Brian"
    let herMiddleName = None

    let displayMiddleName (name : Option<string>) =
    // Alternatively:
    //let displayMiddleName (name : string option) =
        match name with 
        | Some s -> s
        | None -> ""

    // >>>Brian<<<
    printfn ">>>%s<<<" (displayMiddleName myMiddleName)
    // >>><<<
    printfn ">>>%s<<<" (displayMiddleName herMiddleName)

module Chapter03_06 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // Error: the expression was expected to have type 'string'
    // but here has type 'string option'
    // Uncomment to see errors:
    //printfn "%s" myOrder.delivery
    //printfn "%s" hisOrder.delivery

module Chapter03_07 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let addressForPackage (details : BillingDetails) =
        let address =
            match details.delivery with
            | Some s -> s
            | None -> details.billing
        sprintf "%s\n%s" details.name address

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // Kit Eason
    // 112 Fibonacci Street
    // Erehwon
    // 35813
    printfn "%s" (addressForPackage myOrder)

    // John Doe
    // 16 Planck Parkway
    // Erewhon
    // 62291
    printfn "%s" (addressForPackage hisOrder)

module Chapter03_08 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let addressForPackage (details : BillingDetails) =
        let address = 
            Option.defaultValue details.billing details.delivery

        // More idiomatic:
        //let address = 
        //     details.delivery |> Option.defaultValue details.billing

        sprintf "%s\n%s" details.name address

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // Kit Eason
    // 112 Fibonacci Street
    // Erehwon
    // 35813
    printfn "%s" (addressForPackage myOrder)

    // John Doe
    // 16 Planck Parkway
    // Erewhon
    // 62291
    printfn "%s" (addressForPackage hisOrder)

module Chapter03_09 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let addressForPackage (details : BillingDetails) =
        let address = 
            details.delivery
            |> Option.defaultValue details.billing

        sprintf "%s\n%s" details.name address

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // Kit Eason
    // 112 Fibonacci Street
    // Erehwon
    // 35813
    printfn "%s" (addressForPackage myOrder)

    // John Doe
    // 16 Planck Parkway
    // Erewhon
    // 62291
    printfn "%s" (addressForPackage hisOrder)

module Chapter03_10 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let printDeliveryAddress (details : BillingDetails) =
        details.delivery
        |> Option.iter 
            (fun address -> printfn "Delivery address:\n%s\n%s" details.name address)

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }
            
    // No output at all
    myOrder |> printDeliveryAddress
        
    // Delivery address:
    // John Doe
    // 16 Planck Parkway
    // Erewhon
    // 62291
    hisOrder |> printDeliveryAddress

module Chapter03_11 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let printDeliveryAddress (details : BillingDetails) =
        details.delivery
        |> Option.map 
            (fun address -> address.ToUpper())
        |> Option.iter 
            (fun address -> 
                printfn "Delivery address:\n%s\n%s" 
                    (details.name.ToUpper()) address)

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // No output at all
    myOrder |> printDeliveryAddress
        
    // Delivery address:
    // JOHN DOE
    // 16 PLANCK PARKWAY
    // EREWHON
    // 62291
    hisOrder |> printDeliveryAddress

module Chapter03_12 =

    open System

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let tryLastLine (address : string) =
        let parts = 
            address.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries) 
        // Could also just do parts |> Array.tryLast
        match parts with
        | [||] -> 
            None
        | parts -> 
            parts |> Array.last |> Some

    let tryPostalCode (codeString : string) =
        match Int32.TryParse(codeString) with
        | true, i -> i |> Some
        | false, _ -> None

    let postalCodeHub (code : int) =
        if code = 62291 then
            "Hub 1"
        else
            "Hub 2"

    let tryHub (details : BillingDetails) =
        details.delivery
        |> Option.bind tryLastLine
        |> Option.bind tryPostalCode
        |> Option.map postalCodeHub

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }

    // None
    myOrder |> tryHub
        
    // Some "Hub 1"
    hisOrder |> tryHub

module Chapter03_13 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    // Accessing payload via .IsSome and .Value
    // Don't do this!
    let printDeliveryAddress (details : BillingDetails) =
        if details.delivery.IsSome then
            printfn "Delivery address:\n%s\n%s" 
                (details.name.ToUpper()) 
                (details.delivery.Value.ToUpper())

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = None }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Some "16 Planck Parkway\nErewhon\n62291" }
            
    // No output at all
    myOrder |> printDeliveryAddress
        
    // Delivery address:
    // John Doe
    // 16 Planck Parkway
    // Erewhon
    // 62291
    hisOrder |> printDeliveryAddress

module Chapter03_14 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

module Chapter03_15 =

    type Delivery =
    | AsBilling
    | Physical of string
    | Download

    type BillingDetails = {
        name : string
        billing :  string
        delivery : Delivery }

module Chapter03_16 =

    type Delivery =
    | AsBilling
    | Physical of string
    | Download

    type BillingDetails = {
        name : string
        billing :  string
        delivery : Delivery }

    let tryDeliveryLabel (billingDetails : BillingDetails) =
        match billingDetails.delivery with
        | AsBilling -> 
            billingDetails.billing |> Some
        | Physical address -> 
            address |> Some
        | Download -> None
        |> Option.map (fun address -> 
            sprintf "%s\n%s" billingDetails.name address)

    let deliveryLabels (billingDetails : BillingDetails seq) =
        billingDetails
        |> Seq.choose tryDeliveryLabel

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = AsBilling }

    let hisOrder = {
        name = "John Doe"
        billing = "314 Pi Avenue\nErewhon\n15926"
        delivery = Physical "16 Planck Parkway\nErewhon\n62291" }

    let herOrder = {
        name = "Jane Smith"
        billing = "9 Gravity Road\nErewhon\n80665" 
        delivery = Download }

    // seq
    //     [ "Kit Eason
    //        112 Fibonacci Street
    //        Erehwon
    //        35813";
    //       "John Doe
    //        16 Planck Parkway
    //        Erewhon
    //        62291"]
    [ myOrder; hisOrder; herOrder ]
    |> deliveryLabels

module Chapter03_17 =

    type BillingDetails = {
        name : string
        billing :  string
        delivery : string option }

    let printDeliveryAddress (details : BillingDetails) =
        details.delivery
        |> Option.map 
            (fun address -> address.ToUpper())
        |> Option.iter 
            (fun address -> 
                printfn "Delivery address:\n%s\n%s" 
                    (details.name.ToUpper()) address)

    let dangerOrder = {
        name = "Will Robinson"
        billing = "2 Jupiter Avenue\nErewhon\n199732"
        delivery = Some null }

    // NullReferenceException
    printDeliveryAddress dangerOrder

module Chapter03_18 =

    type SafeString (s : string) =
        do 
            if s = null then
                raise <| System.ArgumentException()
        member __.Value = s
        override __.ToString() = s

    type BillingDetails = {
        name : SafeString
        billing :  SafeString
        delivery : SafeString option }

    let printDeliveryAddress (details : BillingDetails) =
        details.delivery
        |> Option.map 
            (fun address -> address.Value.ToUpper())
        |> Option.iter 
            (fun address -> 
                printfn "Delivery address:\n%s\n%s" 
                    (details.name.Value.ToUpper()) address)

    // NullReferenceException at construction time
    let dangerOrder = {
        name = SafeString "Will Robinson"
        billing = SafeString "2 Jupiter Avenue\nErewhon\n199732"
        delivery = SafeString null |> Some }

    printDeliveryAddress dangerOrder

module Chapter03_19 =

    let myApiFunction (stringParam : string) =
        let s = 
            stringParam
            |> Option.ofObj 
            |> Option.defaultValue "(none)"
            
        // You can do things here knowing that s isn't null
        printfn "%s" (s.ToUpper())

    // HELLO
    myApiFunction "hello"

    // NONE
    myApiFunction null

module Chapter03_20 =
    
    open System

    let showHeartRate (rate : Nullable<int>) =
        rate
        |> Option.ofNullable
        |> Option.map (fun r -> r.ToString())
        |> Option.defaultValue "N/A"

    // 96
    showHeartRate (System.Nullable(96))
        
    // N/A
    showHeartRate (System.Nullable())

module Chapter03_21 =
    
    open System

    let random = new Random()

    let tryLocationDescription (locationId : int) =
        // In reality this would be attempting
        // to get the description from a database etc.
        let r = random.Next(1, 100)
        if r < 50 then 
            Some (sprintf "Location number %i" r)
        else
            None

    let tryLocationDescriptionNullable (locationId : int) =
        tryLocationDescription locationId
        |> Option.toObj

    // Sometimes null, sometimes "Location number #"
    tryLocationDescriptionNullable 99
        
module Chapter03_22 =
    
    open System

    let random = new Random()

    let tryLocationDescription (locationId : int, description : string byref) : bool =
        // In reality this would be attempting
        // to get the description from a database etc.
        let r = random.Next(1, 100)
        if r < 50 then 
            description <- sprintf "Location number %i" r
            true
        else
            description <- null
            false

module Chapter03_23 =
    
    open System

    let random = new Random()

    let getHeartRateInternal() =
        // In reality this would be attempting
        // to get a heart rate from a sensor:
        let rate = random.Next(0, 200)
        if rate = 0 then 
            None
        else
            Some rate

    let tryGetHeartRate () =
        getHeartRateInternal()
        |> Option.toNullable

// Listing 3-24 is in C# and so is not included in the provided examples.

module Chapter03_25 =

    let valueOptionString (v : int voption) =
        match v with
        | ValueSome x ->
            sprintf "Value: %i" x
        | ValueNone ->
            sprintf "No value"

    // "No value"
    ValueOption.ValueNone
    |> valueOptionString

    // "Value: 99"
    ValueOption.ValueSome 99
    |> valueOptionString
