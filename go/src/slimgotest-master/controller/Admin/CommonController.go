package Admin

import (
	"github.com/jesusslim/slimgo"
)

type CommonController struct {
	slimgo.Controller
}

func (this *CommonController) Show() {
	this.Data["json"] = "show it"
	this.ServeJson(nil)
}
