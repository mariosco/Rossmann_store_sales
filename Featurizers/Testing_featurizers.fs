namespace Featurizers

open Utility_functions

module Testing_featurizers =
    let Trivial_transformation indices_to_remove = Arrays.filter_elements_at_indices indices_to_remove >> Array.toList

