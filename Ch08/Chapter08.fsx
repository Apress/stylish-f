module Chapter08_01 =

    open System

    type ConsolePrompt(message : string) =
        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                Console.Beep()
                this.GetValue()

    let firstPrompt = ConsolePrompt("Please enter your first name")
    let lastPrompt = ConsolePrompt("Please enter your last name")

    let demo() =

        let first, last = firstPrompt.GetValue(), lastPrompt.GetValue()

        printfn "Hello %s %s" first last

    // > demo();;
    // Please enter your first name:
    // Kit
    // Please enter your last name:
    // Eason
    // Hello Kit Eason

 module Chapter08_02 =

    open System

    type ConsolePrompt(message : string) =

        do
            if String.IsNullOrWhiteSpace(message) then
                raise <| ArgumentException("Null or empty", "message")
        let message = message.Trim()

        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                Console.Beep()
                this.GetValue()

    let demo() =

        // System.ArgumentException: Null or empty
        // Parameter name: message
        let first = ConsolePrompt(null)
        printfn "Hello %s" (first.GetValue())

 module Chapter08_03 =

    open System

    type ConsolePrompt(message : string) =

        do
            if String.IsNullOrWhiteSpace(message) then
                raise <| ArgumentException("Null or empty", "message")
        let message = message.Trim()

        member __.Message = 
            message

        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                Console.Beep()
                this.GetValue()

    let first = ConsolePrompt("First name")
    // First name
    printfn "%s" first.Message

 module Chapter08_04 =

    open System

    type ConsolePrompt(message : string) =

        do
            if String.IsNullOrWhiteSpace(message) then
                raise <| ArgumentException("Null or empty", "message")
        let message = message.Trim()

        member val BeepOnError = true
            with get, set
        member __.Message = 
            message
        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                if this.BeepOnError then 
                    Console.Beep()
                this.GetValue()

    let demo() =
        let first = ConsolePrompt("First name")
        first.BeepOnError <- false
        let name = first.GetValue()
        // No beep on invalid input!
        printfn "%s" name

 module Chapter08_05 =

    open System

    type ConsolePrompt(message : string, beepOnError : bool) =

        do
            if String.IsNullOrWhiteSpace(message) then
                raise <| ArgumentException("Null or empty", "message")
        let message = message.Trim()

        new (message : string) =
            ConsolePrompt(message, true)

        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                if beepOnError then 
                    Console.Beep()
                this.GetValue()

    let demo() =
        let first = ConsolePrompt("First name", false)
        let last = ConsolePrompt("Second name")
        // No beep on invalid input!
        let firstName = first.GetValue()
        // Beep on invalid input!
        let lastName = last.GetValue()
        printfn "Hello %s %s" firstName lastName

 module Chapter08_06 =

    open System

    type ConsolePrompt(message : string) =

        let mutable foreground = ConsoleColor.White
        let mutable background = ConsoleColor.Black

        member __.ColorScheme
            with get() =
                foreground, background
            and set(fg, bg) =
                if fg = bg then
                    raise <| ArgumentException(
                                "Foreground, background can't be same")
                foreground <- fg
                background <- bg

        member this.GetValue() =
            Console.ForegroundColor <- foreground
            Console.BackgroundColor <- background
            printfn "%s:" message
            Console.ResetColor()
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            else
                this.GetValue()

    let demo() =
        let first = ConsolePrompt("First name")
        // System.ArgumentException: Foreground, background can't be same
        first.ColorScheme <- (ConsoleColor.Red, ConsoleColor.Red)

 module Chapter08_07 =

    open System

    type ConsolePrompt(message : string, maxAttempts : int) =

        let mutable attempts = 1

        member this.GetValue() =
            printfn "%s:" message
            let input = Console.ReadLine()
            if not (String.IsNullOrWhiteSpace(input)) then
                input
            elif attempts < maxAttempts then
                attempts <- attempts + 1
                this.GetValue()
            else
                raise <| Exception("Max attempts exceeded")

    let demo() =
        let first = ConsolePrompt("First name", 2)
        let name = first.GetValue()
        // Exception if you try more than twice:
        printfn "%s" name

 module Chapter08_08 =

    open System

    type ConsolePrompt<'T>(message : string, maxAttempts : int, tryConvert : string -> 'T option) =

        let mutable attempts = 1

        member this.GetValue() =
            printfn "%s:" message
            let input =  Console.ReadLine()
            match input |> tryConvert with
            | Some v -> v
            | None -> 
                if attempts < maxAttempts then
                    attempts <- attempts + 1
                    this.GetValue()
                else
                    raise <| Exception("Max attempts exceeded")

 module Chapter08_09 =

    open System

    type ConsolePrompt<'T>(message : string, maxAttempts : int, tryConvert : string -> 'T option) =

        let mutable attempts = 1

        member this.GetValue() =
            printfn "%s:" message
            let input =  Console.ReadLine()
            match input |> tryConvert with
            | Some v -> v
            | None -> 
                if attempts < maxAttempts then
                    attempts <- attempts + 1
                    this.GetValue()
                else
                    raise <| Exception("Max attempts exceeded")

    let tryConvertString (s : string) =
        if String.IsNullOrWhiteSpace(s) then
            None
        else
            Some s

    let tryConvertInt (s : string) =
        match Int32.TryParse(s) with
        | true, x -> Some x
        | false, _ -> None

    let demo() =
        let namePrompt = ConsolePrompt("Name", 2, tryConvertString)
        let agePrompt = ConsolePrompt("Age", 2, tryConvertInt)
        let name = namePrompt.GetValue()
        let age = agePrompt.GetValue()
        printfn "Name: %s Age: %i" name age

 module Chapter08_10 =

    open System

    type ConsolePrompt<'T>(message : string, maxAttempts : int, tryConvert : string -> 'T option) =

        let mutable attempts = 1

        member this.GetValue() =
            printfn "%s:" message
            Console.ReadLine()
            |> tryConvert
            |> Option.defaultWith (fun () ->
                if attempts < maxAttempts then
                    attempts <- attempts + 1
                    this.GetValue()
                else
                    raise <| Exception("Max attempts exceeded"))

    let tryConvertString (s : string) =
        if String.IsNullOrWhiteSpace(s) then
            None
        else
            Some s

    let tryConvertInt (s : string) =
        match Int32.TryParse(s) with
        | true, x -> Some x
        | false, _ -> None

    let demo() =
        let namePrompt = ConsolePrompt("Name", 2, tryConvertString)
        let agePrompt = ConsolePrompt("Age", 2, tryConvertInt)
        let name = namePrompt.GetValue()
        let age = agePrompt.GetValue()
        printfn "Name: %s Age: %i" name age


 module Chapter08_11 =

    open System

    type ConsolePrompt<'T>(message : string, maxAttempts : int, tryConvert : string -> 'T option) =

        let mutable attempts = 1

        member val BeepOnError = true
            with get, set

        member this.GetValue() =
            printfn "%s:" message
            let input =  Console.ReadLine()
            match input |> tryConvert with
            | Some v -> v
            | None -> 
                if attempts < maxAttempts then
                    attempts <- attempts + 1
                    if this.BeepOnError then 
                        Console.Beep()
                    this.GetValue()
                else
                    raise <| Exception("Max attempts exceeded")

    let tryConvertString (s : string) =
        if String.IsNullOrWhiteSpace(s) then
            None
        else
            Some s

    let tryConvertInt (s : string) =
        match Int32.TryParse(s) with
        | true, x -> Some x
        | false, _ -> None

    let demo() =
        
        // No argument names:
        let namePrompt1 = 
            ConsolePrompt("Name", 2, tryConvertString)

        // With argument names:
        let namePrompt2 = 
            ConsolePrompt(
                message = "Name", 
                maxAttempts = 2, 
                tryConvert = tryConvertString)

        // No argument names, but with object initialization:
        let namePrompt3 = 
            ConsolePrompt(
                "Name", 
                2, 
                tryConvertString, 
                BeepOnError = false)

        // With argument names and object initialization:
        let namePrompt4 = 
            ConsolePrompt(
                message = "Name", 
                maxAttempts = 2, 
                tryConvert = tryConvertString, 
                BeepOnError = false)

        let name = namePrompt4.GetValue()
        printfn "Hello %s" name

