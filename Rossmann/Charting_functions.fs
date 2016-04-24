namespace Rossmann

open System.Drawing
open FSharp.Charting

module Charting_functions = 
    let group_charting_pairs scaling pairs = 
        pairs 
        |> Seq.groupBy (fun (x, y) -> (x, y / scaling ))
        |> Seq.map (fun (key, value) -> (fst key, snd key * scaling))

    let increasing_value_line_graph pairs = 
        pairs 
        |> Seq.groupBy fst
        |> Seq.collect (fun (key, value) -> 
            [value |> Seq.maxBy snd; value |> Seq.minBy snd])
        |> Seq.sortBy fst
        //|> Chart.Line

    let Comparison_chart scaling feature_selection samples labelx labely=
        let graph_points = 
            samples 
            |> Seq.map feature_selection 
            |> group_charting_pairs scaling
        //Chart
          //  .Point(graph_points)
            //.WithXAxis(true, labelx)
            //.WithYAxis(true, labely);
        "Dummy"

    (*let multiple_properties_graphs scaling property_of_interest data = 
        let properties =
            (data |> Seq.head).GetType().GetProperties()
            |> Array.filter (fun prop -> prop.Name != property_of_interest) 
        
        let data_fields = 
            data
            |> Seq.map (fun sample ->
                sample.GetType().GetProperties()
                |> Array.map (fun prop -> prop.GetValue(properties, null)))
        let charts = 
            [1..properties.Length]
            |> Array.map (fun i -> data_fields |> Seq.map (fun props -> (props.[i])))      
            *)

