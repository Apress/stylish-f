module Chapter07_01 =

    open System
    
    type FileDescription = {
        Path : string
        Name : string
        LastModified : DateTime }

module Chapter07_02 =

    open System
    
    type FileDescription = {
        Path : string
        Name : string
        LastModified : DateTime }

    open System.IO

    let fileSystemInfo (rootPath : string) =
        Directory.EnumerateFiles(rootPath, "*.*", 
                                    SearchOption.AllDirectories)
        |> Seq.map (fun path ->
            { FileDescription.Path = path |> Path.GetDirectoryName
              Name = path |> Path.GetFileName
              LastModified = (FileInfo(path)).LastWriteTime })

module Chapter07_03 =

    open System
    
    type FileDescription = {
        Path : string
        Name : string
        LastModified : DateTime }

    open System.IO

    let fileSystemInfo (rootPath : string) =
        Directory.EnumerateFiles(rootPath, "*.*", 
                                    SearchOption.AllDirectories)
        |> Seq.map (fun path ->
            { FileDescription.Path = path |> Path.GetDirectoryName
              Name = path |> Path.GetFileName
              LastModified = (FileInfo(path)).LastWriteTime })

    // Name: ad.png Path: c:\temp Last modified: 15/08/2017 22:07:34
    // Name: capture-1.avi Path: c:\temp Last modified: 27/02/2017 22:04:31
    // ...
    fileSystemInfo @"c:\temp"
    |> Seq.iter (fun info -> // info is a FileDescription instance
        printfn "Name: %s Path: %s Last modified: %A"
            info.Name info.Path info.LastModified)
        
module Chapter07_04 =

    type MyRecord = {
        String : string
        Int : int }

    let mutable myRecord = 
        { String = "Hullo clouds"
          Int = 99 }

    // {String = "Hullo clouds";
    //  Int = 99;}
    printfn "%A" myRecord

    myRecord <-
        { String = "Hullo sky"
          Int = 100 }

    // {String = "Hullo sky";
    //  Int = 100;}
    printfn "%A" myRecord

module Chapter07_05 =

    type MyRecord = {
        mutable String : string
        mutable Int : int }

    let myRecord = 
        { String = "Hullo clouds"
          Int = 99 }

    // {String = "Hullo clouds";
    //  Int = 99;}
    printfn "%A" myRecord

    myRecord.String <- "Hullo sky"

    // {String = "Hullo sky";
    //  Int = 99;}
    printfn "%A" myRecord

module Chapter07_06 =

    type MyRecord = {
        String : string
        Int : int }

    let myRecord = 
        { String = "Hullo clouds"
          Int = 99 }

    // {String = "Hullo clouds";
    //  Int = 99;}
    printfn "%A" myRecord

    let myRecord2 = 
        { myRecord with String = "Hullo sky" }

    // {String = "Hullo sky";
    //  Int = 99;}
    printfn "%A" myRecord2

module Chapter07_07 =

    open System

    type FileDescriptionOO(path : string, name : string, lastModified : DateTime) =
        member __.Path = path
        member __.Name = name
        member __.LastModified = lastModified

    open System.IO

    let fileSystemInfoOO (rootPath : string) =
        Directory.EnumerateFiles(rootPath, "*.*", 
                                    SearchOption.AllDirectories)
        |> Seq.map (fun path ->
            FileDescriptionOO(path |> Path.GetDirectoryName,
                              path |> Path.GetFileName,
                              (FileInfo(path)).LastWriteTime))

    // Name: ad.png Path: c:\temp Last modified: 15/08/2017 22:07:34
    // Name: capture-1.avi Path: c:\temp Last modified: 27/02/2017 22:04:31
    // ...
    fileSystemInfoOO @"c:\temp"
    |> Seq.iter (fun info ->
        printfn "Name: %s Path: %s Last modified: %A"
            info.Name info.Path info.LastModified)

module Chapter07_08 =

    type LatLon(latitude : float, longitude : float) =
        member __.Latitude = latitude
        member __.Longitude = longitude

