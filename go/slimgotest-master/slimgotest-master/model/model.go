package model

import (
	"github.com/Unknwon/goconfig"
	//	"github.com/jesusslim/slimgo"
	"github.com/jesusslim/slimmysql"
)

func init() {
	conf, _ := goconfig.LoadConfigFile("./conf/db.ini")
	conf_sq := "local"
	slimmysql.RegisterConnectionDefault(conf.MustBool(conf_sq, "rwseparate"), conf.MustValue(conf_sq, "host"), conf.MustValue(conf_sq, "port"), conf.MustValue(conf_sq, "db"), conf.MustValue(conf_sq, "user"), conf.MustValue(conf_sq, "pass"), conf.MustValue(conf_sq, "prefix"), false)
	conf_sq = "company"
	slimmysql.RegisterConnection("biteabc", conf.MustBool(conf_sq, "rwseparate"), conf.MustValue(conf_sq, "host"), conf.MustValue(conf_sq, "port"), conf.MustValue(conf_sq, "db"), conf.MustValue(conf_sq, "user"), conf.MustValue(conf_sq, "pass"), conf.MustValue(conf_sq, "prefix"), false)
}
