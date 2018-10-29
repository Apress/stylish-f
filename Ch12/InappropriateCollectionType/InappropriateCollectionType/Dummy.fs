// From Listing 12-1:
module Dummy

let slowFunction() =
    System.Threading.Thread.Sleep(100)
    99

let fastFunction() =
    System.Threading.Thread.Sleep(10)
    99
