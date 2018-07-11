
[<EntryPoint>]
let main argv =
    let (all, _) = 
        System.IO.File.ReadAllLines "./Knight.png.meta"
        |> Seq.fold (fun (all, current) line -> 
            let clean = line.Trim().Replace("\t", "")
            match current with
            | Some (name, x, y, w, h) -> 
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
                    (all, current)) ([], None)
    let text = sprintf "name,x,y,width,height\r\n%s" <| (all |> Seq.rev |> Seq.map (fun (n, x, y, w, h) -> sprintf "%s,%i,%i,%i,%i" n x y w h) |> String.concat "\r\n")
    System.IO.File.WriteAllText ("./result.csv", text)
    0
