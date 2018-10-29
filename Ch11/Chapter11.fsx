   module Chapter11_01 = 

       open System

       let checkString (s : string) =
           if isNull(s) then
               raise <| ArgumentNullException("Must not be null")
           elif String.IsNullOrEmpty(s) then
               raise <| ArgumentException("Must not be empty")
           elif String.IsNullOrWhiteSpace(s) then
               raise <| ArgumentException("Must not be white space")
           else
               s 

   module Chapter11_02 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

   module Chapter11_03 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

   module Chapter11_04 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

   module Chapter11_05 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Failure "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Failure "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Failure "Must not be white space"
           else
               Success s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Success s
           else
               Failure "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Success s
           else
               Failure (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               // raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Success
           with
           | e ->
               log e.Message
               Failure "Sorry, there was an internal error saving your password"

   module Chapter11_06 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Failure "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Failure "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Failure "Must not be white space"
           else
               Success s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Success s
           else
               Failure "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Success s
           else
               Failure (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               // raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Success
           with
           | e ->
               log e.Message
               Failure "Sorry, there was an internal error saving your password"

       // password:string -> Outcome<unit, string>
       let validateAndSave password = 

           let mixedCase' = adapt mixedCase
           let containsAny' = adapt (containsAny "-_!?")
           let tidy' = passThrough tidy
           let save' = adapt save
           
           password
           |> notEmpty
           |> mixedCase'
           |> containsAny'
           |> tidy'
           |> save'

   module Chapter11_07 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Failure "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Failure "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Failure "Must not be white space"
           else
               Success s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Success s
           else
               Failure "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Success s
           else
               Failure (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               //raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Success
           with
           | e ->
               log e.Message
               Failure "Sorry, there was an internal error saving your password"

       // password:string -> Outcome<unit, string>
       let validateAndSave password = 

           let mixedCase' = adapt mixedCase
           let containsAny' = adapt (containsAny "-_!?")
           let tidy' = passThrough tidy
           let save' = adapt save
           
           password
           |> notEmpty
           |> mixedCase'
           |> containsAny'
           |> tidy'
           |> save'

       let demo() =

           // Failure "Must not be null"
           null |> validateAndSave |> printfn "%A"

           // Failure "Must not be empty"
           "" |> validateAndSave |> printfn "%A"

           // Failure "Must not be white space"
           " " |> validateAndSave |> printfn "%A"

           // Failure "Must contain mixed case"
           "the quick brown fox" |> validateAndSave |> printfn "%A"

           // Failure "Must contain at least one of "-_!?""
           "The quick brown fox" |> validateAndSave |> printfn "%A"

           // Saving password 'The quick brown fox!'
           // Success ()
           "The quick brown fox!" |> validateAndSave |> printfn "%A"

   module Chapter11_09 =

       type Outcome<'TSuccess, 'TFailure> =
           | Success of 'TSuccess
           | Failure of 'TFailure

       let adapt func input =
           match input with
           | Success x -> func x
           | Failure f -> Failure f

       let passThrough func input =
           match input with
           | Success x -> func x |> Success
           | Failure f -> Failure f

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Failure "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Failure "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Failure "Must not be white space"
           else
               Success s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Success s
           else
               Failure "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Success s
           else
               Failure (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               //raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Success
           with
           | e ->
               log e.Message
               Failure "Sorry, there was an internal error saving your password"

       // string -> Outcome<unit, string>
       let validateAndSave = 

           notEmpty
           >> adapt mixedCase
           >> adapt (containsAny "-_!?")
           >> passThrough tidy
           >> adapt save

       let demo() =

           // Failure "Must not be null"
           null |> validateAndSave |> printfn "%A"

           // Failure "Must not be empty"
           "" |> validateAndSave |> printfn "%A"

           // Failure "Must not be white space"
           " " |> validateAndSave |> printfn "%A"

           // Failure "Must contain mixed case"
           "the quick brown fox" |> validateAndSave |> printfn "%A"

           // Failure "Must contain at least one of "-_!?""
           "The quick brown fox" |> validateAndSave |> printfn "%A"

           // Saving password 'The quick brown fox!'
           // Success ()
           "The quick brown fox!" |> validateAndSave |> printfn "%A"

   module Chapter11_10 =

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Error "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Error "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Error "Must not be white space"
           else
               Ok s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Ok s
           else
               Error "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Ok s
           else
               Error (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               //raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Ok
           with
           | e ->
               log e.Message
               Error "Sorry, there was an internal error saving your password"

   module Chapter11_11 =

       open System

       let notEmpty (s : string) =
           if isNull(s) then
               Error "Must not be null"
           elif String.IsNullOrEmpty(s) then
               Error "Must not be empty"
           elif String.IsNullOrWhiteSpace(s) then
               Error "Must not be white space"
           else
               Ok s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Ok s
           else
               Error "Must contain mixed case"

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Ok s
           else
               Error (sprintf "Must contain at least one of %A" cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               //raise <| Exception "Dummy exception"
           let log m = 
               printfn "Logging error: %s" m
           try 
               dbSave s
               |> Ok
           with
           | e ->
               log e.Message
               Error "Sorry, there was an internal error saving your password"

       open Result

       // string -> Result<unit, string>
       let validateAndSave = 

           notEmpty
           >> bind mixedCase
           >> bind (containsAny "-_!?")
           >> map tidy
           >> bind save

       let demo() =

           // Error "Must not be null"
           null |> validateAndSave |> printfn "%A"

           // Error "Must not be empty"
           "" |> validateAndSave |> printfn "%A"

           // Error "Must not be white space"
           " " |> validateAndSave |> printfn "%A"

           // Error "Must contain mixed case"
           "the quick brown fox" |> validateAndSave |> printfn "%A"

           // Failure "Must contain at least one of "-_!?""
           "The quick brown fox" |> validateAndSave |> printfn "%A"
           
           // Saving password 'The quick brown fox!'
           // Ok ()
           "The quick brown fox!" |> validateAndSave |> printfn "%A"

    module Chapter11_12 =

       open System

       type ValidationError =
           | MustNotBeNull
           | MustNotBeEmpty
           | MustNotBeWhiteSpace
           | MustContainMixedCase
           | MustContainOne of chars:string
           | ErrorSaving of exn:Exception
       
       let notEmpty (s : string) =
           if isNull(s) then
               Error MustNotBeNull
           elif String.IsNullOrEmpty(s) then
               Error MustNotBeEmpty
           elif String.IsNullOrWhiteSpace(s) then
               Error MustNotBeWhiteSpace
           else
               Ok s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Ok s
           else
               Error MustContainMixedCase

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Ok s
           else
               Error (MustContainOne cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               raise <| Exception "Dummy exception"
           try 
               dbSave s
               |> Ok
           with
           | e ->
               Error (ErrorSaving e)

    module Chapter11_13 =

       open System

       type ValidationError =
           | MustNotBeNull
           | MustNotBeEmpty
           | MustNotBeWhiteSpace
           | MustContainMixedCase
           | MustContainOne of chars:string
           | ErrorSaving of exn:Exception
       
       let notEmpty (s : string) =
           if isNull(s) then
               Error MustNotBeNull
           elif String.IsNullOrEmpty(s) then
               Error MustNotBeEmpty
           elif String.IsNullOrWhiteSpace(s) then
               Error MustNotBeWhiteSpace
           else
               Ok s

       let mixedCase (s : string) =
           let hasUpper = 
               s |> Seq.exists (Char.IsUpper)
           let hasLower =
               s |> Seq.exists (Char.IsLower)
           if hasUpper && hasLower then
               Ok s
           else
               Error MustContainMixedCase

       let containsAny (cs : string) (s : string) =
           if s.IndexOfAny(cs.ToCharArray()) > -1 then
               Ok s
           else
               Error (MustContainOne cs)

       let tidy (s : string) =
           s.Trim()

       let save (s : string) =
           let dbSave s : unit = 
               printfn "Saving password '%s'" s
               // Uncomment this to simulate an exception:
               raise <| Exception "Dummy exception"
           try 
               dbSave s
               |> Ok
           with
           | e ->
               Error (ErrorSaving e)

       open Result

       // string -> Result<unit, ValidationError>
       let validateAndSave = 

           notEmpty
           >> bind mixedCase
           >> bind (containsAny "-_!?")
           >> map tidy
           >> bind save

       let savePassword =

           let log m = 
               printfn "Logging error: %s" m

           validateAndSave
           >> mapError (fun err -> 
               match err with
               | MustNotBeNull
               | MustNotBeEmpty
               | MustNotBeWhiteSpace ->
                   sprintf "Password must be entered"
               | MustContainMixedCase ->
                   sprintf "Password must contain upper and lower case characters"
               | MustContainOne cs ->
                   sprintf "Password must contain one of %A" cs
               | ErrorSaving e ->
                   log e.Message
                   sprintf "Sorry there was an internal error saving the password")

       let demo() =

           // Error "Password must be entered"
           null |> savePassword |> printfn "%A"

           // Error "Password must be entered"
           "" |> savePassword |> printfn "%A"

           // Error "Password must be entered"
           " " |> savePassword |> printfn "%A"

           // Error "Password must contain upper and lower case characters"
           "the quick brown fox" |> savePassword |> printfn "%A"

           // Error "Password must contain one of "-_!?""
           "The quick brown fox" |> savePassword |> printfn "%A"
           
           // Saving password 'The quick brown fox!'
           // Logging error: Dummy exception
           // Error "Sorry there was an internal error saving the password"
           "The quick brown fox!" |> savePassword |> printfn "%A"
