package controller

import (
	"github.com/jesusslim/slimgo"
	"github.com/jesusslim/slimmysql"
	"math/rand"
	"strconv"
)

type TestController struct {
	slimgo.Controller
}

func (this *TestController) Index() {
	funcs := []string{
		"test",
		"list",
		"list2",
		"condition",
		"join",
		"insert?nickname=Test" + strconv.Itoa(rand.Intn(100)),
		"trans",
		"cookie",
		"jsonp?callback=test",
		"xml",
		"stoptimetask",
		"restarttimetask",
		"session1",
		"session2",
		"index2",
		"index3",
	}
	html := ""
	for _, v := range funcs {
		html += "<a href='/test/" + v + "' target='_blank'>The example link of:" + v + "</a><br>"
	}
	this.Context.WriteString(html)
}

func (this *TestController) Test() {
	this.Data["json"] = slimgo.AppName
	this.ServeJson()
}

func (this *TestController) List() {
	sm, _ := slimmysql.NewSqlInstanceDefault()
	stds, err := sm.Table("students").Select()
	if err != nil {
		this.Data["json"] = err.Error()
	} else {
		this.Data["json"] = stds
	}
	this.ServeJson()
}

func (this *TestController) List2() {
	sm, _ := slimmysql.NewSqlInstance("biteabc")
	stds, err := sm.Table("students").Select()
	if err != nil {
		this.Data["json"] = err.Error()
	} else {
		this.Data["json"] = stds
	}
	this.ServeJson()
}

func (this *TestController) Condition() {
	sm, _ := slimmysql.NewSqlInstanceDefault()
	condition := map[string]interface{}{
		"nickname__like": "sl",
	}
	stds, err := sm.Table("students").Where(condition).GetField("id,nickname")
	if err != nil {
		this.Data["json"] = err.Error()
	} else {
		this.Data["json"] = map[string]interface{}{
			"students": stds,
		}
	}
	this.ServeJson()
}

func (this *TestController) Join() {
	sm, _ := slimmysql.NewSqlInstanceDefault()
	condition := sm.NewCondition()
	condition["relation"] = "OR"
	condition["age__egt"] = 20
	condition["_"] = map[string]interface{}{
		"gender":         1,
		"nickname__like": "sl",
	}
	r, _ := sm.MustMaster(true).Table("students").Where(condition).Order("age desc").Select()
	sql1 := sm.GetSql(true)
	r2, _ := sm.Clear().MustMaster(true).Table("students").Join("INNER JOIN go_order as o on o.sid=go_students.id").Group("sid").GetField("sid,sum(price) as total,count(o.id) as count")
	sql2 := sm.GetSql(true)
	this.ServeJson(map[string]interface{}{
		"result1": r,
		"sql1":    sql1,
		"result2": r2,
		"sql2":    sql2,
	})
}

func (this *TestController) Insert() {
	sm, _ := slimmysql.NewSqlInstanceDefault()
	nickname := this.Context.Request.FormValue("nickname")
	id, err := sm.Table("students").Add(map[string]interface{}{
		"nickname": nickname,
	})
	if err != nil {
		this.Data["json"] = err.Error()
	} else {
		this.Data["json"] = "insert success,id:" + strconv.Itoa(int(id))
	}
	this.ServeJson()
}

func (this *TestController) Trans() {
	sm, _ := slimmysql.NewSqlInstanceDefault()
	sm.StartTrans()
	p1, _ := sm.Table("students").Find(2)
	sm.Table("students").Where("id = 2").Save(map[string]interface{}{"nickname": "BigDaddy"})
	p2, _ := sm.Table("students").Find(2)
	sm.Rollback()
	p3, _ := sm.Table("students").Find(3)
	this.ServeJson(map[string]interface{}{
		"p1": p1,
		"p2": p2,
		"p3": p3,
	})
}

func (this *TestController) Cookie() {
	test1 := this.Context.GetCookie("test2")
	if test1 != "" {
		this.Data["json"] = map[string]string{
			"Result": "find cookie",
			"Cookie": test1,
		}
	} else {
		this.Context.SetCookie("test2", "ooooooook", 200)
		this.Data["json"] = map[string]string{
			"Result": "not found,set it",
		}
	}
	this.ServeJson()
}

func (this *TestController) Jsonp() {
	this.ServeJsonp(map[string]string{
		"nickname": "jsonp",
		"age":      "27",
	})
}

func (this *TestController) Xml() {
	this.ServeXml("testxml")
}

func (this *TestController) StopTimeTask() {
	err := slimgo.ShutDownTimeTask("showtime")
	if err != nil {
		this.ServeJson(err.Error())
	} else {
		this.ServeJson("shut down success")
	}
}

func (this *TestController) RestartTimeTask() {
	err := slimgo.RestartTimeTask("showtime")
	if err != nil {
		this.ServeJson(err.Error())
	} else {
		this.ServeJson("restart success")
	}
}

func (this *TestController) Session1() {
	session := this.Context.Input.Session
	user := session.Get("user")
	if user == nil {
		session.Set("user", map[string]interface{}{
			"nickname": "Slim",
			"age":      18,
		})
		this.Data["json"] = "no session,set it"
	} else {
		this.Data["json"] = user
	}
	this.ServeJson()
}

func (this *TestController) Session2() {
	session := this.Context.Input.Session
	user := session.Get("user")
	if user == nil {
		session.Set("user", map[string]interface{}{
			"nickname": "Slim",
			"age":      18,
		})
		this.Data["json"] = "no session,set it"
	} else {
		user.(map[string]interface{})["nickname"] = 1234
		session.Set("user", user)
		this.Data["json"] = user
	}
	this.ServeJson()
}

func (this *TestController) Index2() {
	this.Data["Test"] = "Test"
	this.Data["Std"] = map[string]interface{}{
		"nickname": "slim",
		"age":      18,
	}
}

func (this *TestController) Index3() {
	this.Data["Test"] = "Test in index3"
	this.Data["Std"] = map[string]interface{}{
		"nickname": "Lawliet",
		"age":      20,
	}
	this.SetView("test/index2.html")
}
