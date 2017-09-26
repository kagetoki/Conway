// Learn more about F# at http://fsharp.org

open System
open ConwayGame
[<EntryPoint>]
let main argv =
    ConwayGame.launchConwayAsync() |> ignore
    Console.ReadLine () |> ignore
    0 // return an integer exit code
