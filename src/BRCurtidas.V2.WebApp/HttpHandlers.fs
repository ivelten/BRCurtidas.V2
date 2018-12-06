namespace BRCurtidas.V2.WebApp

open Giraffe
open Giraffe.Razor
open Microsoft.AspNetCore.Http
open BRCurtidas.V2.WebApp.Models

type HttpHandler = HttpFunc -> HttpContext -> HttpFuncResult

module HttpHandlers =
    let model = { WelcomeText = "Hello, World" }

    let app : HttpHandler =
        choose [
            route  "/" >=> razorView "text/html" "Index" (Some model) None
        ]