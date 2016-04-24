namespace Manage_datasets

open Global_configurations.Data_types

module internal Manage_features =
    let split_dataset_to_features (dataset:Dataset<'a>) =
        let feature_observations =
            Array.init
                dataset.Header.Length
                (fun ind -> 
                    dataset.Observations
                    |> Array.map (fun obs -> obs.[ind]))
        Array.mapi             
            (fun ind feat -> { Name = dataset.Header.[ind]; Observations = feat})
            feature_observations