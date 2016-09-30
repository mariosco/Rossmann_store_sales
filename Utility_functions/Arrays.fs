namespace Utility_functions

module Arrays =
    let contains elem arr = arr |> Array.exists (fun e -> e = elem)

    let filter_array_at_indices indices arr =
        arr
        |> Array.indexed
        |> Array.filter (fun (ind, _) -> not (contains ind indices))
        |> Array.map snd
