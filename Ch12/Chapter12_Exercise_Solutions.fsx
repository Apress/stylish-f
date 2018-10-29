module Exercise12_01 =

    type Transaction = { Id : int } // Would contain more fields in reality

    let addTransactions
        (oldTransactions : Transaction[])
        (newTransactions : Transaction[]) =
        Array.append oldTransactions newTransactions

module Exercise12_03 =

    // To see the effect on performance, paste this code into
    // NaiveStringBuilding.fs in the NaiveStringBuilding solution.

    open System
    open System.Text

    let buildCsv (data : float[,]) = 
        let dataStrings =
            data |> Array2D.map (sprintf "%f")
        let sb = StringBuilder()
        for cols in 0..(dataStrings |> Array2D.length1) - 1 do
            sb.AppendLine(String.Join(",", cols)) |> ignore
        sb.ToString()

