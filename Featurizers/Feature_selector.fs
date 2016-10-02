namespace Featurizers

open Utility_functions.Parsing
open System

module Main_featurizer =
    let evaluation = [|"Sales"; "Store"; "Customers"; "Open"|]

    let Header = 
        [|"Day_of_week"; 
          "Day_just_before_christmas"; 
          "Promotion"; 
          "Is_public_holiday";
          "Is_easter";
          "Is_christmas";
          "School_holiday";
          "Day_of_week_pow_2";
          "Day_of_week_pow_3";
          "Day_of_week_pow_4";
          "Day_of_week_pow_5";
          "Day_of_week_pow_6";
          "Day_of_week_times_promotion"|]
        |> Array.append evaluation

    let Transformation (index_of:Map<string, int>) (obs:float[]) = 
        let day_just_before_christmas = obs.[index_of.["Date_month"]] = 12. |> Boolean_to_float 
        let day_of_week = float obs.[index_of.["DayOfWeek"]]

        let promotion = float obs.[index_of.["Promo"]]

        let school_holiday = obs.[index_of.["SchoolHoliday"]]
        
        let is_public_holiday = obs.[index_of.["StateHoliday_a"]]
        let is_easter = obs.[index_of.["StateHoliday_b"]]
        let is_christmas = obs.[index_of.["StateHoliday_c"]]

        let basic_features = 
            [|day_of_week; 
              day_just_before_christmas; 
              promotion; 
              is_public_holiday;
              is_easter;
              is_christmas;
              school_holiday|]

        let powers_of_day_of_week = 
            [|Math.Pow(day_of_week, 2.0); 
              Math.Pow(day_of_week, 3.0); 
              Math.Pow(day_of_week, 4.0); 
              Math.Pow(day_of_week, 5.0); 
              Math.Pow(day_of_week, 6.0)|]

        let evaluation_features = evaluation |> Array.map (fun name -> obs.[index_of.[name]])

        basic_features 
        |> Array.append powers_of_day_of_week
        |> Array.append [| day_of_week * promotion |]
        |> Array.append evaluation_features

