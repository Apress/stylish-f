module Chapter05_01 =

    open System
    open System.IO

    let latestWriteTime (path : string) (searchPattern : string) =
        let files = Directory.EnumerateFiles(path, searchPattern, 
                                                SearchOption.AllDirectories)
        let mutable latestDate = DateTime.MinValue
        for file in files do
            let thisDate = File.GetLastWriteTime(file)
            if thisDate > latestDate then
                latestDate <- thisDate
        latestDate

    latestWriteTime @"c:\temp" "*.*"

module Chapter05_02 =

    open System.IO

    let latestWriteTime (path : string) (searchPattern : string) =
        Directory.EnumerateFiles(path, searchPattern, 
                                    SearchOption.AllDirectories)
        // Could also just say 'Seq.map File.GetLastWriteTime' here.
        |> Seq.map (fun file -> File.GetLastWriteTime(file))
        |> Seq.max

    latestWriteTime @"c:\temp" "*.*"

module Chapter05_03 =

    open System.IO

    module Seq =
        let tryMax s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.max |> Some

    let tryLatestWriteTime (path : string) (searchPattern : string) =
        Directory.EnumerateFiles(path, searchPattern, 
                                    SearchOption.AllDirectories)
        |> Seq.map File.GetLastWriteTime
        |> Seq.tryMax

    // Some date
    tryLatestWriteTime @"c:\temp" "*.*"
    // None
    tryLatestWriteTime @"c:\temp" "doesnotexist.*"

module Chapter05_04 =

    type Student = { Name : string; Grade : char }

    let findFirstWithGrade (grade : char) (students : seq<Student>) =
        let mutable result = { Name = ""; Grade = ' ' }
        let mutable found = false
        for student in students do
            if not found && student.Grade = grade then
                result <- student
                found <- true
        result

module Chapter05_05 =

    type Student = { Name : string; Grade : char }

    let findFirstWithGrade (grade : char) (students : seq<Student>) =
        students
        |> Seq.find (fun s -> s.Grade = grade)

module Chapter05_06 =

    type Student = { Name : string; Grade : char }

    let tryFindFirstWithGrade (grade : char) (students : seq<Student>) =
        let mutable result = { Name = ""; Grade = ' ' }
        let mutable found = false
        for student in students do
            if not found && student.Grade = grade then
                result <- student
                found <- true
        if found then
            Some result
        else
            None

module Chapter05_07 =

    type Student = { Name : string; Grade : char }

    let tryFindFirstWithGrade (grade : char) (students : seq<Student>) =
        students
        |> Seq.tryFind (fun s -> s.Grade = grade)

module Chapter05_08 =

    type Student = { Name : string; Grade : char }

    let printGradeLabel (student : Student) =
        printfn "%s\n\nGrade: %c" (student.Name.ToUpper()) student.Grade

    let printGradeLabels (students : seq<Student>) =
        for student in students do
            printGradeLabel student

module Chapter05_09 =

    type Student = { Name : string; Grade : char }

    let printGradeLabel (student : Student) =
        printfn "%s\n\nGrade: %c" (student.Name.ToUpper()) student.Grade

    let printGradeLabels (students : seq<Student>) =
        students
        |> Seq.iter (fun student -> printGradeLabel student)
        // Alternatively:
        //|> Seq.iter printGradeLabel

module Chapter05_10 =

    type Student = { Name : string; Grade : char }

    let makeGradeLabel (student : Student) =
        sprintf "%s\n\nGrade: %c" (student.Name.ToUpper()) student.Grade

    let makeGradeLabels (students : seq<Student>) =
        let result = ResizeArray<string>()
        for student in students do
            result.Add(makeGradeLabel student)
        result |> Seq.cast<string>

    [ { Name = "Kit"; Grade = 'C' } ]
    |> makeGradeLabels
        
