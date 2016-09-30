namespace Global_configurations

open System.IO

module Config =
    let private data = Path.Combine(@"C:\", "Temp", "RossmannData")
    let private raw_data =  Path.Combine(data, "RawData")

    module File_extension =
        let csv filename = Path.ChangeExtension(filename, "csv")

    module File_locations =
        let Raw_trainig_set = Path.Combine(raw_data, "train") |> File_extension.csv
        let Initial_dataset = Path.Combine(data, "InitialDataset")
        let Features = Path.Combine(data, "Features")
        let Datasets = Path.Combine(data, "Datasets")

    module Data_parameters =
        let Number_of_observations = 1017209
