namespace Rossmann

open System
open Manage_datasets
open Utility_functions
open Global_configurations.Data_types
open Training
open Testing_set_evaluation

module Navigation_actions =
    let Load_datasets () =
        Console.WriteLine "Loading datasets..."
        let dataset = 
            Manage_datasets_API.Get_available_feature_names ()
            |> Manage_datasets_API.Load_feature_set             
        
        let get_index name = dataset.Header |> Array.findIndex (fun n -> n = name)
        let index_of_sales = get_index "Sales"
        let index_of_store = get_index "Store"
        let index_of_customers = get_index "Customers"

        let dataset_grouped_by_store = Manage_datasets_API.Group_by_feature dataset "Store"
        let featurizer = Arrays.filter_elements_at_indices [|index_of_store; index_of_sales; index_of_customers|] >> Array.toList
        let feature_to_optimize = fun (obs:float[]) -> obs.[index_of_sales]
        Console.Clear ()
        (dataset_grouped_by_store, featurizer, feature_to_optimize)
        
    let Single_store_testing (dataset_grouped_by_store:(float*Dataset<float>)[]) featurizer feature_to_optimize =
        Console.WriteLine "Select store of preference:"
        let store = Int32.Parse (Console.ReadLine())
        let one_store_data = snd dataset_grouped_by_store.[store]

        let one_store_model =  Single_model.Train featurizer feature_to_optimize one_store_data |> snd

        let one_store_evaluation = Evaluate_one_store one_store_data one_store_model

        printfn "Error = %s\n" (Parsing.Sprintf_probability_percentage one_store_evaluation)
    
    let Multiple_stores_testing (dataset_grouped_by_store:(float*Dataset<float>)[]) featurizer feature_to_optimize = 
        Console.WriteLine "Training models..."
        let multiple_stores_models = Multiple_models.Train featurizer feature_to_optimize dataset_grouped_by_store
        Console.Clear ()
        Console.WriteLine "Evaluating models..."
        let multiple_stores_evaluation = Evaluate_multiple_stores multiple_stores_models dataset_grouped_by_store
        Console.Clear ()
        Print_evaluation <| Average_evaluation multiple_stores_evaluation

