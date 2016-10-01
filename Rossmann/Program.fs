namespace Rossmann

open System
open System.IO
open Charting_functions
open Feature_selector
open Training
open Utility_functions
open Testing_set_evaluation
open Manage_datasets
open Global_configurations.Data_types
open Global_configurations.Config

type Navigation_Option = Single_store_testing | Charting | Multiple_store_testing | Save_model_evaluation

module Program = 
    [<EntryPoint>]
    let Main (_:string[]) = 
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
        
        let mutable choice = new ConsoleKey()
        while choice <> ConsoleKey.Escape do
            Console.WriteLine "Please select an option:\n1. Single store testing\n2. Charting\n3. Multiple store testing\n4. Save model evaluation\n"
            choice <- (Console.ReadKey ()).Key
            Console.Clear ()

            match choice with 
            | ConsoleKey.D1 -> 
                Console.WriteLine "Select store of preference:"
                let store = Int32.Parse (Console.ReadLine())
                let one_store_data = snd dataset_grouped_by_store.[store]

                let one_store_model =  Single_model.Train featurizer feature_to_optimize one_store_data |> snd

                let one_store_evaluation = Evaluate_one_store one_store_data one_store_model

                printfn "Error = %s\n" (Parsing.Sprintf_probability_percentage one_store_evaluation)
            | ConsoleKey.D2 -> Console.WriteLine "Not implemented\n"
                //Comparison_chart 
                  //  30 
                    //(fun (row:Datapoint) -> (row.DayOfWeek, row.Sales)) 
                    //one_store_data
                    //"dow" 
                    //"Sales"
                /////////////////////////////////////////////////////////////////////////////////////
            | ConsoleKey.D3 ->
                // TODO: Dots during the training to monitor progress
                let multiple_stores_models = Multiple_models.Train featurizer feature_to_optimize dataset_grouped_by_store
                let multiple_stores_evaluation = Evaluate_multiple_stores multiple_stores_models dataset_grouped_by_store
                printfn "%A" (multiple_stores_evaluation |> Array.filter (fun (st, ev) -> ev > 1.0))
                let evaluation = Average_evaluation multiple_stores_evaluation
                Print_evaluation evaluation
                /////////////////////////////////////////////////////////////////////////////////////
            (*| ConsoleKey.D4 -> 
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
                save_in_desktop*)
            
            | ConsoleKey.Escape -> Console.WriteLine "Exiting program"
            | _ -> Console.WriteLine "Invalid input.\n"

        0