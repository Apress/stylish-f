module Exercise03_01 =

    type Delivery =
    | AsBilling
    | Physical of string
    | Download
    | ClickAndCollect of int

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
        | Download -> 
            None
        // Added for exercise
        | ClickAndCollect _ 
            -> None
        |> Option.map (fun address -> 
            sprintf "%s\n%s" billingDetails.name address)

    let deliveryLabels (billingDetails : BillingDetails seq) =
        billingDetails
        |> Seq.choose tryDeliveryLabel

    let collectionsFor (storeId : int) (billingDetails : BillingDetails seq) =
        billingDetails
        |> Seq.choose (fun d ->
            match d.delivery with
            | ClickAndCollect s when s = storeId -> 
                Some d
            | _ -> None)

    let myOrder = {
        name = "Kit Eason"
        billing = "112 Fibonacci Street\nErehwon\n35813"
        delivery = AsBilling }

    let yourOrder = {
        name = "Alison Chan"
        billing = "885 Electric Avenue\nErewhon\n41878"
        delivery = ClickAndCollect 1 }

    let theirOrder = {
        name = "Pana Okpik"
        billing = "299 Relativity Drive\nErewhon\79245"
        delivery = ClickAndCollect 2 }

    // { name = "Alison Chan";
    //   billing = "885 Electric Avenue
    //              Erewhon
    //              41878"; }
    //   delivery = ClickAndCollect 1;}
    [ myOrder; yourOrder; theirOrder ]
    |> collectionsFor 1
    |> Seq.iter (printfn "%A")

module Exercise03_02 =

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
        delivery = None }

    let herOrder = {
        name = "Jane Smith"
        billing = null
        delivery = None }

    let orders = [| myOrder; hisOrder; herOrder |]

    let countNullBillingAddresses (orders : seq<BillingDetails>) = 
        orders
        |> Seq.map (fun bd -> bd.billing)
        |> Seq.map Option.ofObj
        |> Seq.sumBy Option.count

    countNullBillingAddresses orders

