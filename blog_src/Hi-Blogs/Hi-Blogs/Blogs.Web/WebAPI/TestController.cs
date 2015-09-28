using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blogs.Web.WebAPI
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {

            Model1Container db = new Model1Container();
            db.Blogs.Where(t => t.BlogTitle == "留言板"
             ).ToList();

            BLL.BlogsBLL blogbll = new BLL.BlogsBLL();//|| t.BlogTitle.Contains(EntityFunctions.AsNonUnicode("取存过和函数"))
            var blogtemp = blogbll.GetList(t => t.BlogTitle == "留言板"
                || t.BlogTitle == "友情链接", isAsNoTracking: false).ToList();

            for (int i = 0; i < blogtemp.Count; i++)
            {
                blogtemp[i].BlogContent = "异常异fsdfs常异常";
            }
            blogbll.save();


            return new string[] { "value1", "value2", "admin".MD5().MD5(), "admin".MD5().MD5().MD5().MD5() };
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}