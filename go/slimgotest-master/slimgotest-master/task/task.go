package task

import (
	"fmt"
	"github.com/jesusslim/slimgo"
	"github.com/jesusslim/slimgo/utils"
	"math/rand"
	"time"
)

func init() {
	slimgo.RegisterTimeTask("showtime", showTime, 5, false)
}

func showTime() error {
	fmt.Println("现在的时间是:", utils.TimeFormat(time.Now(), "Y-m-d H:i:s"), " AND the rand is ", test1())
	return nil
}

func test1() int {
	return rand.Intn(2000)
}
