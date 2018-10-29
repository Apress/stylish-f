module Exercise07_01 =

    open System

    [<Struct>]
    type Position = {
        X : float32
        Y : float32
        Z : float32
        Time : DateTime }

    // Uncomment and run in FSI without selecting the module line
    // #time "on"

    let test = 
        Array.init 1_000_000 (fun i ->
            { X = float32 i
              Y = float32 i
              Z = float32 i
              Time = DateTime.MinValue } )

    // Uncomment and run in FSI without selecting the module line
    // #time "off"

module Exercise07_03 =

    type Track = {
        Name : string
        Artist : string }

    // set [{Name = "Bread Hair";
    //       Artist = "They Might Be Giants";};
    //      {Name = "The Mollusk";
    //       Artist = "Ween";}]
    let tracks =
        [ { Name = "The Mollusk"
            Artist = "Ween" }
          { Name = "Bread Hair"
            Artist = "They Might Be Giants" }
          { Name = "The Mollusk"
            Artist = "Ween" } ]
        |> Set.ofList
        
module Exercise07_04 =

    open System

    [<Struct>]
    type Position = {
        X : float32
        Y : float32
        Z : float32
        Time : DateTime }

    let translate dx dy dz position =
        { position with
            X = position.X + dx
            Y = position.Y + dy
            Z = position.Z + dz }
            
    let p1 =
        { X = 1.0f
          Y = 2.0f
          Z = 3.0f
          Time = DateTime.MinValue }

    // val p2 : Position = {X = 1.5f;
    //                      Y = 1.5f;
    //                      Z = 4.5f;
    //                      Time = 01/01/0001 00:00:00;}
    let p2 = p1 |> translate 0.5f -0.5f 1.5f
 


            
            

        
