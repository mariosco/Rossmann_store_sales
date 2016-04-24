namespace Global_configurations

module Config =
    module File_locations =
        let Datasets_features = @"C:\Users\Marios\Desktop\Rossmann_datasets_features\"
        let Raw_data = Datasets_features + @"Raw_data\"
        let Raw_trainigset = Raw_data + @"train.csv"
        let Initial_dataset = Datasets_features + @"Initial_dataset\"
        let Features = Datasets_features + @"Features\"
        let Datasets = Datasets_features + @"Datasets\"

    module File_endings =
        let csv = @".csv"

    module Data_parameters =
        let Number_of_observations = 1017209
