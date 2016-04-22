namespace Fazo

open System
open Microsoft.Owin.Hosting
open Startup

module Program =

    [<EntryPoint>]
    let main _ =
        let rootUrl = App.getRootUrl
        use app = WebApp.Start<Startup>(rootUrl)

        Console.WriteLine("Listening at {0}", rootUrl)
        Console.WriteLine("Press any key to stop")

        Console.ReadLine() |> ignore

        0