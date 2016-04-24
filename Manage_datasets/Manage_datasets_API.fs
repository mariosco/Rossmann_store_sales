namespace Manage_datasets

open Global_configurations.Config
open Global_configurations.Data_types

module Manage_datasets_API =
    let Generate_basic_features =
        let dataset_filename = "initial_dataset.csv"

        let dataset = 
            File_locations.Raw_trainigset
            |> Load.dataset 
            |> Parse.string_dataset
        Save.dataset dataset (File_locations.Initial_dataset + dataset_filename)

        let features = Manage_features.split_dataset_to_features dataset
        features
        |> Array.Parallel.iter (fun feature -> 
            Save.feature feature (File_locations.Features + feature.Name + File_endings.csv))

    let Load_feature_set (feature_names:string[]) = Load.multiple_features feature_names

    let Load_feature feature_name = Load.feature (File_locations.Features + feature_name + File_endings.csv)

    let Get_available_feature_names () = Load.feature_names File_locations.Features

    let Save_feature (feature:Feature<float>) = Save.feature feature (File_locations.Features + feature.Name + File_endings.csv)

    let Group_by_feature dataset feature_name = Transform.group_by_feature dataset feature_name 