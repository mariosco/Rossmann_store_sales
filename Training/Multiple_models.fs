namespace Training

module Multiple_models = 
    let Train featurizer feature_to_optimize multiple_store_datasets = 
        multiple_store_datasets 
        |> Array.Parallel.map (fun (store, dataset) -> 
            let estimation = Single_model.Train featurizer feature_to_optimize dataset
            (store, fst estimation, snd estimation))

