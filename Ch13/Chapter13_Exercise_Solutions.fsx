module Exercise13_01 = 

    open System.IO
    open System.Text.RegularExpressions

    module FileSearch = 

        module private FileName = 

            let isMatch pattern =
                let re = Regex(pattern)
                fun path ->
                    let fileName = Path.GetFileName(path)
                    re.IsMatch(fileName)

        module private FileAttributes =

            let hasFlag flag filePath =
                FileInfo(filePath)
                    .Attributes
                    .HasFlag(flag)

        /// Search below path for files whose file names match the specified
        /// regular expression, and which have the 'read only' attribute set.
        let findReadOnly pattern dir = 
            Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
            |> Seq.filter (FileName.isMatch pattern)
            |> Seq.filter (FileAttributes.hasFlag FileAttributes.ReadOnly)

