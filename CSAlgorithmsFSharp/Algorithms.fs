namespace CSAlgorithmsFSharp
    module Algorithms =
        let rec private CalculateCombinationsHelper(list, take, accum) =
            if take = 0 then accum
            else if take = 1 then 
                match list with
                    |(h::t) -> 
                        let result = List.fold (fun a n -> [h, n]::a) [] t in
                        result::accum
                    |[] -> raise (System.ArgumentException("Not enough elements to take in this combination"))
            else
                 let t = List.fold (fun a n -> let subList = list |> List.filter (fun t -> t != n) in
                                               let subListGen = CalculateCombinationsHelper(subList, take - 1, []) in
                                               List.fold (fun b m -> [n::m]::b) [] subListGen) [] list in
                 t::accum           