namespace Rossmann

open System
open Training
open Utility_functions.Parsing
open Global_configurations.Data_types

module Testing_set_evaluation =
    let private squareError (model:Model) real_value (sample:float[]) = 
        let model_sales = model sample
        match real_value with 
        | 0.0 -> 0.0
        | _ -> Math.Pow (((model_sales - real_value) / real_value), 2.0)

    let private meanAbsError (model:Model) (data:Dataset<float>) =
        let index_of_sales = data.Header |> Array.findIndex (fun name -> name = "Sales") // TODO: pull the value to optimise index out
        data.Observations 
        |> Array.averageBy (fun sample -> squareError model (sample.[index_of_sales]) sample )
        |> Math.Sqrt

    (*let Convert_model_to_testing_model (model:Model) = 
        fun testing_datapoint -> model (Convert_testingpoint_to_datapoint testing_datapoint)*)

    (*let Store_model_map multiple_models = 
        let stores = multiple_models |> Array.Parallel.map (fun (store, theta, model) -> store) |> Array.distinct
        let store_to_model = 
            stores 
            |> Array.Parallel.map (fun store -> 
                (store, 
                 multiple_models 
                 |> Array.find (fun (store_nr, theta, model) -> store_nr = store) 
                 |> fun (x, y, z) -> z))
        Map.ofArray store_to_model*)

    (*let Converted_store_model_map multiple_models = 
        let converted_models = 
            multiple_models
            |> Array.map (fun (store, theta, model) -> (store, theta, Convert_model_to_testing_model model))
        Store_model_map converted_models*)

    let Evaluate_one_store store model = meanAbsError model store

    let Evaluate_multiple_stores multiple_models multiple_stores_data =
        multiple_stores_data 
        |> Array.Parallel.map (fun (store, data) ->
            let (_, _, model) = multiple_models |> Array.find (fun (st, _, _) -> st = store)
            let accuracy = meanAbsError model data
            (store, accuracy))

    let Average_evaluation (evaluation:(float * float)[]) = evaluation |> Array.averageBy snd

    let Print_evaluation evaluation = 
        evaluation 
        |> Sprintf_probability_percentage 
        |> printfn "Mean error: %s"