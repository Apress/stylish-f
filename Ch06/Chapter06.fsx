module Chapter06_01 =
    
    // C#

    //int caseSwitch = 1;
      
    //switch (caseSwitch)
    //{
    //    case 1:
    //        Console.WriteLine("Case 1");
    //        break;
    //    case 2:
    //        Console.WriteLine("Case 2");
    //        break;
    //    default:
    //        Console.WriteLine("Default case");
    //        break;
    //}
    ()

module Chapter06_02 =

    let caseSwitch = 2

    match caseSwitch with
    | 1 -> printfn "Case 1"
    | 2 -> printfn "Case 2"
    | _ -> printfn "Default case"

module Chapter06_03 =

    let caseSwitch = 3

    // "Maybe 3, maybe 4"
    match caseSwitch with
    | 1 -> printfn "Case 1"
    | 2 -> printfn "Case 2"
    | 3
    | 4 -> printfn "Maybe 3, maybe 4"
    | _ -> printfn "Default case"

module Chapter06_04 =

    let caseSwitch = 3

    // "Maybe 3, maybe 4. But actually 3."
    match caseSwitch with
    | 1 -> printfn "Case 1"
    | 2 -> printfn "Case 2"
    | 3
    | 4 as x -> printfn "Maybe 3, maybe 4. But actually %i." x
    | _ -> printfn "Default case"

module Chapter06_05 =

    let caseSwitch = 11

    // "Less than a dozen"
    match caseSwitch with
    | 1 -> printfn "One"
    | 2 -> printfn "A couple"
    | x when x < 12 ->
        printfn "Less than a dozen"
    | x when x = 12 ->
        printfn "A dozen"
    | _ ->
        printfn "More than a dozen"

module Chapter06_06 = 

    let arr0 = [||]
    let arr1 = [|"One fish" |]
    let arr2 = [|"One fish"; "Two fish" |]
    let arr3 = [|"One fish"; "Two fish"; "Red fish" |]
    let arr4 = [|"One fish"; "Two fish"; "Red fish"; "Blue fish" |]

    let arr = arr1

    // "A pond containing one fish: One fish"
    match arr with
    | [||] -> 
        "An empty pond"
    | [| fish |] -> 
        sprintf "A pond containing one fish: %s" fish
    | [| f1; f2 |] -> 
        sprintf "A pond containing two fish: %s and %s" f1 f2
    | _ ->
        "Too many fish to list!"

module Chapter06_07 = 

    let list0 = []
    let list1 = ["One fish" ]
    let list2 = ["One fish"; "Two fish" ]
    let list3 = ["One fish"; "Two fish"; "Red fish" ]
    let list4 = ["One fish"; "Two fish"; "Red fish"; "Blue fish" ]

    let list = list4

    // "A pond containing one fish: One fish (and 3 more fish)"
    match list with
    | [] -> 
        "An empty pond"
    | [ fish ] -> 
        sprintf "A pond containing one fish only: %s" fish
    | head::tail -> 
        sprintf "A pond containing one fish: %s (and %i more fish)" 
            head (tail |> List.length)

module Chapter06_08 = 

    let extremes (s : seq<_>) =
        s |> Seq.min,
        s |> Seq.max

    // lowest : int = -1
    // highest : int = 9
    let lowest, highest = 
        [1; 2; 9; 3; -1] |> extremes

module Chapter06_09 = 

    open System

    let tryParseInt s =
        match Int32.TryParse(s) with
        | true, i -> Some i
        | false, _ -> None

    // Some 30
    "30" |> tryParseInt
    // None
    "3X" |> tryParseInt

module Chapter06_10 = 

    type Track = { Title : string; Artist : string }

    let songs =
        [ { Title = "Summertime"
            Artist = "Ray Barretto" }
          { Title = "La clave, maraca y guiro"
            Artist = "Chico Alvarez" }
          { Title = "Summertime"
            Artist = "DJ Jazzy Jeff & The Fresh Prince" } ]

    // seq ["Summertime"; "La clave, maraca y guiro"]
    let distinctTitles =
        songs
        |> Seq.map (fun song -> 
            match song with 
            | { Title = title } -> title)
        |> Seq.distinct

