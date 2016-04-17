module Startup

open Owin
open Microsoft.Owin
open System
open System.IO
open System.Drawing
open System.Drawing.Imaging;
open System.Reflection
open System.Threading.Tasks

let transform (input: string) =
    sprintf "%s transformed" input

type public Startup() = 

    let handleOwinContext (context:IOwinContext) =

        use writer = new StreamWriter(context.Response.Body)

        match context.Request.Method with
        | "POST" ->
            let userName = context.Request.Headers.Get("userName")
            let fileName = context.Request.Headers.Get("fileName")
            let currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            let directory = Path.Combine(currentDir, "images", userName);
            Directory.CreateDirectory(directory) |> ignore
            let filePath = Path.Combine(directory, fileName);
            Console.WriteLine(filePath);
            use img = System.Drawing.Image.FromStream(context.Request.Body);
            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            let url = Path.Combine("http://localhost:9000/images", userName, fileName);
            writer.Write(url);
        | _ ->
            context.Response.StatusCode <- 400
            writer.Write("Only POST")

    let owinHandler = fun (context:IOwinContext) (_:Func<Task>) -> 
        handleOwinContext context; 
        Task.FromResult(null) :> Task

    member x.Configuration (app:IAppBuilder) = app.UseFileServer(true).Use(owinHandler) |> ignore

let private Main () =
    printfn "%s" (transform "some input")
