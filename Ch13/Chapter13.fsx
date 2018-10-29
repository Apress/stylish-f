module Chapter13_03_04  = 

    module MinorPlanets = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let toDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

        let toChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then None
            else
                Some(s.[0])
        
        let toInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

        let columnAsString startInd endInd (line : string) =
            line.Substring(startInd-1,endInd-startInd+1).Trim()

        let columnAsCharArray startInd endInd (line : string) =
            charArray(columnAsString startInd endInd line)

        let columnAsInt startInd endInd (line : string) =
            toInt(columnAsString startInd endInd line)

        let columnAsDouble startInd endInd (line : string) =
            toDouble(columnAsString startInd endInd line)

        let columnAsChar startInd endInd (line : string) =
            toChar(columnAsString startInd endInd line)

        type ObservationRange =
            private
                | SingleOpposition of int 
                | MultiOpposition of int * int

        let rangeFromLine (oppositions : int option) (line : string) =
            match oppositions with
            | None -> None
            | Some o when o = 1 ->
                line |> columnAsInt 128 131
                |> Option.map SingleOpposition 
            | Some o ->
                match (line |> columnAsInt 128 131), 
                      (line |> columnAsInt 133 136) with
                | Some(firstObservedYear), Some(lastObservedYear) ->
                    MultiOpposition(firstObservedYear, 
                       lastObservedYear) |> Some
                | _ -> None   

        type MinorPlanet = {
            Designation : string; AbsMag : float option
            SlopeParam : float option; Epoch : string
            MeanAnom : float option; Perihelion : float option
            Node : float option; Inclination : float option
            OrbEcc : float option; MeanDaily : float option
            SemiMajor : float option; Uncertainty : char option
            Reference : string; Observations : int option
            Oppositions : int option; Range : ObservationRange option
            RmsResidual : double option; PerturbersCoarse : string
            PerturbersPrecise : string; ComputerName : string
            Flags : char[]; ReadableDesignation : string
            LastOpposition : string }

        let private create (line : string) =

            let oppositions = line |> columnAsString  124 126 |> toInt
            let range = line |> rangeFromLine oppositions

            { Designation = columnAsString  1 7 line 
              AbsMag = columnAsDouble 9 13 line 
              SlopeParam = columnAsDouble 15 19 line 
              Epoch = columnAsString  21 25 line 
              MeanAnom = columnAsDouble 27 35 line 
              Perihelion = columnAsDouble 38 46 line 
              Node = columnAsDouble 49 57 line 
              Inclination = columnAsDouble 60 68 line 
              OrbEcc = columnAsDouble 71 79 line 
              MeanDaily = columnAsDouble 81 91 line 
              SemiMajor = columnAsDouble 93 103 line 
              Uncertainty = columnAsChar 106 106 line 
              Reference = columnAsString 108 116 line 
              Observations = columnAsInt 118 122 line 
              Oppositions = oppositions
              Range = range
              RmsResidual = columnAsDouble 138 141 line 
              PerturbersCoarse = columnAsString 143 145 line 
              PerturbersPrecise = columnAsString 147 149 line 
              ComputerName = columnAsString 151 160 line 
              Flags = columnAsCharArray 162 165 line 
              ReadableDesignation = columnAsString 167 194 line 
              LastOpposition = columnAsString 195 202 line 
            }

        let createFromData (data : seq<string>) =
            data
            |> Seq.skipWhile (fun line -> 
                                            line.StartsWith("----------") 
                                            |> not) |> Seq.skip 1
            |> Seq.filter (fun line -> 
                                line.Length > 0)
            |> Seq.map (fun line -> create line)

    open System.IO

    let demo() =

        // Brightest 10 minor planets (absolute magnitude)

        // Edit the path to reflect where you stored the file:
        @"C:\Data\MinorPlanets\MPCORB.DAT"
        |> File.ReadLines
        |> MinorPlanets.createFromData
        |> Seq.filter (fun mp -> 
            mp.AbsMag |> Option.isSome)
        |> Seq.sortBy (fun mp -> 
            mp.AbsMag.Value)
        |> Seq.truncate 10
        |> Seq.iter (fun mp ->
            printfn "Name: %s Abs. magnitude: %0.2f" 
                mp.ReadableDesignation (mp.AbsMag |> Option.defaultValue nan))