module Chapter06_11 = 

    type Track = { Id : int
                   Title : string
                   Artist : string
                   Length : int }

    let songs =
        [ { Id = 1
            Title = "Summertime"
            Artist = "Ray Barretto"
            Length = 99 }
          { Id = 2
            Title = "La clave, maraca y guiro"
            Artist = "Chico Alvarez"
            Length = 99 }
          { Id = 3
            Title = "Summertime"
            Artist = "DJ Jazzy Jeff & The Fresh Prince"
            Length = 99 } ]

    let formatMenuItem ( { Title = title; Artist = artist } ) =
        let shorten (s : string) = s.Substring(0, 10)
        sprintf "%s - %s" (shorten title) (shorten artist)

    // Summertime - Ray Barret
    // La clave,  - Chico Alva
    // Summertime - DJ Jazzy J
    songs
    |> Seq.map formatMenuItem
    |> Seq.iter (printfn "%s")
    
module Chapter06_12 = 

    type MeterReading =
    | Standard of int
    | Economy7 of Day:int * Night:int

    let formatReading (reading : MeterReading) =
        match reading with
        | Standard reading ->
            sprintf "Your reading: %07i" reading
        | Economy7(Day=day; Night=night) ->
            sprintf "Your readings: Day: %07i Night: %07i" day night

    let reading1 = Standard 12982
    let reading2 = Economy7(Day=3432, Night=98218)

    reading1 |> formatReading
    reading2 |> formatReading

module Chapter06_13 = 

    type MeterReading =
    | Standard of int
    | Economy7 of int * int

    let formatReading (reading : MeterReading) =
        match reading with
        | Standard reading ->
            sprintf "Your reading: %07i" reading
        | Economy7(day, night) ->
            sprintf "Your readings: Day: %07i Night: %07i" day night

    let reading1 = Standard 12982
    let reading2 = Economy7(3432, 98218)

    reading1 |> formatReading
    reading2 |> formatReading

module Chapter06_14 =

    type Complex = 
    | Complex of Real:float * Imaginary:float

    let add (Complex(Real=a; Imaginary=b)) (Complex(Real=c; Imaginary=d)) =
        Complex(Real=(a+c), Imaginary=(b+d))

    let (+) = add
            
    let c1 = Complex(Real = 0.2, Imaginary = 3.4)
    let c2 = Complex(Real = 2.2, Imaginary = 9.8)

    // Complex (2.4,13.2)
    let c3 = c1 + c2

module Chapter06_15 =

    type Number = 
    | Real of float
    | Complex of Real:float * Imaginary:float
        
    // Warning: Incomplete pattern matches on this expression.
    let add (Complex(Real=a; Imaginary=b)) (Complex(Real=c; Imaginary=d)) =
        Complex(Real=(a+c), Imaginary=(b+d))

module Chapter06_16 =

    type Number = 
    | Real of float
    | Complex of Real:float * Imaginary:float
       
    let addReal (Complex(Real=a)|Real(a)) (Complex(Real=b)|Real(b)) = 
        Real(a+b)

module Chapter06_17 =

    type Complex = 
    | Complex of Real:float * Imaginary:float

    let c1 = Complex(Real = 0.2, Imaginary = 3.4)

    let (Complex(real, imaginary)) = c1

    // 0.200000, 3.400000
    printfn "%f, %f" real imaginary

module Chapter06_18 =

    type Complex = 
    | Real of float
    | Complex of Real:float * Imaginary:float

    let c1 = Complex(Real = 0.2, Imaginary = 3.4)

    let (Complex(real, _)|Real (real)) = c1

module Chapter06_19 =

    let fruits =
        [ "Apples", 3
          "Oranges", 4
          "Bananas", 2 ]

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    for (name, count) in fruits do
        printfn "There are %i %s" count name

    // There are 3 Apples
    // There are 4 Oranges
    // There are 2 Bananas
    fruits
    |> List.iter (fun (name, count) ->
        printfn "There are %i %s" count name)
                