module Chapter08_12 =

    type RingBuffer<'T>(items : 'T seq) =

        let _items = items |> Array.ofSeq
        let length = _items.Length

        member __.Item i =
            _items.[i % length]
                    
    let demo() =
        let fruits = RingBuffer(["Apple"; "Orange"; "Pear"])

        // Apple Orange Pear Apple Orange Pear Apple Orange
        for i in 0..7 do
            printfn "%s" fruits.[i]

        // Invalid assignment
        // fruits.[4] <- "Grape"

module Chapter08_13 =

    type RingBuffer<'T>(items : 'T seq) =

        let _items = items |> Array.ofSeq
        let length = _items.Length

        member __.Item 
            with get(i) =
                _items.[i % length]
            and set i value = 
                _items.[i % length] <- value
                    
    let demo() =
        let fruits = RingBuffer(["Apple"; "Orange"; "Pear"])
        fruits.[4] <- "Grape"
        // Apple Grape Pear Apple Grape Pear Apple Grape
        for i in 0..7 do
            printfn "%s" fruits.[i]

module Chapter08_14 =

    type RingBuffer2D<'T>(items : 'T[,]) =

        let leni = items.GetLength(0)
        let lenj = items.GetLength(1)
        let _items = Array2D.copy items

        member __.Item 
            with get(i, j) =
                _items.[i % leni, j % lenj]
            and set (i, j) value = 
                _items.[i % leni, j % lenj] <- value
                    
    let demo() =
        let numbers = Array2D.init 4 5 (fun x y -> x * y)
        let numberRing = RingBuffer2D(numbers)
        // 0 0 -> 0
        // 0 1 -> 0
        // ...
        // 1 1 -> 1
        // 1 2 -> 2
        // ..
        // 9 8 -> 3
        // 9 9 -> 4
        for i in 0..9 do
            for j in 0..9 do
                printfn "%i %i -> %A" i j (numberRing.[i,j])
    
