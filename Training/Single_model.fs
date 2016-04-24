namespace Training

open System
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.Providers.LinearAlgebra.Mkl
open Global_configurations.Data_types
open Training_data_types

module Single_model = 
    MathNet.Numerics.Control.LinearAlgebraProvider <- MklLinearAlgebraProvider ()

    let private predict (theta:Vec) (v:Vec) = theta * v
    let private estimate (y:Vec) (M:Mat) =
        (M.Transpose() * M).Inverse() * M.Transpose() * y

    let private predictor (f:Featurizer) (theta:Vec) =
        f >> vector >> (*) theta

    let Train featurizer feature_to_optimize (dataset:Dataset<float>) =
        let Yt, Xt = 
            dataset.Observations
            |> Array.Parallel.map (fun obs -> feature_to_optimize obs, featurizer obs)
            |> Array.unzip
            |> fun (x, y) -> (Array.toList x, Array.toList y)
            
        let theta = estimate (vector Yt) (matrix (Xt))
        let predict = predictor featurizer theta
        // TODO: Gather information about all the public holidays and the opening days
        let holiday_filtered_prediction (obs:float[]) = 
            let store_open_index = dataset.Header |> Array.findIndex (fun name -> name = "Open")
            match obs.[store_open_index] <> 0.0 && predict obs > 0.0 with
            | true -> predict obs
            | false -> 0.0
        (theta , holiday_filtered_prediction)

