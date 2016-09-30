namespace Global_configurations

module Data_types = 
    type Dataset<'a> = { Header: string[]; Observations: 'a[][] }
    type Feature<'a> = { Name: string; Observations: 'a[]}
    type Featurizer = float[] -> float list
    type Model = float[] -> float