module Chapter07_09 =

    type LatLon(latitude : float, longitude : float) =
        member __.Latitude = latitude
        member __.Longitude = longitude

    let waterloo = LatLon(51.5031, -0.1132)
    let victoria = LatLon(51.4952, -0.1441)
    let waterloo2 = LatLon(51.5031, -0.1132)

    // false
    printfn "%A" (waterloo = victoria)
    // true
    printfn "%A" (waterloo = waterloo)
    // false!
    printfn "%A" (waterloo = waterloo2)

module Chapter07_10 =

    type LatLon = {
        Latitude : float
        Longitude : float }

    let waterloo = { Latitude = 51.5031; Longitude = -0.1132 }
    let victoria = { Latitude = 51.4952; Longitude = -0.1441 }
    let waterloo2 = { Latitude = 51.5031; Longitude = -0.1132 }

    // false
    printfn "%A" (waterloo = victoria)
    // true
    printfn "%A" (waterloo = waterloo)
    // true
    printfn "%A" (waterloo = waterloo2)

module Chapter07_11 =

    type Surveyor(name : string) =
        member __.Name = name

    type LatLon = {
        Latitude : float
        Longitude : float
        SurveyedBy : Surveyor }

    let waterloo = 
        { Latitude = 51.5031
          Longitude = -0.1132
          SurveyedBy = Surveyor("Kit") }

    let waterloo2 = 
        { Latitude = 51.5031
          Longitude = -0.1132
          SurveyedBy = Surveyor("Kit") } 

    // true
    printfn "%A" (waterloo = waterloo)
    // false
    printfn "%A" (waterloo = waterloo2)

module Chapter07_12 =

    [<ReferenceEquality>]
    type LatLon = {
        Latitude : float
        Longitude : float }

    let waterloo = { Latitude = 51.5031; Longitude = -0.1132 }
    let waterloo2 = { Latitude = 51.5031; Longitude = -0.1132 }

    // true
    printfn "%A" (waterloo = waterloo)
    // false
    printfn "%A" (waterloo = waterloo2)
                                
module Chapter07_13 =

    type LatLon = {
        Latitude : float
        Longitude : float }

    [<Struct>]
    type LatLonStruct = {
        Latitude : float
        Longitude : float }

    // Uncomment and run in FSI without selecting the module line
    // #time "on"

    // Real: 00:00:00.159, CPU: 00:00:00.156, GC gen0: 5, gen1: 3, gen2: 1
    let llMany = 
        Array.init 1_000_000 (fun x -> 
            { LatLon.Latitude = float x
              LatLon.Longitude = float x } )

    // Real: 00:00:00.046, CPU: 00:00:00.046, GC gen0: 0, gen1: 0, gen2: 0
    let llsMany = 
        Array.init 1_000_000 (fun x -> 
            { LatLonStruct.Latitude = float x
              LatLonStruct.Longitude = float x } )

    // Uncomment and run in FSI without selecting the module line
    // #time "off"
        
module Chapter07_14 =

    [<Struct>]
    type LatLonStruct = {
        mutable Latitude : float
        mutable Longitude : float }

    let waterloo = { Latitude = 51.5031; Longitude = -0.1132 }

    // Error: a value must be mutable in order to mutate the contents.
    //waterloo.Latitude <- 51.5032
      
    let mutable waterloo2 = { Latitude = 51.5031; Longitude = -0.1132 }
    waterloo2.Latitude <- 51.5032
    
module Chapter07_15 =

    type LatLon<'T> = {
        mutable Latitude : 'T
        mutable Longitude : 'T }

    // LatLon<float>
    let waterloo = { Latitude = 51.5031; Longitude = -0.1132 }

    // LatLon<float32>
    let waterloo2 = { Latitude = 51.5031f; Longitude = -0.1132f }

    // Error: Type Mismatch...
    //printfn "%A" (waterloo = waterloo2)

