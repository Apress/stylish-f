module Exercise13_01 = 

    open System.IO
    open System.Text.RegularExpressions

    let find pattern dir =
        let re = Regex(pattern)
        Directory.EnumerateFiles
                      (dir, "*.*", SearchOption.AllDirectories)
        |> Seq.filter (fun path -> re.IsMatch(Path.GetFileName(path)))
        |> Seq.map (fun path -> 
            FileInfo(path))
        |> Seq.filter (fun fi -> 
                    fi.Attributes.HasFlag(FileAttributes.ReadOnly))
        |> Seq.map (fun fi -> fi.Name)

