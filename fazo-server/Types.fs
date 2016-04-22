namespace Fazo

open FSharp.Data

[<AutoOpen>]
module Types =

    type Conf = JsonProvider<"conf.json">