module Chapter08_15 =

    type MediaId = string
    type TimeStamp = int

    type Status = 
        | Empty 
        | Playing of MediaId * TimeStamp
        | Stopped of MediaId

    type IMediaPlayer = 
        abstract member Open : MediaId -> unit
        abstract member Play : unit -> unit
        abstract member Stop : unit -> unit
        abstract member Eject : unit -> unit
        abstract member Status : unit -> Status

module Chapter08_16 =

    type MediaId = string
    type TimeStamp = int

    type Status = 
        | Empty 
        | Playing of MediaId * TimeStamp
        | Stopped of MediaId

    type IMediaPlayer = 
        abstract member Open : MediaId -> unit
        abstract member Play : unit -> unit
        abstract member Stop : unit -> unit
        abstract member Eject : unit -> unit
        abstract member Status : unit -> Status

    type DummyPlayer() =

        let mutable status = Empty

        interface IMediaPlayer with

            member __.Open(mediaId : MediaId) =
                printfn "Opening '%s'" mediaId
                status <- Stopped mediaId

            member __.Play() =
                match status with
                | Empty 
                | Playing(_, _) -> ()
                | Stopped(mediaId) ->
                    printfn "Playing '%s'" mediaId
                    status <- Playing(mediaId, 0)  

            member __.Stop() =
                match status with
                | Empty 
                | Stopped(_) -> ()  
                | Playing(mediaId, _) ->
                    printfn "Stopping '%s'" mediaId
                    status <- Stopped(mediaId)

            member __.Eject() =
                match status with
                | Empty -> ()
                | Stopped(_) 
                | Playing(_, _) ->
                    printfn "Ejecting"
                    status <- Empty

            member __.Status() =
                status

