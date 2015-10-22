package main

import (
	"github.com/jesusslim/slimgo"
	"slimgotest/controller"
	"slimgotest/controller/Admin"
	_ "slimgotest/hook"
	_ "slimgotest/model"
	_ "slimgotest/task"
)

func main() {
	slimgo.SlimApp.Handerlers.Register(&controller.IndexController{}, &controller.TestController{})
	slimgo.SlimApp.Handerlers.Register(&Admin.CommonController{})
	slimgo.Run(":9022")
}
