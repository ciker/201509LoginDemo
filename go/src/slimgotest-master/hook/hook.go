package hook

import (
	"fmt"
	"github.com/jesusslim/slimgo"
	"github.com/jesusslim/slimgo/context"
)

func init() {
	slimgo.RegisterHookBeforeHttpPre("showreqinfo", showReqInfo)
}

func showReqInfo(ctx *context.Context) {
	fmt.Println("Req url is:", ctx.Request.URL.Path, ",method is:", ctx.Request.Method)
}
