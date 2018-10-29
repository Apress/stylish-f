// From Listing 10-1:
module Log =

    open System
    open System.Threading

    /// Print a colored log message.
    let report (color : ConsoleColor) (message : string) =
        Console.ForegroundColor <- color
        printfn "%s (thread ID: %i)" 
            message Thread.CurrentThread.ManagedThreadId
        Console.ResetColor()

    let red = report ConsoleColor.Red
    let green = report ConsoleColor.Green
    let yellow = report ConsoleColor.Yellow
    let cyan = report ConsoleColor.Cyan

// From Listing 10-2:
module Outcome = 

    type Outcome = 
        | OK of filename:string 
        | Failed of filename:string

    let isOk = function 
        | OK _ -> true 
        | Failed _ -> false

    let fileName = function
        | OK fn
        | Failed fn -> fn

// From Listing 10-4:
module Download =

    open System
    open System.IO
    open System.Net
    open System.Text.RegularExpressions
    // From Nuget package "FSharp.Data"
    open FSharp.Data

    let private absoluteUri (pageUri : Uri) (filePath : string) =
        if filePath.StartsWith("http:") 
           || filePath.StartsWith("https:") then
            Uri(filePath)
        else
            let sep = '/'
            filePath.TrimStart(sep)
            |> (sprintf "%O%c%s" pageUri sep)
            |> Uri

    /// Get the URLs of all links in a specified page matching a 
    /// specified regex pattern.
    let private getLinks (pageUri : Uri) (filePattern : string) =
        Log.cyan "Getting names..."
        let re = Regex(filePattern)

        let html = HtmlDocument.Load(pageUri.AbsoluteUri)

        let links = 
            html.Descendants ["a"]
            |> Seq.choose (fun node -> 
                node.TryGetAttribute("href")
                |> Option.map (fun att -> att.Value()))
            |> Seq.filter (re.IsMatch)
            |> Seq.map (absoluteUri pageUri)
            |> Seq.distinct
            |> Array.ofSeq

        links

    // From Listing 10-5:
    /// Download a file to the specified local path.
    let private tryDownload (localPath : string) (fileUri : Uri) =
        let fileName = fileUri.Segments |> Array.last
        Log.yellow (sprintf "%s - starting download" fileName)
        let filePath = Path.Combine(localPath, fileName)

        use client = new WebClient()
        try
            client.DownloadFile(fileUri, filePath) 
            Log.green (sprintf "%s - download complete" fileName)
            Outcome.OK fileName
        with
        | e ->
            Log.red (sprintf "%s - error: %s" fileName e.Message)
            Outcome.Failed fileName

    // From Listing 10-6:
    /// Download all the files linked to in the specified webpage, whose
    /// link path matches the specified regular expression, to the specified 
    /// local path. Return a tuple of succeeded and failed file names.
    let GetFiles 
            (pageUri : Uri) (filePattern : string) (localPath : string) = 
        let links = getLinks pageUri filePattern 
            
        let downloaded, failed = 
            links
            |> Array.map (tryDownload localPath)
            |> Array.partition Outcome.isOk

        downloaded |> Array.map Outcome.fileName,
        failed |> Array.map Outcome.fileName

// From Listing 10-7:
open System
open System.Diagnostics

[<EntryPoint>]
let main _ =

    // Some minor planets data:
    let uri = Uri @"https://minorplanetcenter.net/data"
    let pattern = @"neam.*\.json\.gz$"

    // From Table 10-1:
    // Some computational linguistics data:
    //let uri = Uri @"http://compling.hss.ntu.edu.sg/omw"
    //let pattern = @"\.zip$"

    // Some google n-grams:
    //let uri = Uri @"http://storage.googleapis.com/books/ngrams/books/datasetsv2.html"
    //let pattern = @"eng\-1M\-2gram.*\.zip$"

    // Edit this to a suitable path in your environment:
    let localPath = @"c:\temp\downloads"

    let sw = Stopwatch()
    sw.Start()

    let downloaded, failed = 
        Download.GetFiles uri pattern localPath

    failed
    |> Array.iter (fun fn -> 
        Log.report ConsoleColor.Red (sprintf "Failed: %s" fn))

    Log.cyan 
        (sprintf "%i files downloaded in %0.1fs, %i failed. Press a key" 
            downloaded.Length sw.Elapsed.TotalSeconds failed.Length)

    Console.ReadKey() |> ignore

    0
