namespace Rossmann

open Utility_functions.Parsing
open System

module Feature_selector = () (*

    let Features (obs:Datapoint) = 
        let constant = 1.0
        let day_of_year = float obs.Date.DayOfYear
        let day_just_before_christmas = day_of_year > 325.0 |> Boolean_to_float 
        let day_of_week = float obs.DayOfWeek
        let promotion = float obs.Promo
        let school_holiday = obs.SchoolHoliday |> Boolean_to_float
        let is_public_holiday = obs.StateHoliday = "a" |> Boolean_to_float
        let is_easter = obs.StateHoliday = "b" |> Boolean_to_float
        let is_christmas = obs.StateHoliday = "c" |> Boolean_to_float
        // let is_not_holiday = obs.StateHoliday = "0" |> Boolean_to_float TODO find out why it ruins the result
        // Comment: The order of the variables is significant!
        // TODO: Remove insignificant factors
        let basic_features = 
            [day_of_week; 
            day_just_before_christmas; 
            promotion; 
            is_public_holiday;
            is_easter;
            is_christmas;
            school_holiday]

        let powers_of_day_of_week = 
            [Math.Pow(day_of_week, 2.0); 
             Math.Pow(day_of_week, 3.0); 
             Math.Pow(day_of_week, 4.0); 
             Math.Pow(day_of_week, 5.0); 
             Math.Pow(day_of_week, 6.0)]

        basic_features 
        |> List.append powers_of_day_of_week
        |> List.append [ day_of_week * promotion ]

    let Featurizer (obs:Datapoint) = Features obs*)

