namespace CSAlgorithmsFSharp
    module Algorithms =
        let rec private AddElementToSublistsMap(e, list) = list |> List.map (fun x -> e::x)

        let rec private CalculateCombinations(list, take) =
            if take = 0 then []
            else if take = 1 then 
                match list with
                    |(h::t) -> t |> List.fold (fun a n -> [h; n]::a) []
                    |[] -> raise (System.ArgumentException("Not enough elements to take in this combination"))
            else
                list |> List.map (fun x -> let filteredList = list |> List.filter (fun y -> x != y) in 
                                           let subListOfCombinations = CalculateCombinations(filteredList, take - 1) in
                                           AddElementToSublistsMap(x, subListOfCombinations))