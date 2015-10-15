using System;
using System.Web.Mvc;

namespace FangsiChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["userid"] = Guid.NewGuid().ToString().ToUpper();
            Session["username"] = GenName();
            Session["deptname"] = "放肆雷特";
            Session["deptid"] = "88888";
            return View();
        }

        #region My Init
        System.Random rnd;
        string[] _firstName = new string[]{ "白","毕","卞","蔡","曹","岑","常","车","陈","成","程","池","邓","丁","范","方","樊","费","冯","符"
,"傅","甘","高","葛","龚","古","关","郭","韩","何","贺","洪","侯","胡","华","黄","霍","姬","简","江"
,"姜","蒋","金","康","柯","孔","赖","郎","乐","雷","黎","李","连","廉","梁","廖","林","凌","刘","柳"
,"龙","卢","鲁","陆","路","吕","罗","骆","马","梅","孟","莫","母","穆","倪","宁","欧","区","潘","彭"
,"蒲","皮","齐","戚","钱","强","秦","丘","邱","饶","任","沈","盛","施","石","时","史","司徒","苏","孙"
,"谭","汤","唐","陶","田","童","涂","王","危","韦","卫","魏","温","文","翁","巫","邬","吴","伍","武"
,"席","夏","萧","谢","辛","邢","徐","许","薛","严","颜","杨","叶","易","殷","尤","于","余","俞","虞"
,"元","袁","岳","云","曾","詹","张","章","赵","郑","钟","周","邹","朱","褚","庄","卓" };
        string _lastName = "是一种优秀的数据打包和数据交换的形式在当今大行于天下如果没有听说过它的大名那可真是孤陋寡闻了用描述数据的优势显而易见它具有结构简单便于人和机器阅读的双重功效并弥补了关系型数据对客观世界中真实数据描述能力的不足组织根据技术领域的需要制定出了的格式规范并相应的建立了描述模型简称各种流行的";
        #endregion

        #region GenName
        public string GenName()
        {
            rnd = new Random(System.DateTime.Now.Millisecond);
            return string.Format("{0}{1}{2}", _firstName[rnd.Next(_firstName.Length - 1)], _lastName.Substring(rnd.Next(0, _lastName.Length - 1), 1), _lastName.Substring(rnd.Next(0, _lastName.Length - 1), 1));
        }
        #endregion
    }
}