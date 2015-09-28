
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;

namespace Blogs.Helper
{
    public class MyDbContext
    {
        //public static Model1Container dbEntities
        //{
        //    get
        //    {
        //        DbContext dbContext = CallContext.GetData(typeof(MyDbContext).Name) as DbContext;
        //        if (dbContext == null)
        //        {
        //            dbContext = new Model1Container();
        //            dbContext.Configuration.ValidateOnSaveEnabled = false;
        //            //将新创建的 ef上下文对象 存入线程
        //            CallContext.SetData(typeof(MyDbContext).Name, dbContext);
        //        }
        //        return dbContext as Model1Container;
        //    }
        //}
    }
}
