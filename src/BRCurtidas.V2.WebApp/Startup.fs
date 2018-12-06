namespace BRCurtidas.V2.WebApp

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.Razor
open Microsoft.Extensions.Logging

type Startup() =
    member __.ConfigureServices(services: IServiceCollection) =
        let sp  = services.BuildServiceProvider()
        let env = sp.GetService<IHostingEnvironment>()
        Path.Combine(env.ContentRootPath, "Views")
        |> services.AddRazorEngine
        |> ignore

    member __.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        let errorHandler (ex : Exception) (log : ILogger) =
            log.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
            clearResponse >=> setStatusCode 500
        if env.IsDevelopment() then 
            app.UseDeveloperExceptionPage() |> ignore
        app
            .UseGiraffeErrorHandler(errorHandler)
            .UseGiraffe(HttpHandlers.app)