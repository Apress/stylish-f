module Exercise12_01 =

    type Transaction = { Id : int } // Would contain more fields in reality

    let addTransactions
        (oldTransactions : Transaction list)
        (newTransactions : Transaction list) =
        oldTransactions @ newTransactions

module Exercise12_03 =

    open System
    open System.Text

    let private buildLine (data : float[]) =
        let cols = data |> Array.Parallel.map (sprintf "%f")
        // This call has been changed from taking a character separator
        // to a string separator - i.e. "," instead of ','. This is because
        // the character overload is not always available in scripts.
        String.Join(",", cols)

    let buildCsv (data : float[,]) =
        let sb = StringBuilder()
        for r in 0..(data |> Array2D.length1) - 1 do
            let row = data.[r, *]
            let rowString = row |> buildLine
            sb.AppendLine(rowString) |> ignore
        sb.ToString()
