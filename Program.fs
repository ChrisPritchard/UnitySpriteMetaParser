
open System.IO

let getInfo path = 
    System.IO.File.ReadAllLines path 
        |> Seq.fold (fun (all, current) line -> 
            let clean = line.Trim().Replace("\t", "")
            match current with
            | Some (name, x, y, w, _) -> 
                if clean.StartsWith("x: ") then
                    let newSet = name, int <| clean.Substring("x: ".Length), -1, -1, -1
                    (all, Some newSet)
                else if clean.StartsWith("y: ") then
                    let newSet = name, x, int <| clean.Substring("y: ".Length), -1, -1
                    (all, Some newSet)
                else if clean.StartsWith("width: ") then
                    let newSet = name, x, y, int <| clean.Substring("width: ".Length), -1
                    (all, Some newSet)
                else if clean.StartsWith("height: ") then
                    let newSet = name, x, y, w, int <| clean.Substring("height: ".Length)
                    (newSet::all, None)
                else
                    (all, current)
            | None ->
                if clean.StartsWith("- name: ") then 
                    let name = clean.Substring("- name: ".Length)
                    let newSet = name, -1, -1, -1, -1
                    (all, Some newSet)
                else
                    (all, current)) ([], None) |> fst

let isInt x = (System.Int64.TryParse (x : string)) |> fst

[<EntryPoint>]
let main argv =
    match argv with
    | [|inPath;outPath;height|] when File.Exists inPath && isInt height -> 
        let ih = int height
        let info = getInfo inPath
        let text = 
            sprintf "name,x,y,width,height\r\n%s" <| 
            (info |> Seq.rev |> Seq.map (fun (n, x, y, w, h) -> 
                sprintf "%s,%i,%i,%i,%i" n x (ih - y - h) w h) |> String.concat "\r\n")
        File.WriteAllText (outPath, text)
        0
    | _ ->
        printfn "Invalid args. Should be:"
        printfn "[existing meta file] [output path] [image height in pixels]"
        -1