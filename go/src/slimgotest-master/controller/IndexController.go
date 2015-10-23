package controller

import (
	"github.com/jesusslim/slimgo"
)

type IndexController struct {
	slimgo.Controller
}

func (this *IndexController) Index() {
	this.Redirect("/test", 302)
}
