namespace Manage_datasets

open Global_configurations.Config
open Global_configurations.Data_types
open System.IO

module Manage_datasets_API =
    let Generate_initial_dataset () =
        let dataset = 
            File_paths.Raw_trainig_set
            |> Load.dataset 
            |> Parse.string_dataset

        Save.dataset dataset File_paths.Initial_dataset
    
    let Generate_basic_features () =
        let features = 
            File_paths.Initial_dataset
            |> Load.dataset
            |> Manage_features.split_dataset_to_features

        features
        |> Array.Parallel.iter (fun feature ->
            let filepath = Path.Combine(File_directories.Features, feature.Name) |> File_extension.csv
            Save.feature feature filepath)

    let Load_feature_set (feature_names:string[]) = Load.multiple_features feature_names

    let Load_feature feature_name = Load.feature (Path.Combine(File_directories.Features, feature_name) |> File_extension.csv)

    let Get_available_feature_names () = Load.feature_names File_directories.Features

    let Save_feature (feature:Feature<float>) = Save.feature feature (Path.Combine(File_directories.Features, feature.Name) |> File_extension.csv)

    let Group_by_feature dataset feature_name = Transform.group_by_feature dataset feature_name 