module Chapter06_20 =
            
    type Shape =
    | Circle of Radius:float
    | Square of Length:float
    | Rectangle of Length:float * Height:float

    let shapes =
        [ Circle 3.
          Square 4.
          Rectangle(5., 6.)
          Circle 4. ]

    // Circle of radius 3.000000
    // Circle of radius 4.000000
    for (Circle r) in shapes do
        printfn "Circle of radius %f" r

module Chapter06_21 =

    open System

    [<Flags>]
    type FileMode =
    | None =    0uy
    | Read =    4uy
    | Write =   2uy
    | Execute = 1uy

    let canRead (fileMode : FileMode) =
        fileMode.HasFlag FileMode.Read

    let modea = FileMode.Read
    let modeb = FileMode.Write
    let modec = modea ^^^ modeb

    // True
    canRead (modea)
    // False
    canRead (modeb)
    // True
    canRead (modec)

    open Microsoft.FSharp.Core.LanguagePrimitives

    let naughtyMode =
        EnumOfValue<byte, FileMode> 255uy

module Chapter06_22 =
    
    open System

    [<Flags>]
    type FileMode =
    | None =    0uy
    | Read =    4uy
    | Write =   2uy
    | Execute = 1uy
        
    let describe (fileMode : FileMode) =
        let read = 
            // Compiler warning: Incomplete pattern matches...
            match fileMode with 
                | FileMode.None -> "cannot"
                | FileMode.Read -> "can" 
                | FileMode.Write -> "cannot"
                | FileMode.Execute -> "cannot"

        printfn "You %s read the file"

module Chapter06_23 =

    open System

    let (|Currency|) (x : float) =
        Math.Round(x, 2)

    // true
    match 100./3. with
    | Currency 33.33 -> true
    | _ -> false

module Chapter06_24 =

    open System

    let (|Currency|) (x : float) =
        Math.Round(x, 2)

    // "That didn't match: 33.330000"
    // false
    match 100./3. with
    | Currency 33.34 -> true
    | Currency c -> 
        printfn "That didn't match: %f" c
        false

    // Cs: 33.330000
    let (Currency c) = 1000./30.
    printfn "Cs: %f" c

    let add (Currency c1) (Currency c2) =
        c1 + c2

    // 66.66
    add (100./3.) (1000./30.)

module Chapter06_25 =

    open System.Text.RegularExpressions

    let (|Mitsubishi|Samsung|Other|) (s : string) = 
        let m = Regex.Match(s, @"([A-Z]{3})(\-?)(.*)")
        if m.Success then
            match m.Groups.[1].Value with
            | "MWT" -> Mitsubishi
            | "SWT" -> Samsung
            | _     -> Other
        else
            Other

    // From https://eerscmap.usgs.gov/uswtdb/
    let turbines = [
        "MWT1000"; "MWT1000A"; "MWT102/2.4"; "MWT57/1.0"
        "SWT1.3_62"; "SWT2.3_101"; "SWT2.3_93"; "SWT-2.3-101"
        "40/500" ]

    turbines
    |> Seq.iter (fun t ->
        match t with
        | Mitsubishi -> printfn "%s is a Mitsubshi turbine" t
        | Samsung ->    printfn "%s is a Samsung turbine" t
        | Other ->      printfn "%s is an unknown turbine" t)

module Chapter06_26 =

    open System.Text.RegularExpressions

    let (|Mitsubishi|_|) (s : string) = 
        let m = Regex.Match(s, @"([A-Z]{3})(\-?)(.*)")
        if m.Success then
            match m.Groups.[1].Value with
            | "MWT" -> Some()
            | _     -> None
        else
            None

    // From https://eerscmap.usgs.gov/uswtdb/
    let turbines = [
        "MWT1000"; "MWT1000A"; "MWT102/2.4"; "MWT57/1.0"
        "SWT1.3_62"; "SWT2.3_101"; "SWT2.3_93"; "SWT-2.3-101"
        "40/500" ]

    turbines
    |> Seq.iter (fun t -> 
        match t with
        | Mitsubishi as m -> printfn "%s is a Mitsubishi turbine" m
        | _ as m ->          printfn "%s is not a Mitsubishi turbine" m)

    // Alternative using 'function' syntax:
    turbines
    |> Seq.iter (function
        | Mitsubishi as m -> printfn "%s is a Mitsubishi turbine" m
        | _ as m ->          printfn "%s is not a Mitsubishi turbine" m)

