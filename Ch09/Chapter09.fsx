module Chapter09_01 =

    // int -> int -> int
    let add a b = a + b

    // int -> int -> int
    let addUp = add

    // 5
    printfn "%i" (add 2 3)
    // 5
    printfn "%i" (addUp 2 3)

module Chapter09_02 =

    let add a b = a + b

    let applyAndPrint f a b =
        let r = f a b
        printfn "%i" r

    // "5"
    applyAndPrint add 2 3

module Chapter09_03 =

    // int -> int -> int
    let add a b = a + b

    // int -> int
    let addTwo = add 2

    // 5
    printfn "%i" (add 2 3)
    // 5
    printfn "%i" (addTwo 3)

module Chapter09_04 =

    let surround prefix suffix s =
        sprintf "%s%s%s" prefix s suffix

    let roundParen = surround "(" ")"
    let squareParen = surround "[" "]"
    let xmlComment = surround "<!--" "-->"
    let quote q = surround q q
    let doubleQuote = quote "\""

    let demo() =
        // ~~Markdown strikethrough~~
        printfn "%s" (surround "~~" "~~" "Markdown strikethrough")
        // (Round parentheses)
        printfn "%s" (roundParen "Round parentheses")
        // [Square parentheses]
        printfn "%s" (squareParen "Square parentheses")
        // <!--XML comment-->
        printfn "%s" (xmlComment "XML comment")
        // "To be or not to be"
        printfn "%s" (doubleQuote "To be or not to be")

module Chapter09_05 =

    let surround (prefix, suffix) s =
        sprintf "%s%s%s" prefix s suffix

    let roundParen = surround ("(", ")")
    let squareParen = surround ("[", "]")
    let xmlComment = surround ("<!--", "-->")
    let quote q = surround(q, q)
    let doubleQuote = quote "\""

    let demo() =
        // ~~Markdown strikethrough~~
        printfn "%s" (surround ("~~", "~~") "Markdown strikethrough")
        // (Round parentheses)
        printfn "%s" (roundParen "Round parentheses")
        // [Square parentheses]
        printfn "%s" (squareParen "Square parentheses")
        // <!--XML comment-->
        printfn "%s" (xmlComment "XML comment")
        // "To be or not to be"
        printfn "%s" (doubleQuote "To be or not to be")

module Chapter09_06 =

    type RingBuffer2D<'T>(items : 'T[,]) =

        let leni = items.GetLength(0)
        let lenj = items.GetLength(1)
        let _items = Array2D.copy items

        member __.Item 
            with get(i, j) =
                _items.[i % leni, j % lenj]
            and set (i, j) value = 
                _items.[i % leni, j % lenj] <- value

module Chapter09_07 =

    // int -> int -> int
    let add a b = a + b

    // int -> int
    let addTwo = add 2

    // int
    let result = addTwo 3

module Chapter09_08 =

    // int * int -> int
    let add(a, b) = a + b

    // int
    let result = add(2, 3)

module Chapter09_09 =

    let add a b = a + b

    let applyAndPrint f a b =
        let r = f a b
        printfn "%i" r

    // "5"
    applyAndPrint add 2 3

module Chapter09_10 =

    // Takes curried arguments:
    let add a b = a + b

    // Takes tupled argument:
    let addTupled(a, b) = a + b

    // f must take curried arguments and return an int:
    let applyAndPrint1 (f : 'a -> 'b -> int) a b =
        let r = f a b
        printfn "%i" r

    // f must take curried integer arguments and return an int:
    let applyAndPrint2 (f : int -> int -> int) a b =
        let r = f a b
        printfn "%i" r

    // f must take tupled integer arguments and return an int:
    let applyAndPrint3 (f : int * int -> int) a b =
        let r = f(a, b)
        printfn "%i" r

    // Must use the curried version of add here:
    applyAndPrint1 add 2 3
    applyAndPrint2 add 2 3

    // Must use the tupled version of add here:
    applyAndPrint3 addTupled 2 3

module Chapter09_11 =

    // int -> int -> int
    let add a =
        fun b -> a + b

    // 5
    printfn "%i" (add 2 3) 
    
module Chapter09_12 =

    let counter start =
        let mutable current = start
        fun () -> 
            let this = current
            current <- current + 1
            this

    let demo() =

        let c1 = counter 0
        let c2 = counter 100

        // c1: 0
        // c2: 100
        // c1: 1
        // c2: 101
        // c1: 2
        // c2: 102
        // c1: 3
        // c2: 103
        // c1: 4
        // c2: 104
        for _ in 0..4 do
            printfn "c1: %i" (c1())
            printfn "c2: %i" (c2())