module Chapter07_16 =

    type LatLon<'T> = {
        mutable Latitude : 'T
        mutable Longitude : 'T }

    // LatLon<float>
    let waterloo : LatLon<float> = { 
        Latitude = 51.5031
        Longitude = -0.1132 }

    // Error: The expression was expected to have type 'float32' 
    // but here has type 'float'.
    //let waterloo2 : LatLon<float32> = { 
    //    Latitude = 51.5031f
    //    Longitude = -0.1132 }

module Chapter07_17 =

    type Point = { X : float32; Y : float32 }

    type UiControl = {
        Name : string
        Position : Point
        Parent : UiControl option }

    let form = {
        Name = "MyForm"
        Position = { X = 0.f; Y = 0.f }
        Parent = None }

    let button = {
        Name = "MyButton"
        Position = { X = 10.f; Y = 20.f }
        Parent = Some form }

module Chapter07_18 =

    // You probably don't want to do this!

    type Point = { X : float32; Y : float32 }

    type UiControl = {
        Name : string
        Position : Point
        Parent : UiControl }

    let rec form = {
        Name = "MyForm"
        Position =  { X = 0.f; Y = 0.f }
        Parent = button }
    and button = {
        Name = "MyButton"
        Position =  { X = 10.f; Y = 20.f }
        Parent = form }

module Chapter07_19 =

    type LatLon = {
        Latitude : float
        Longitude : float }
    with
        // Naive, straight-line distance
        member this.DistanceFrom(other : LatLon) =
            let milesPerDegree = 69.
            ((other.Latitude - this.Latitude) ** 2.)
            +
            ((other.Longitude - this.Longitude) ** 2.)
            |> sqrt
            |> (*) milesPerDegree

    let coleman = { 
        Latitude = 31.82
        Longitude = -99.42 }

    let abilene = {
        Latitude = 32.45
        Longitude = -99.75 }

    // Are we going to Abilene? Because it's 49 miles!
    printfn "Are we going to Abilene? because it's %0.0f miles!"
        (abilene.DistanceFrom(coleman))
       
module Chapter07_20 =

    open System

    type LatLon = {
        Latitude : float
        Longitude : float }
        with
            static member TryFromString(s : string) =
                match s.Split([|','|]) with
                | [|lats; lons|] ->
                    match (Double.TryParse(lats), 
                            Double.TryParse(lons)) with
                    | (true, lat), (true, lon) ->
                        { Latitude = lat
                          Longitude = lon } |> Some
                    | _ -> None
                | _ -> None

    // Some {Latitude = 50.514444;
    //       Longitude = -2.457222;}
    let somewhere = LatLon.TryFromString "50.514444, -2.457222"

    // None
    let nowhere = LatLon.TryFromString "hullo trees"
                
module Chapter07_21 =

    type LatLon = {
        Latitude : float
        Longitude : float }
        with     
            override this.ToString() =
                sprintf "%f, %f" this.Latitude this.Longitude

    // 51.972300, 1.149700
    { Latitude = 51.9723
      Longitude = 1.1497 }
    |> printfn "%O"

module Chapter07_22 =

    // Declaration:

    // Good 
    type LatLon1 = { Lat : float; Lon : float }

    // Good
    type LatLon2 = 
        { Latitude : float
          Longitude : float }

    // Good
    type LatLon3 = { 
        Latitude : float
        Longitude : float }

    // Bad - needless semi-colons
    type LatLon4 = {
        Latitude : float;
        Longitude : float }

    // Bad - mixed newline style
    type Position = { Lat : float; Lon : float 
                      Altitude : float }

    // Instantiation:

    // Good
    let ll1 = { Lat = 51.9723; Lon = 1.1497 }

    // Good
    let ll2 = 
        { Latitude = 51.9723
          Longitude = 1.1497 }

    // Bad - needless semi-colons
    let ll3 = 
        { Latitude = 51.9723;
            Longitude = 1.1497 }

    // Bad - mixed newline style
    let position = { Lat = 51.9723; Lon = 1.1497 
                     Altitude = 22.3 }