module Chapter08_17 =

    type MediaId = string
    type TimeStamp = int

    type Status = 
        | Empty 
        | Playing of MediaId * TimeStamp
        | Stopped of MediaId

    type IMediaPlayer = 
        abstract member Open : MediaId -> unit
        abstract member Play : unit -> unit
        abstract member Stop : unit -> unit
        abstract member Eject : unit -> unit
        abstract member Status : unit -> Status

    type DummyPlayer() =

        let mutable status = Empty

        interface IMediaPlayer with

            member __.Open(mediaId : MediaId) =
                printfn "Opening '%s'" mediaId
                status <- Stopped mediaId

            member __.Play() =
                match status with
                | Empty 
                | Playing(_, _) -> ()
                | Stopped(mediaId) ->
                    printfn "Playing '%s'" mediaId
                    status <- Playing(mediaId, 0)  

            member __.Stop() =
                match status with
                | Empty 
                | Stopped(_) -> ()  
                | Playing(mediaId, _) ->
                    printfn "Stopping '%s'" mediaId
                    status <- Stopped(mediaId)

            member __.Eject() =
                match status with
                | Empty -> ()
                | Stopped(_) 
                | Playing(_, _) ->
                    printfn "Ejecting"
                    status <- Empty

            member __.Status() =
                status

    let demo() =

        let player = new DummyPlayer() :> IMediaPlayer

        // "Opening 'Dreamer'"
        player.Open("Dreamer")

        // "Playing 'Dreamer'"
        player.Play()

        // "Ejecting"
        player.Eject()
        
        // "Empty"
        player.Status() |> printfn "%A"

module Chapter08_18 =

    type MediaId = string
    type TimeStamp = int

    type Status = 
        | Empty 
        | Playing of MediaId * TimeStamp
        | Stopped of MediaId

    type IMediaPlayer = 
        abstract member Open : MediaId -> unit
        abstract member Play : unit -> unit
        abstract member Stop : unit -> unit
        abstract member Eject : unit -> unit
        abstract member Status : unit -> Status

    open System
    open System.IO

    type DummyPlayer() =

        let uniqueId = Guid.NewGuid()
        let mutable status = Empty
        let stream = new MemoryStream()

        member __.UniqueId =
            uniqueId

        interface IMediaPlayer with

            member __.Open(mediaId : MediaId) =
                printfn "Opening '%s'" mediaId
                status <- Stopped mediaId

            member __.Play() =
                match status with
                | Empty 
                | Playing(_, _) -> ()
                | Stopped(mediaId) ->
                    printfn "Playing '%s'" mediaId
                    status <- Playing(mediaId, 0)  

            member __.Stop() =
                match status with
                | Empty 
                | Stopped(_) -> ()  
                | Playing(mediaId, _) ->
                    printfn "Stopping '%s'" mediaId
                    status <- Stopped(mediaId)

            member __.Eject() =
                match status with
                | Empty -> ()
                | Stopped(_) 
                | Playing(_, _) ->
                    printfn "Ejecting"
                    status <- Empty

            member __.Status() =
                status

        interface IDisposable with

            member __.Dispose() =
                stream.Dispose()

    let demo() =
        let player = new DummyPlayer()
        (player :> IMediaPlayer).Open("Dreamer")
        // 95cf8c51-ee29-4c99-b714-adbe1647b62c
        printfn "%A" player.UniqueId
        (player :> IDisposable).Dispose()
        