module Chapter06_27 =

    open System
    open System.Text.RegularExpressions

    let zipCodes = [ "90210"; "94043"; "10013"; "1OO13" ]
    let postCodes = [ "SW1A 1AA"; "GU9 0RA"; "PO8 0AB"; "P 0AB" ]

    let regexZip = @"^\d{5}$"
    // Simplified: the official regex for UK postcodes is much longer!
    let regexPostCode = @"^(\d|[A-Z]){2,4} (\d|[A-Z]){3}"
        
    let (|PostalCode|) pattern s =
        let m = Regex.Match(s, pattern)
        if m.Success then
            Some s
        else
            None

    // None
    let (PostalCode regexZip z) = "WRONG"

    // ["90210"; "94043"; "10013"]
    let validZipCodes =
        zipCodes
        |> List.choose (fun (PostalCode regexZip p) -> p)

    // ["SW1A 1AA"; "GU9 0RA"; "PO8 0AB"]
    let validPostCodes =
        postCodes
        |> List.choose (fun (PostalCode regexPostCode p) -> p)

module Chapter06_28 =

    open System.Text.RegularExpressions

    let (|PostCode|) s =
        let m = Regex.Match(s, @"^(\d|[A-Z]){2,4} (\d|[A-Z]){3}")
        if m.Success then
            Some s
        else
            None

    let outerLondon = 
        ["BR";"CR";"DA";"EN";"HA";"IG";"KT";"RM";"SM";"TW";"UB";"WD"]

    let (|OuterLondon|) (s : string) =
        outerLondon
        |> List.tryFind (s.StartsWith)

    let promotionAvailable (postcode : string) =
        match postcode with
        | PostCode(Some p) & OuterLondon(Some o) ->
            printfn "We can offer the promotion in %s (%s)" p o
        | PostCode(Some p) & OuterLondon(None) ->
            printfn "We cannot offer the promotion in %s" p
        | _ ->
            printfn "Invalid postcode"

    let demo() =
        // "We cannot offer the promotion in RG7 1DP"
        "RG7 1DP" |> promotionAvailable
        // "We can offer the promotion in RM3 5NA (RM)"
        "RM3 5NA" |> promotionAvailable
        // "Invalid postcode"
        "Hullo sky" |> promotionAvailable

module Chapter06_29 =

    type Person (name : string) =
        member __.Name = name

    type Child(name, parent : Person) = 
        inherit Person(name)
        member __.ParentName =
            parent.Name

    let alice = Person("Alice")
    let bob = Child("Bob", alice)

    let people = [ alice; bob :> Person ]

    // Person: Alice
    // Child: Bob of parent Alice
    people
    |> List.iter (fun person ->
        match person with
        | :? Child as child ->
            printfn "Child: %s of parent %s" child.Name child.ParentName
        | _ as person ->
            printfn "Person: %s" person.Name)

module Chapter06_30 =

    let myApiFunction (stringParam : string) =
        let s = 
            stringParam
            |> Option.ofObj 
            |> Option.defaultValue "(none)"
            
        // You can do things here knowing that s isn't null
        sprintf "%s" (s.ToUpper())

    // HELLO
    myApiFunction "hello"

    // (NONE)
    myApiFunction null

module Chapter06_31 =

    let myApiFunction (stringParam : string) =

        match stringParam with
        | null -> "(NONE)"
        | _ -> stringParam.ToUpper()

    // HELLO
    myApiFunction "hello"

    // (NONE)
    myApiFunction null





        