module Chapter13_05 =

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then None
            else
                Some(s.[0])
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

module Chapter13_06 =

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then None
            else
                Some(s.[0])
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

    module Column =

        let asString startInd endInd (line : string) =
            line.Substring(startInd-1,endInd-startInd+1).Trim()

        let asCharArray startInd endInd (line : string) =
            Convert.charArray(asString startInd endInd line)

        let tryAsInt startInd endInd (line : string) =
            Convert.tryInt(asString startInd endInd line)

        let tryAsDouble startInd endInd (line : string) =
            Convert.tryDouble(asString startInd endInd line)

        let tryAsChar startInd endInd (line : string) =
            Convert.tryChar(asString startInd endInd line)

module Chapter13_07 =

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then None
            else
                Some(s.[0])
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, x -> None

    module Column =

        let asString startInd endInd (line : string) =
            let len = endInd - startInd + 1
            line
                .Substring(startInd-1, len)
                .Trim()

        let asCharArray startInd endInd =
            (asString startInd endInd) >> Convert.charArray

        let tryAsInt startInd endInd =
            (asString startInd endInd) >> Convert.tryInt

        let tryAsDouble startInd endInd =
            (asString startInd endInd) >> Convert.tryDouble

        let tryAsChar startInd endInd =
            (asString startInd endInd) >> Convert.tryChar

module Chapter13_09 =

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then
                None
            else
                s.[0] |> Some
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

    module Column = 

        let asString startInd endInd (line : string) =
            let len = endInd - startInd + 1
            line
                .Substring(startInd-1, len)
                .Trim()

        let asCharArray startInd endInd =
            (asString startInd endInd) >> Convert.charArray

        let tryAsInt startInd endInd =
            (asString startInd endInd) >> Convert.tryInt

        let tryAsDouble startInd endInd =
            (asString startInd endInd) >> Convert.tryDouble

        let tryAsChar startInd endInd =
            (asString startInd endInd) >> Convert.tryChar

    module ObservationRange = 

        type Range =
            private
                | SingleOpposition of ArcLengthDays:int 
                | MultiOpposition of FirstYear:int * LastYear:int

        let fromLine (oppositions : int option) (line : string) =
            match oppositions with
            | None -> 
                None
            | Some o when o = 1 ->
                line 
                |> Column.tryAsInt 128 131
                |> Option.map SingleOpposition 
            | Some _ ->
                let firstYear = line |> Column.tryAsInt 128 131
                let lastYear = line |> Column.tryAsInt 133 136
                match firstYear, lastYear with
                | Some(fy), Some(ly) ->
                    MultiOpposition(FirstYear=fy, LastYear=ly) |> Some
                | _ ->
                    None 

module Chapter13_11 =

    module ObservationRange = 

        type Range =
            private
                | SingleOpposition of ArcLengthDays:int 
                | MultiOpposition of FirstYear:int * LastYear:int

    module MinorPlanet =

        type Body = {
            /// Number or provisional designation (packed format)
            Designation : string
            /// Absolute magnitude
            H : float option
            /// Slope parameter
            G : float option
            /// Epoch in packed form
            Epoch : string
            /// Mean anomaly at the epoch (degrees)
            M : float option
            /// Argument of perihelion, J2000.0 (degrees)
            Perihelion : float option
            /// Longitude of the ascending node, J2000.0 (degrees)
            Node : float option
            /// Inclination to the ecliptic, J2000.0 (degrees)
            Inclination : float option
            /// Orbital eccentricity
            e : float option
            /// Mean daily motion (degrees per day)
            n : float option
            /// Semimajor axis (AU)
            a : float option
            /// Uncertainty parameter
            Uncertainty : char option
            /// Reference
            Reference : string
            /// Number of observations
            Observations : int option
            /// Number of oppositions
            Oppositions : int option
            /// Year of first and last observation,
            /// or arc length in days.
            Range : ObservationRange.Range option
            /// RMS residual (arcseconds)
            RmsResidual : double option
            /// Coarse indicator of perturbers
            PerturbersCoarse : string
            /// Precise indicator of perturbers
            PerturbersPrecise : string
            /// Computer name
            ComputerName : string
            /// Flags
            Flags : char[]
            /// Readable designation
            ReadableDesignation : string
            /// Date of last observation included in orbit solution (YYYYMMDD)
            LastOpposition : string }

