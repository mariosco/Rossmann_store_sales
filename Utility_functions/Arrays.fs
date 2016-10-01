namespace Utility_functions

module Arrays =
    let filter_elements_at_indices indices arr =
        arr
        |> Array.indexed
        |> Array.filter (fun (ind, _) -> not (Array.contains ind indices))
        |> Array.map snd