namespace Fazo

module Conf =

    let getConf = Conf.Load("conf.json")

module App =

    let getRootUrl = 
        let conf = Conf.getConf
        sprintf "http://*:%d" conf.Port