module Chapter13_12 = 

    let eccentricity ϵ h μ =
        1. + ((2. * ϵ * h * h) / (μ * μ))
        |> sqrt 

module Chapter13_13 = 

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then
                None
            else
                s.[0] |> Some
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

    module Column = 

        let asString startInd endInd (line : string) =
            let len = endInd - startInd + 1
            line
                .Substring(startInd-1, len)
                .Trim()

        let asCharArray startInd endInd =
            (asString startInd endInd) >> Convert.charArray

        let tryAsInt startInd endInd =
            (asString startInd endInd) >> Convert.tryInt

        let tryAsDouble startInd endInd =
            (asString startInd endInd) >> Convert.tryDouble

        let tryAsChar startInd endInd =
            (asString startInd endInd) >> Convert.tryChar

    module ObservationRange = 

        type Range =
            private
                | SingleOpposition of ArcLengthDays:int 
                | MultiOpposition of FirstYear:int * LastYear:int

        let fromLine (oppositions : int option) (line : string) =
            match oppositions with
            | None -> 
                None
            | Some o when o = 1 ->
                line 
                |> Column.tryAsInt 128 131
                |> Option.map SingleOpposition 
            | Some _ ->
                let firstYear = line |> Column.tryAsInt 128 131
                let lastYear = line |> Column.tryAsInt 133 136
                match firstYear, lastYear with
                | Some(fy), Some(ly) ->
                    MultiOpposition(FirstYear=fy, LastYear=ly) |> Some
                | _ ->
                    None   

    module MinorPlanet =

        type Body = {
            /// Number or provisional designation (packed format)
            Designation : string
            /// Absolute magnitude
            H : float option
            /// Slope parameter
            G : float option
            /// Epoch in packed form
            Epoch : string
            /// Mean anomaly at the epoch (degrees)
            M : float option
            /// Argument of perihelion, J2000.0 (degrees)
            Perihelion : float option
            /// Longitude of the ascending node, J2000.0 (degrees)
            Node : float option
            /// Inclination to the ecliptic, J2000.0 (degrees)
            Inclination : float option
            /// Orbital eccentricity
            e : float option
            /// Mean daily motion (degrees per day)
            n : float option
            /// Semimajor axis (AU)
            a : float option
            /// Uncertainty parameter
            Uncertainty : char option
            /// Reference
            Reference : string
            /// Number of observations
            Observations : int option
            /// Number of oppositions
            Oppositions : int option
            /// Year of first and last observation,
            /// or arc length in days.
            Range : ObservationRange.Range option
            /// RMS residual (arcseconds)
            RmsResidual : double option
            /// Coarse indicator of perturbers
            PerturbersCoarse : string
            /// Precise indicator of perturbers
            PerturbersPrecise : string
            /// Computer name
            ComputerName : string
            /// Flags
            Flags : char[]
            /// Readable designation
            ReadableDesignation : string
            /// Date of last observation included in orbit solution (YYYYMMDD)
            LastOpposition : string }

        let private fromMpcOrbLine (line : string) =

            let oppositions = line |> Column.asString 124 126 |> Convert.tryInt
            let range = line |> ObservationRange.fromLine oppositions

            { Designation =         line |> Column.asString      1   7
              H =                   line |> Column.tryAsDouble   9  13
              G =                   line |> Column.tryAsDouble  15  19 
              Epoch =               line |> Column.asString     21  25
              M =                   line |> Column.tryAsDouble  27  35 
              Perihelion =          line |> Column.tryAsDouble  38  46 
              Node =                line |> Column.tryAsDouble  49  57 
              Inclination =         line |> Column.tryAsDouble  60  68 
              e =                   line |> Column.tryAsDouble  71  79 
              n =                   line |> Column.tryAsDouble  81  91 
              a =                   line |> Column.tryAsDouble  93 103  
              Uncertainty =         line |> Column.tryAsChar   106 106
              Reference =           line |> Column.asString    108 116
              Observations =        line |> Column.tryAsInt    118 122
              Oppositions =         oppositions
              Range =               range
              RmsResidual =         line |> Column.tryAsDouble 138 141
              PerturbersCoarse =    line |> Column.asString    143 145
              PerturbersPrecise =   line |> Column.asString    147 149
              ComputerName =        line |> Column.asString    151 160
              Flags =               line |> Column.asCharArray 162 165
              ReadableDesignation = line |> Column.asString    167 194
              LastOpposition =      line |> Column.asString    195 202 }

