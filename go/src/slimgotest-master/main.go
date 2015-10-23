package main

import (
	"github.com/jesusslim/slimgo"
	"slimgotest-master/controller"
	"slimgotest-master/controller/Admin"
	_ "slimgotest-master/hook"
	_ "slimgotest-master/model"
	_ "slimgotest-master/task"
)

func main() {
	slimgo.SlimApp.Handerlers.Register(&controller.IndexController{}, &controller.TestController{})
	slimgo.SlimApp.Handerlers.Register(&Admin.CommonController{})
	slimgo.Run(":9022")
}
