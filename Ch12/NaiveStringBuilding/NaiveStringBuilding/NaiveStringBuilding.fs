module NaiveStringBuilding

// From Listing 12-39:
open System

module Old =

    let private buildLine (data : float[]) =
        let mutable result = ""
        for x in data do
            result <- sprintf "%s%f," result x
        result.TrimEnd(',')

    let buildCsv (data : float[,]) =
        let mutable result = ""
        for r in 0..(data |> Array2D.length1) - 1 do
            let row = data.[r, *]
            let rowString = row |> buildLine
            result <- sprintf "%s%s%s" result rowString Environment.NewLine
        result

module New = 

    let private buildLine (data : float[]) =
        let mutable result = ""
        for x in data do
            result <- sprintf "%s%f," result x
        result.TrimEnd(',')

    let buildCsv (data : float[,]) =
        let mutable result = ""
        for r in 0..(data |> Array2D.length1) - 1 do
            let row = data.[r, *]
            let rowString = row |> buildLine
            result <- sprintf "%s%s%s" result rowString Environment.NewLine
        result

// Method |    Mean |        Gen 0 |       Gen 1 |       Gen 2 | Allocated |
//------- |--------:|-------------:|------------:|------------:|----------:|
//    Old | 2.307 s | 1048000.0000 | 241000.0000 | 236000.0000 |   3.08 GB |
//    New | 2.288 s | 1052000.0000 | 245000.0000 | 240000.0000 |   3.08 GB |

    // From Listing 12-42:
    //open System.Text

    //let private buildLine (data : float[]) =
    //    let sb = StringBuilder()
    //    for x in data do
    //        sb.Append(sprintf "%f," x) |> ignore
    //    sb.ToString().TrimEnd(',')

    //let buildCsv (data : float[,]) =
    //    let sb = StringBuilder()
    //    for r in 0..(data |> Array2D.length1) - 1 do
    //        let row = data.[r, *]
    //        let rowString = row |> buildLine
    //        sb.AppendLine(rowString) |> ignore
    //    sb.ToString()

// Method |       Mean |        Gen 0 |       Gen 1 |       Gen 2 |  Allocated |
//------- |-----------:|-------------:|------------:|------------:|-----------:|
//    Old | 2,227.5 ms | 1039000.0000 | 234000.0000 | 228000.0000 | 3151.65 MB |
//    New |   166.5 ms |   17333.3333 |   2666.6667 |   1000.0000 |   95.63 MB |

    // From Listing 12-44:
    //open System.Text

    //let private buildLine (data : float[]) =
    //    let cols = data |> Array.map (sprintf "%f")
    //    String.Join(',', cols)

    //let buildCsv (data : float[,]) =
    //    let sb = StringBuilder()
    //    for r in 0..(data |> Array2D.length1) - 1 do
    //        let row = data.[r, *]
    //        let rowString = row |> buildLine
    //        sb.AppendLine(rowString) |> ignore
    //    sb.ToString()

//Method |       Mean |        Gen 0 |       Gen 1 |       Gen 2 | Allocated |
//------ |-----------:|-------------:|------------:|------------:|----------:|
//   Old | 2,853.5 ms | 1036000.0000 | 229000.0000 | 224000.0000 | 3151.6 MB |
//   New |   135.7 ms |    9500.0000 |   2500.0000 |    750.0000 |  50.85 MB |

    // From Listing 12-46:
    //open System.Text

    //let private buildLine (data : float[]) =
    //    let cols = data |> Array.Parallel.map (sprintf "%f")
    //    String.Join(',', cols)

    //let buildCsv (data : float[,]) =
    //    let sb = StringBuilder()
    //    for r in 0..(data |> Array2D.length1) - 1 do
    //        let row = data.[r, *]
    //        let rowString = row |> buildLine
    //        sb.AppendLine(rowString) |> ignore
    //    sb.ToString()

//Method |        Mean |        Gen 0 |       Gen 1 |       Gen 2 |  Allocated 
//------ |------------:|-------------:|------------:|------------:|-----------:
//   Old | 2,745.12 ms | 1038000.0000 | 229000.0000 | 226000.0000 | 3151.66 MB 
//   New |    91.21 ms |   10833.3333 |   2166.6667 |    833.3333 |   30.95 MB 





