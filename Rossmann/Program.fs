namespace Rossmann

open System
open Navigation_actions

module Program = 
    [<EntryPoint>]
    let Main (_:string[]) =
        Generate_storage_directories ()
        Generate_initial_data () 

        let (dataset_grouped_by_store, featurizer, feature_to_optimize) = Load_datasets ()
        
        let mutable choice = new ConsoleKey()
        while choice <> ConsoleKey.Escape do
            Console.WriteLine "Please select an option:\n1. Single store testing\n2. Multiple store testing\n"
            choice <- (Console.ReadKey ()).Key
            Console.Clear ()

            match choice with 
            | ConsoleKey.D1 -> Single_store_testing dataset_grouped_by_store featurizer feature_to_optimize                
            //| ConsoleKey.D2 -> Console.WriteLine "Not implemented\n"
                //Comparison_chart 
                  //  30 
                    //(fun (row:Datapoint) -> (row.DayOfWeek, row.Sales)) 
                    //one_store_data
                    //"dow" 
                    //"Sales"
            | ConsoleKey.D2 -> Multiple_stores_testing dataset_grouped_by_store featurizer feature_to_optimize
                
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

            Console.WriteLine "Press a key to continue..."
            Console.ReadKey () |> ignore
            Console.Clear ()
        0