module Chapter13_15 = 

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then
                None
            else
                s.[0] |> Some
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

    module Column = 

        let asString startInd endInd (line : string) =
            let len = endInd - startInd + 1
            line
                .Substring(startInd-1, len)
                .Trim()

        let asCharArray startInd endInd =
            (asString startInd endInd) >> Convert.charArray

        let tryAsInt startInd endInd =
            (asString startInd endInd) >> Convert.tryInt

        let tryAsDouble startInd endInd =
            (asString startInd endInd) >> Convert.tryDouble

        let tryAsChar startInd endInd =
            (asString startInd endInd) >> Convert.tryChar

    module ObservationRange = 

        type Range =
            private
                | SingleOpposition of ArcLengthDays:int 
                | MultiOpposition of FirstYear:int * LastYear:int

        let fromLine (oppositions : int option) (line : string) =
            match oppositions with
            | None -> 
                None
            | Some o when o = 1 ->
                line 
                |> Column.tryAsInt 128 131
                |> Option.map SingleOpposition 
            | Some _ ->
                let firstYear = line |> Column.tryAsInt 128 131
                let lastYear = line |> Column.tryAsInt 133 136
                match firstYear, lastYear with
                | Some(fy), Some(ly) ->
                    MultiOpposition(FirstYear=fy, LastYear=ly) |> Some
                | _ ->
                    None   

    module MinorPlanet =

        type Body = {
            /// Number or provisional designation (packed format)
            Designation : string
            /// Absolute magnitude
            H : float option
            /// Slope parameter
            G : float option
            /// Epoch in packed form
            Epoch : string
            /// Mean anomaly at the epoch (degrees)
            M : float option
            /// Argument of perihelion, J2000.0 (degrees)
            Perihelion : float option
            /// Longitude of the ascending node, J2000.0 (degrees)
            Node : float option
            /// Inclination to the ecliptic, J2000.0 (degrees)
            Inclination : float option
            /// Orbital eccentricity
            e : float option
            /// Mean daily motion (degrees per day)
            n : float option
            /// Semimajor axis (AU)
            a : float option
            /// Uncertainty parameter
            Uncertainty : char option
            /// Reference
            Reference : string
            /// Number of observations
            Observations : int option
            /// Number of oppositions
            Oppositions : int option
            /// Year of first and last observation,
            /// or arc length in days.
            Range : ObservationRange.Range option
            /// RMS residual (arcseconds)
            RmsResidual : double option
            /// Coarse indicator of perturbers
            PerturbersCoarse : string
            /// Precise indicator of perturbers
            PerturbersPrecise : string
            /// Computer name
            ComputerName : string
            /// Flags
            Flags : char[]
            /// Readable designation
            ReadableDesignation : string
            /// Date of last observation included in orbit solution (YYYYMMDD)
            LastOpposition : string }

        let fromMpcOrbLine (line : string) =

            let oppositions = line |> Column.asString 124 126 |> Convert.tryInt
            let range = line |> ObservationRange.fromLine oppositions

            { Designation =         line |> Column.asString      1   7
              H =                   line |> Column.tryAsDouble   9  13
              G =                   line |> Column.tryAsDouble  15  19 
              Epoch =               line |> Column.asString     21  25
              M =                   line |> Column.tryAsDouble  27  35 
              Perihelion =          line |> Column.tryAsDouble  38  46 
              Node =                line |> Column.tryAsDouble  49  57 
              Inclination =         line |> Column.tryAsDouble  60  68 
              e =                   line |> Column.tryAsDouble  71  79 
              n =                   line |> Column.tryAsDouble  81  91 
              a =                   line |> Column.tryAsDouble  93 103  
              Uncertainty =         line |> Column.tryAsChar   106 106
              Reference =           line |> Column.asString    108 116
              Observations =        line |> Column.tryAsInt    118 122
              Oppositions =         oppositions
              Range =               range
              RmsResidual =         line |> Column.tryAsDouble 138 141
              PerturbersCoarse =    line |> Column.asString    143 145
              PerturbersPrecise =   line |> Column.asString    147 149
              ComputerName =        line |> Column.asString    151 160
              Flags =               line |> Column.asCharArray 162 165
              ReadableDesignation = line |> Column.asString    167 194
              LastOpposition =      line |> Column.asString    195 202 }

        let private skipHeader (data : seq<string>) =
            data
            |> Seq.skipWhile (fun line -> 
                line.StartsWith("----------") |> not)
            |> Seq.skip 1

        let fromMpcOrbData (data : seq<string>) =
            data
            |> skipHeader
            |> Seq.filter (fun line -> line.Length > 0)
            |> Seq.map fromMpcOrbLine