module Chapter05_11 =
            
    type Student = { Name : string; Grade : char }

    let makeGradeLabel (student : Student) =
        sprintf "%s\n\nGrade: %c" (student.Name.ToUpper()) student.Grade

    let printGradeLabels (students : seq<Student>) =
        let length = students |> Seq.length
        let result = Array.zeroCreate<string> length
        let mutable i = 0
        for student in students do
            result.[i] <- makeGradeLabel student
            i <- i + 1
        result |> Seq.ofArray

module Chapter05_12 =
            
    type Student = { Name : string; Grade : char }

    let makeGradeLabel (student : Student) =
        sprintf "%s\n\nGrade: %c" (student.Name.ToUpper()) student.Grade

    let printGradeLabels (students : seq<Student>) =
        students
        |> Seq.map makeGradeLabel

module Chapter05_13 =

    // Simulate something coming from an API, which only 
    // tells you if you are going to get something after
    // you asked for it.
    let tryGetSomethingFromApi =
        let mutable thingCount = 0
        let maxThings = 10
        fun () ->
            if thingCount < maxThings then
                thingCount <- thingCount+1
                "Soup"
            else
                null // No more soup for you!

    let listThingsFromApi() =
        let mutable finished = false
        while not finished do
            let thing = tryGetSomethingFromApi()
            if thing <> null then
                printfn "I got %s" thing
            else
                finished <- true

    // I got Soup (x10)
    // No more soup for me!
    listThingsFromApi()

module Chapter05_14 =

    // Simulate something coming from an API, which only 
    // tells you if you are going to get something after
    // you asked for it.
    let tryGetSomethingFromApi =
        let mutable thingCount = 0
        let maxThings = 10
        fun () ->
            if thingCount < maxThings then
                thingCount <- thingCount+1
                "Soup"
            else
                null // No more soup for you!

    let rec apiToSeq() =
        seq {
            match tryGetSomethingFromApi() |> Option.ofObj with
            | Some thing -> 
                yield thing
                yield! apiToSeq()
            | None -> ()
        }

    let listThingsFromApi() =
        apiToSeq()
        |> Seq.iter (printfn "I got %s")

    // I got Soup (x10)
    listThingsFromApi()

module Chapter05_15 =

    open System

    let getMax (numbers : seq<float>) =
        let mutable max = Double.MinValue
        for number in numbers do
            if number > max then
                max <- number
        max

module Chapter05_16 =

    module Seq =
            
        let tryMax s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.max |> Some

        let tryMaxBy f s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.maxBy f |> Some

module Chapter05_17 =

    module Seq =
            
        let tryMax s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.max |> Some

        let tryMaxBy f s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.maxBy f |> Some
                    
    type Student = { Name : string; Grade : char }

    let tryGetLastStudentByName (students : seq<Student>) =
        students
        |> Seq.tryMaxBy (fun s -> s.Name)

module Chapter05_18 =

    module Seq =
            
        let tryMax s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.max |> Some

        let tryMaxBy f s =
            if s |> Seq.isEmpty then
                None
            else
                s |> Seq.maxBy f |> Some

    // -5.3
    let furthestFromZero =
        [| -1.1; -0.1; 0.; 1.1; -5.3 |]
        |> Seq.tryMaxBy abs

module Chapter05_19 =

    let rms (s : seq<float>) =
        let mutable total = 0.
        let mutable count = 0
        for item in s do
            total <- total + (item ** 2.)
            count <- count + 1
        let average = total / (float count)
        sqrt average

    // 120.2081528
    [|0.; -170.; 0.; 170.|]
    |> rms

module Chapter05_20 =

    let rms (s : seq<float>) =
        s 
        |> Seq.averageBy (fun item -> item ** 2.)
        |> sqrt

    // 120.2081528
    [|0.; -170.; 0.; 170.|]
    |> rms

module Chapter05_21 =

    let product (s : seq<float>) =
        let mutable total = 1.
        for item in s do
            total <- total * item
        total

    // 1.98
    [| 1.2; 1.1; 1.5|]
    |> product

module Chapter05_22 =
        
    let product (s : seq<float>) =
        s 
        |> Seq.fold (fun acc elem -> acc * elem) 1.

    // 1.98
    [| 1.2; 1.1; 1.5|]
    |> product
