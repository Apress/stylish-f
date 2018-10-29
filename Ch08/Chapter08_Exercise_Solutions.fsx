module Exercise08_01 =

    type GreyScale(r : byte, g : byte, b : byte) =

        member __.Level =
            (int r + int g + int b) / 3 |> byte

    let demo() =
        // 255
        GreyScale(255uy, 255uy, 255uy).Level |> printfn "%i"

module Exercise08_02 =

    open System.Drawing

    type GreyScale(r : byte, g : byte, b : byte) =

        new (color : Color) =
            GreyScale(color.R, color.G, color.B)

        member __.Level =
            (int r + int g + int b) / 3 |> byte

    let demo() =
        // 83
        GreyScale(Color.Brown).Level |> printfn "%i"

module Exercise08_03 =

    open System.Drawing

    type GreyScale(r : byte, g : byte, b : byte) =

        new (color : Color) =
            GreyScale(color.R, color.G, color.B)

        member __.Level =
            (int r + int g + int b) / 3 |> byte

        override this.ToString() =
            sprintf "Greyscale(%i)" this.Level

    let demo() =
        // Greyscale(140)
        GreyScale(Color.Orange) |> printfn "%A"
        // Greyscale(255)
        GreyScale(255uy, 255uy, 255uy) |> printfn "%A"

module Exercise08_04 =

    open System
    open System.Drawing

    type GreyScale(r : byte, g : byte, b : byte) =

        let level = (int r + int g + int b) / 3 |> byte

        let eq (that : GreyScale) =
            level = that.Level

        new (color : Color) =
            GreyScale(color.R, color.G, color.B)

        member __.Level =
            level

        override this.ToString() =
            sprintf "Greyscale(%i)" this.Level

        override this.GetHashCode() =
            hash level

        override __.Equals(thatObj) =
            match thatObj with
            | :? GreyScale as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<GreyScale> with
            member __.Equals(that : GreyScale) =
                eq that

    let demo() =
        let orange1 = GreyScale(Color.Orange)
        let blue = GreyScale(Color.Blue)
        let orange2 = GreyScale(0xFFuy, 0xA5uy, 0x00uy)
        let orange3 = GreyScale(0xFFuy, 0xA5uy, 0x01uy)
        // true
        printfn "%b" (orange1 = orange2)
        // false
        printfn "%b" (orange1 = blue)
        // true
        printfn "%b" (orange1 = orange3)
