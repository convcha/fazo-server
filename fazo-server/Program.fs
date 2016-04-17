open System
open Microsoft.Owin.Hosting
open Startup

[<EntryPoint>]
let main _ =
    let baseAddress = "http://*:9000"
    use app = WebApp.Start<Startup>(baseAddress)

    Console.WriteLine("Listening at {0}", baseAddress)
    Console.WriteLine("Press any key to stop")

    Console.ReadLine() |> ignore

    0