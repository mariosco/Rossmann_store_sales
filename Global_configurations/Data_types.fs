namespace Global_configurations

open System.Collections.Generic
open System.IO

module Data_types = 
    type Dataset<'a> = { Header: string[]; Observations: 'a[][] }
    type Feature<'a> = { Name: string; Observations: 'a[]}
    type Featurizer = float[] -> float list
    type Model = float[] -> float