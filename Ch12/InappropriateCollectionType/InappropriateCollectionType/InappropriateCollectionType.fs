// From Listing 12-5:
module InappropriateCollectionType

module Old = 

    let sample interval data =
        [
            let max = (List.length data) - 1
            for i in 0..interval..max ->
                data.[i]
        ]

module New = 

    let sample interval data =
        [
            let max = (List.length data) - 1
            for i in 0..interval..max ->
                data.[i]
        ]

// Method |       Mean |      Gen 0 |     Gen 1 |     Gen 2 |   Allocated |
//------- |-----------:|-----------:|----------:|----------:|------------:|
//    Old | 1,624.9 ms |          - |         - |         - |    31.51 KB |
//    New |   177.7 ms | 11333.3333 | 5666.6667 | 1000.0000 | 62563.95 KB |

// From Listing 12-8:
//module New = 

//    let sample interval data =
//        data
//        |> Array.indexed
//        |> Array.filter (fun (i, _) ->
//            i % interval = 0)
//        |> Array.map snd

// Method |       Mean |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
//------- |-----------:|----------:|----------:|----------:|-----------:|
//    Old | 1,622.1 ms |         - |         - |         - |   31.51 KB |
//    New |   120.6 ms | 6200.0000 | 3400.0000 | 1000.0000 | 39201.9 KB |

// From Listing 12-10:
//module New = 

//    let sample interval data =
//        data
//        |> Array.indexed
//        |> Array.filter (fun (i, _) -> 
//            i % interval = 0)
//        |> Array.map snd

// Method |       Mean |     Gen 2 |  Allocated |
//------- |-----------:|----------:|-----------:|
//    Old | 1,622.1 ms |         - |   31.51 KB |
//    New |   120.6 ms | 1000.0000 | 39201.9 KB |

// From Listing 12-13:
//module New = 

//    let sample interval data =
//        data
//        |> Seq.indexed
//        |> Seq.filter (fun (i, _) -> 
//            i % interval = 0)
//        |> Seq.map snd

// Method |        Mean |      Gen 0 |   Allocated |
//------- |------------:|-----------:|------------:|
//    Old | 1,714.18 ms |          - |    31.51 KB |
//    New |    36.77 ms | 15214.2857 | 31274.59 KB |

// From Listing 12-16:
//module New = 

//    let sample interval data =
//        [|
//            let max = (Array.length data) - 1
//            for i in 0..interval..max ->
//                data.[i]
//        |]
    
// Note: expressed in microseconds.
//
// Method |            Mean |          Median |   Gen 0 | Allocated |
//------- |----------------:|----------------:|--------:|----------:|
//    Old | 1,663,428.72 us | 1,653,255.92 us |       - |  31.51 KB |
//    New |        28.61 us |        27.99 us | 11.8103 |  24.29 KB |

// From Listing 12-18:
//module New = 

//    let sample interval data =
//        [|
//            let max = 
//                ( (data |> Array.length |> float) / (float interval) 
//                  |> ceil 
//                  |> int ) - 1

//            for i in 0..max ->
//                 data.[i * interval]
//        |]
    
// Note: expressed in microseconds.
//
// Method |            Mean |   Gen 0 | Allocated |
//------- |----------------:|--------:|----------:|
//    Old | 1,704,387.26 us |       - |  31.51 KB |
//    New |        29.30 us | 11.8103 |  24.27 KB |