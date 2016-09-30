namespace Utility_functions

module Parsing =
    let Sprintf_probability_percentage (probability:float) = 
        sprintf "%.3f %s" (probability * 100.0) @"%"

    let Boolean_to_float (b:bool) = if b then 1.0 else 0.0