module Chapter08_19 =

    type ILogger = 
        abstract member Info : string -> unit
        abstract member Error : string -> unit

    type MediaId = string
    type TimeStamp = int

    type Status = 
        | Empty 
        | Playing of MediaId * TimeStamp
        | Stopped of MediaId

    type IMediaPlayer = 
        abstract member Open : MediaId -> unit
        abstract member Play : unit -> unit
        abstract member Stop : unit -> unit
        abstract member Eject : unit -> unit
        abstract member Status : unit -> Status

    type LoggingPlayer(logger : ILogger) =

        let mutable status = Empty

        interface IMediaPlayer with

            member __.Open(mediaId : MediaId) =
                logger.Info(sprintf "Opening '%s'" mediaId)
                status <- Stopped mediaId

            member __.Play() =
                match status with
                | Empty ->
                    logger.Error("Nothing to play")
                | Playing(_, _) ->
                    logger.Error("Already playing")
                | Stopped(mediaId) ->
                    logger.Info(sprintf "Playing '%s'" mediaId)
                    status <- Playing(mediaId, 0)  

            member __.Stop() =
                match status with
                | Empty 
                | Stopped(_) -> 
                    logger.Error("Not playing")
                | Playing(mediaId, _) ->
                    logger.Info(sprintf "Playing '%s'" mediaId)
                    status <- Stopped(mediaId)

            member __.Eject() =
                match status with
                | Empty -> 
                    logger.Error("Nothing to eject")
                | Stopped(_) 
                | Playing(_, _) ->
                    logger.Info("Ejecting")
                    status <- Empty

            member __.Status() =
                status

    let demo() =

        let logger = {
            new ILogger with
                member __.Info(msg) = printfn "%s" msg
                member __.Error(msg) = printfn "%s" msg }

        let player = new LoggingPlayer(logger) :> IMediaPlayer
        
        // "Nothing to eject"
        player.Eject()

        // "Opening 'Dreamer'"
        player.Open("Dreamer")

        // "Ejecting"
        player.Eject()

module Chapter08_20 =

    [<AbstractClass>]
    type AbstractClass() =
        abstract member SaySomething : string -> string

    type ConcreteClass(name : string) =
        inherit AbstractClass()
        override __.SaySomething(whatToSay) =
            sprintf "%s says %s" name whatToSay

    let demo() =
        let cc = ConcreteClass("Concrete")
        // "Concrete says hello"
        cc.SaySomething("hello")

module Chapter08_21 =

    type ParentClass() =
        abstract member SaySomething : string -> string 
        default __.SaySomething(whatToSay) =
            sprintf "Parent says %s" whatToSay

    type ConcreteClass1(name : string) =
        inherit ParentClass()

    type ConcreteClass2(name : string) =
        inherit ParentClass()
        override __.SaySomething(whatToSay) =
            sprintf "%s says %s" name whatToSay

    let demo() =
        let cc1 = ConcreteClass1("Concrete 1")
        let cc2 = ConcreteClass2("Concrete 2")
        // "Parent says hello"
        printfn "%s" (cc1.SaySomething("hello"))
        // "Concrete 2 says hello"
        printfn "%s" (cc2.SaySomething("hello"))

module Chapter08_22 =

    type LatLon(latitude : float, longitude : float) =

        member __.Latitude = latitude
        member __.Longitude = longitude

    let landsEnd = LatLon(50.07, -5.72)
    let johnOGroats = LatLon(58.64, -3.07)
    let landsEnd2 = LatLon(50.07, -5.72)

    // false
    printfn "%b" (landsEnd = johnOGroats)
    // false
    printfn "%b" (landsEnd = landsEnd2)

module Chapter08_23 =

    open System

    type LatLon(latitude : float, longitude : float) =

        member __.Latitude = latitude
        member __.Longitude = longitude

        interface IEquatable<LatLon> with
            member this.Equals(that : LatLon) =
                this.Latitude = that.Latitude
                && this.Longitude = that.Longitude

    let demo()  =

        let landsEnd = LatLon(50.07, -5.72)
        let johnOGroats = LatLon(58.64, -3.07)
        let landsEnd2 = LatLon(50.07, -5.72)

        // false
        printfn "%b" (landsEnd = johnOGroats)
        // false
        printfn "%b" (landsEnd = landsEnd2)

