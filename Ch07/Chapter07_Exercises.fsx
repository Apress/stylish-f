module Exercise07_03 =

    type Track (name : string, artist : string) =
        member __.Name = name
        member __.Artist = artist

    // Uncomment to see the error:
    //let tracks =
    //    [ Track("The Mollusk", "Ween")
    //      Track("Bread Hair", "They Might Be Giants")
    //      Track("The Mollusk", "Ween") ]
    //    // Error: The type 'Track' does not support the 
    //    // comparison constraint
    //    |> Set.ofList
        
module Exercise06_04 =

    open System

    [<Struct>]
    type Position = {
        X : float32
        Y : float32
        Z : float32
        Time : DateTime }


            
            

        
