namespace Global_configurations

open System.IO

module Config =
    let private data = Path.Combine(@"C:\", "Temp", "RossmannData")
    let private raw_data =  Path.Combine(data, "RawData")

    module File_locations =
        let Raw_trainig_set = Path.Combine(raw_data, "train.csv")
        let Initial_dataset = Path.Combine(data, "InitialDataset")
        let Features = Path.Combine(data, "Features")
        let Datasets = Path.Combine(data, "Datasets")

    module File_endings =
        let csv = ".csv"

    module Data_parameters =
        let Number_of_observations = 1017209
