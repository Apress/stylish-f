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