module Chapter09_13 =

    let randomByte =
        let r = System.Random()
        fun () ->
            r.Next(0, 255) |> byte

    // A3-52-31-D2-90-E6-6F-45-1C-3F-F2-9B-7F-58-34-44-
    let demo() =
        for _ in 0..15 do
            printf "%X-" (randomByte())
        printfn ""

module Chapter09_14 =

    module Typographic = 
        let openSingle = '‘'
        let openDouble = '“'
        let closeSingle = '’'
        let closeDouble = '”'

    module Neutral =
        let single = '\''
        let double = '"'

    /// Translate any typographic single quotes to neutral ones.
    let fixSingleQuotes (s : string) =
        s
         .Replace(Typographic.openSingle, Neutral.single)
         .Replace(Typographic.closeSingle, Neutral.single)

    /// Translate any typographic double quotes to neutral ones.
    let fixDoubleQuotes (s : string) = 
        s
         .Replace(Typographic.openDouble, Neutral.double)
         .Replace(Typographic.closeDouble, Neutral.double)

    /// Translate any typographic quotes to neutral ones.
    let fixTypographicQuotes (s : string) =
        s
        |> fixSingleQuotes
        |> fixDoubleQuotes

module Chapter09_15 =

    module Typographic = 
        let openSingle = '‘'
        let openDouble = '“'
        let closeSingle = '’'
        let closeDouble = '”'

    module Neutral =
        let single = '\''
        let double = '"'

    /// Translate any typographic single quotes to neutral ones.
    let fixSingleQuotes (s : string) =
        s
         .Replace(Typographic.openSingle, Neutral.single)
         .Replace(Typographic.closeSingle, Neutral.single)

    /// Translate any typographic double quotes to neutral ones.
    let fixDoubleQuotes (s : string) = 
        s
         .Replace(Typographic.openDouble, Neutral.double)
         .Replace(Typographic.closeDouble, Neutral.double)

    // Build a 'fixQuotes' function using composition:
    let fixTypographicQuotes (s : string) =
        let fixQuotes = fixSingleQuotes >> fixDoubleQuotes
        s |> fixQuotes

module Chapter09_16 =

    module Typographic = 
        let openSingle = '‘'
        let openDouble = '“'
        let closeSingle = '’'
        let closeDouble = '”'

    module Neutral =
        let single = '\''
        let double = '"'

    /// Translate any typographic single quotes to neutral ones.
    let fixSingleQuotes (s : string) =
        s
         .Replace(Typographic.openSingle, Neutral.single)
         .Replace(Typographic.closeSingle, Neutral.single)

    /// Translate any typographic double quotes to neutral ones.
    let fixDoubleQuotes (s : string) = 
        s
         .Replace(Typographic.openDouble, Neutral.double)
         .Replace(Typographic.closeDouble, Neutral.double)

    // Remove the explicit binding of 'fixQuotes':
    let fixTypographicQuotes (s : string) =
        s |> (fixSingleQuotes >> fixDoubleQuotes)

module Chapter09_15 =

    module Typographic = 
        let openSingle = '‘'
        let openDouble = '“'
        let closeSingle = '’'
        let closeDouble = '”'

    module Neutral =
        let single = '\''
        let double = '"'

    /// Translate any typographic single quotes to neutral ones.
    let fixSingleQuotes (s : string) =
        s
         .Replace(Typographic.openSingle, Neutral.single)
         .Replace(Typographic.closeSingle, Neutral.single)

    /// Translate any typographic double quotes to neutral ones.
    let fixDoubleQuotes (s : string) = 
        s
         .Replace(Typographic.openDouble, Neutral.double)
         .Replace(Typographic.closeDouble, Neutral.double)

    // Build a 'fixQuotes' function using composition:
    let fixTypographicQuotes (s : string) =
        let fixQuotes = fixSingleQuotes >> fixDoubleQuotes
        s |> fixQuotes

module Chapter09_17 =

    module Typographic = 
        let openSingle = '‘'
        let openDouble = '“'
        let closeSingle = '’'
        let closeDouble = '”'

    module Neutral =
        let single = '\''
        let double = '"'

    /// Translate any typographic single quotes to neutral ones.
    let fixSingleQuotes (s : string) =
        s
         .Replace(Typographic.openSingle, Neutral.single)
         .Replace(Typographic.closeSingle, Neutral.single)

    /// Translate any typographic double quotes to neutral ones.
    let fixDoubleQuotes (s : string) = 
        s
         .Replace(Typographic.openDouble, Neutral.double)
         .Replace(Typographic.closeDouble, Neutral.double)

    // Remove the explicit parameter:
    let fixTypographicQuotes =
        fixSingleQuotes >> fixDoubleQuotes