module Chapter08_24 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        member __.Latitude = latitude
        member __.Longitude = longitude

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

module Chapter08_25 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        member __.Latitude = latitude
        member __.Longitude = longitude

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

    let demo() =

        let landsEnd = LatLon(50.07, -5.72)
        let johnOGroats = LatLon(58.64, -3.07)
        let landsEnd2 = LatLon(50.07, -5.72)

        // false
        printfn "%b" (landsEnd = johnOGroats)
        // true
        printfn "%b" (landsEnd = landsEnd2)

        let places = [ landsEnd; johnOGroats; landsEnd2 ]

        let placeDict =
            places
            |> Seq.mapi (fun i place -> place, i)
            |> dict

        // 50.070000, -5.720000 -> 2
        // 58.640000, -3.070000 -> 1
        placeDict
        |> Seq.iter (fun kvp ->
            printfn "%f, %f -> %i" 
                kvp.Key.Latitude kvp.Key.Longitude kvp.Value)

module Chapter08_26 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        member __.Latitude = latitude
        member __.Longitude = longitude

        // static member ( = ) : this:LatLon * that:LatLon -> bool
        static member op_Equality(this : LatLon, that : LatLon) =
            this.Equals(that)

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

module Chapter08_27 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        member __.Latitude = latitude
        member __.Longitude = longitude

        static member op_Equality(this : LatLon, that : LatLon) =
            this.Equals(that)

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

        interface IComparable with
            member this.CompareTo(thatObj) =
                match thatObj with
                | :? LatLon as that -> 
                    compare 
                        (this.Latitude, this.Longitude) 
                        (that.Latitude, that.Longitude)                    
                | _ -> 
                    raise <| ArgumentException("Can't compare different types")

module Chapter08_28 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        member __.Latitude = latitude
        member __.Longitude = longitude

        static member op_Equality(this : LatLon, that : LatLon) =
            this.Equals(that)

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

        interface IComparable with
            member this.CompareTo(thatObj) =
                match thatObj with
                | :? LatLon as that -> 
                    compare 
                        (this.Latitude, this.Longitude) 
                        (that.Latitude, that.Longitude)                    
                | _ -> 
                    raise <| ArgumentException("Can't compare different types")

    let demo() =

        let landsEnd = LatLon(50.07, -5.72)
        let johnOGroats = LatLon(58.64, -3.07)
        let landsEnd2 = LatLon(50.07, -5.72)

        let places = [ landsEnd; johnOGroats; landsEnd2 ]

        // 50.070000, -5.720000
        // 58.640000, -3.070000
        places
        |> Set.ofList
        |> Seq.iter (fun ll -> printfn "%f, %f" ll.Latitude ll.Longitude)

module Chapter08_29 =

    open System

    [<AllowNullLiteral>]
    type LatLon(latitude : float, longitude : float) =

        let eq (that : LatLon) = 
            if isNull that then
                false
            else
                latitude = that.Latitude 
                && longitude = that.Longitude 

        let comp (that : LatLon) =
            compare
                (latitude, longitude) 
                (that.Latitude, that.Longitude)                  

        member __.Latitude = latitude
        member __.Longitude = longitude

        static member op_Equality(this : LatLon, that : LatLon) =
            this.Equals(that)

        override this.GetHashCode() =
            hash (this.Latitude, this.Longitude)

        override __.Equals(thatObj) =
            match thatObj with
            | :? LatLon as that -> 
                eq that  
            | _ -> 
                false

        interface IEquatable<LatLon> with
            member __.Equals(that : LatLon) =
                eq that

        interface IComparable with
            member __.CompareTo(thatObj) =
                match thatObj with
                | :? LatLon as that -> 
                    comp that                 
                | _ -> 
                    raise <| ArgumentException("Can't compare different types")

        interface IComparable<LatLon> with
            member __.CompareTo(that) =
               comp that                   
