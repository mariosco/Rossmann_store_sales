#I @"C:\Users\Marios\Documents\Visual Studio 2015\Projects\Rossmann"
#I @"..\packages"
#load @"Utility_functions.fsx"
#load @"Charting_help_functions.fsx"
#load @"Input_data.fsx"
#load @"Training_algorithm.fsx"
#load @"Features.fsx"
#load @"Model_evaluation.fsx"
open Training_algorithm
open Input_data
open Features
open Utility_functions
open Model_evaluation
open System.IO
let value_to_optimize = (fun (obs:Datapoint) -> float obs.Sales)
/////////////////////////////////////////////////////////////////////////////////////
// TODO: Caution!! Line 480, 1336, 2192, 3048, ... of testing data, missing Open replaced with 0
// TODO: Use the alternative algebraic package to improve the model training and testing
// TODO: Dots during the training to monitor progress

let store = 1
let one_store_data = One_store_data store

let one_store_model = 
    let (store, theta, model) = Single_model Featurizer value_to_optimize store one_store_data
    model

let one_store_evaluation = Evaluate_one_store one_store_data one_store_model

printfn "Error = %s" (Get_percentage one_store_evaluation)

Charting_help_functions.comparison_chart 
    30 
    (fun (row:Datapoint) -> (row.DayOfWeek, row.Sales)) 
    one_store_data
    "dow" 
    "Sales"
/////////////////////////////////////////////////////////////////////////////////////

let all_stores_data = Separate_stores_data
let multiple_stores_models = Multiple_models Featurizer value_to_optimize all_stores_data
// TODO Improve the performance of Evaluate_multiple_stores
let multiple_stores_evaluation = Evaluate_multiple_stores multiple_stores_models all_stores_data // TODO improve performance
let evaluation = Average_evaluation multiple_stores_evaluation
Print_evaluation evaluation
/////////////////////////////////////////////////////////////////////////////////////

let testing_data = Testing_data
let store_to_model = Converted_store_model_map multiple_stores_models
let testing_data_estimation = 
    Testing_data
    |> Array.map (fun obs -> store_to_model.[obs.Store] obs)
let save_in_desktop = 
    let ouput_strings = testing_data_estimation |> Array.mapi (fun ind x -> sprintf "%i,%i" (ind + 1) (int x))
    let desktop = @"C:\Users\Marios\Desktop\submission_file.txt"
    File.WriteAllLines (desktop, ["\"Id\",\"Sales\""])
    File.AppendAllLines (desktop, ouput_strings)
save_in_desktop
        