module Chapter13_16 = 

    module Convert = 

        open System

        let charArray (s : string) =
            s.ToCharArray()

        let tryDouble (s : string) =
            match Double.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

        let tryChar (s : string) =
            if String.IsNullOrWhiteSpace(s) then
                None
            else
                s.[0] |> Some
        
        let tryInt (s : string) =
            match Int32.TryParse(s) with 
            | true, x -> Some x
            | false, _ -> None

    module Column = 

        let asString startInd endInd (line : string) =
            let len = endInd - startInd + 1
            line
                .Substring(startInd-1, len)
                .Trim()

        let asCharArray startInd endInd =
            (asString startInd endInd) >> Convert.charArray

        let tryAsInt startInd endInd =
            (asString startInd endInd) >> Convert.tryInt

        let tryAsDouble startInd endInd =
            (asString startInd endInd) >> Convert.tryDouble

        let tryAsChar startInd endInd =
            (asString startInd endInd) >> Convert.tryChar

    module ObservationRange = 

        type Range =
            private
                | SingleOpposition of ArcLengthDays:int 
                | MultiOpposition of FirstYear:int * LastYear:int

        let fromLine (oppositions : int option) (line : string) =
            match oppositions with
            | None -> 
                None
            | Some o when o = 1 ->
                line 
                |> Column.tryAsInt 128 131
                |> Option.map SingleOpposition 
            | Some _ ->
                let firstYear = line |> Column.tryAsInt 128 131
                let lastYear = line |> Column.tryAsInt 133 136
                match firstYear, lastYear with
                | Some(fy), Some(ly) ->
                    MultiOpposition(FirstYear=fy, LastYear=ly) |> Some
                | _ ->
                    None   

    module MinorPlanet =

        type Body = {
            /// Number or provisional designation (packed format)
            Designation : string
            /// Absolute magnitude
            H : float option
            /// Slope parameter
            G : float option
            /// Epoch in packed form
            Epoch : string
            /// Mean anomaly at the epoch (degrees)
            M : float option
            /// Argument of perihelion, J2000.0 (degrees)
            Perihelion : float option
            /// Longitude of the ascending node, J2000.0 (degrees)
            Node : float option
            /// Inclination to the ecliptic, J2000.0 (degrees)
            Inclination : float option
            /// Orbital eccentricity
            e : float option
            /// Mean daily motion (degrees per day)
            n : float option
            /// Semimajor axis (AU)
            a : float option
            /// Uncertainty parameter
            Uncertainty : char option
            /// Reference
            Reference : string
            /// Number of observations
            Observations : int option
            /// Number of oppositions
            Oppositions : int option
            /// Year of first and last observation,
            /// or arc length in days.
            Range : ObservationRange.Range option
            /// RMS residual (arcseconds)
            RmsResidual : double option
            /// Coarse indicator of perturbers
            PerturbersCoarse : string
            /// Precise indicator of perturbers
            PerturbersPrecise : string
            /// Computer name
            ComputerName : string
            /// Flags
            Flags : char[]
            /// Readable designation
            ReadableDesignation : string
            /// Date of last observation included in orbit solution (YYYYMMDD)
            LastOpposition : string }

        let fromMpcOrbLine (line : string) =

            let oppositions = line |> Column.asString 124 126 |> Convert.tryInt
            let range = line |> ObservationRange.fromLine oppositions

            { Designation =         line |> Column.asString      1   7
              H =                   line |> Column.tryAsDouble   9  13
              G =                   line |> Column.tryAsDouble  15  19 
              Epoch =               line |> Column.asString     21  25
              M =                   line |> Column.tryAsDouble  27  35 
              Perihelion =          line |> Column.tryAsDouble  38  46 
              Node =                line |> Column.tryAsDouble  49  57 
              Inclination =         line |> Column.tryAsDouble  60  68 
              e =                   line |> Column.tryAsDouble  71  79 
              n =                   line |> Column.tryAsDouble  81  91 
              a =                   line |> Column.tryAsDouble  93 103  
              Uncertainty =         line |> Column.tryAsChar   106 106
              Reference =           line |> Column.asString    108 116
              Observations =        line |> Column.tryAsInt    118 122
              Oppositions =         oppositions
              Range =               range
              RmsResidual =         line |> Column.tryAsDouble 138 141
              PerturbersCoarse =    line |> Column.asString    143 145
              PerturbersPrecise =   line |> Column.asString    147 149
              ComputerName =        line |> Column.asString    151 160
              Flags =               line |> Column.asCharArray 162 165
              ReadableDesignation = line |> Column.asString    167 194
              LastOpposition =      line |> Column.asString    195 202 }

        let private skipHeader (data : seq<string>) =
            data
            |> Seq.skipWhile (fun line -> 
                line.StartsWith("----------") |> not)
            |> Seq.skip 1

        let fromMpcOrbData (data : seq<string>) =
            data
            |> skipHeader
            |> Seq.filter (fun line -> line.Length > 0)
            |> Seq.map fromMpcOrbLine

    open System.IO

    let demo() =

        // Brightest 10 minor planets (absolute magnitude)
        // Edit the path to reflect where you stored the file:
        @"C:\Data\MinorPlanets\MPCORB.DAT"
        |> File.ReadLines
        |> MinorPlanet.fromMpcOrbData
        |> Seq.filter (fun mp -> 
            mp.H |> Option.isSome)
        |> Seq.sortBy (fun mp -> 
            mp.H.Value)
        |> Seq.truncate 10
        |> Seq.iter (fun mp ->
            printfn "Name: %s Abs. magnitude: %0.2f" 
                mp.ReadableDesignation (mp.H |> Option.defaultValue nan))
