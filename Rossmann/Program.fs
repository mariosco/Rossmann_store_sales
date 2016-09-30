namespace Rossmann

open System
open System.IO
open Utility_functions
open Charting_functions
open Feature_selector
open Training
open Utility_functions
open Testing_set_evaluation
open Manage_datasets
open Global_configurations.Data_types

type Navigation_Option = Single_store_testing | Charting | Multiple_store_testing | Save_model_evaluation

module Program = 
    [<EntryPoint>]
    let Main (args:string[]) = 
        Manage_datasets_API.Generate_basic_features
        (*(*let stopWatch = new System.Diagnostics.Stopwatch()
        stopWatch.Start()

        stopWatch.Stop()
        Console.WriteLine stopWatch.ElapsedMilliseconds
        Console.WriteLine "Done!"
        let dummy = Console.ReadKey ()*)

        let dataset = Manage_datasets_API.Load_feature_set (Manage_datasets_API.Get_available_feature_names()) 
        let index_of_sales = dataset.Header |> Array.findIndex (fun name -> name = "Sales")
        let index_of_store = dataset.Header |> Array.findIndex (fun name -> name = "Store")
        let index_of_customers = dataset.Header |> Array.findIndex (fun name -> name = "Customers")
        let index_of_date = dataset.Header |> Array.findIndex (fun name -> name = "Date_day")
        let grouped_dataset = Manage_datasets_API.Group_by_feature dataset "Store"
        let featurizer = Arrays.filter_array_at_indices [|index_of_store; index_of_sales; index_of_customers|] >> Array.toList
        let feature_to_optimize = fun (obs:float[]) -> obs.[index_of_sales]

        let suspicious_data_group = 
            grouped_dataset 
            |> Array.filter (fun (st, data) -> st = 85.0) 

        let suspicious_data = 
            suspicious_data_group
            |> Array.map (fun (st, data) -> 
                let obs = data.Observations
                sprintf 
                    "%A" 
                    (obs |> Array.iter (fun line -> File.WriteAllText (@"C:\Users\Marios\Desktop\suspicious.txt", line.[index_of_sales].ToString() + line.[index_of_date].ToString()))))
        
        Console.WriteLine (suspicious_data.Length)
        let mutable choice = new ConsoleKey()

        while choice <> ConsoleKey.Escape do
            Console.WriteLine "Please select an option:\n1. Single store testing\n2. Charting\n3. Multiple store testing\n4. Save model evaluation\n"
            let input = Console.ReadKey ()
            choice <- input.Key

            match choice with 
            | ConsoleKey.D1 -> 
                /////////////////////////////////////////////////////////////////////////////////////
                // TODO: Caution!! Line 480, 1336, 2192, 3048, ... of testing data, missing Open replaced with 0
                // TODO: Dots during the training to monitor progress
                Console.WriteLine "Select store of preference:"
                let store = Int32.Parse (Console.ReadLine())
                let one_store_data = snd grouped_dataset.[store]

                let one_store_model =  Single_model.Train featurizer feature_to_optimize one_store_data |> snd

                let one_store_evaluation = Evaluate_one_store one_store_data one_store_model

                printfn "Error = %s\n" (Get_percentage one_store_evaluation)
            | ConsoleKey.D2 -> Console.WriteLine "Not implemented\n"
                //Comparison_chart 
                  //  30 
                    //(fun (row:Datapoint) -> (row.DayOfWeek, row.Sales)) 
                    //one_store_data
                    //"dow" 
                    //"Sales"
                /////////////////////////////////////////////////////////////////////////////////////
            | ConsoleKey.D3 ->
                let multiple_stores_models = Multiple_models.Train featurizer feature_to_optimize grouped_dataset
                let multiple_stores_evaluation = Evaluate_multiple_stores multiple_stores_models grouped_dataset
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
            | _ -> Console.WriteLine "Invalid input.\n"*)
        0