using Blogs.ModelDB;
using Blogs.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Blogs.DAL
{
    public class BaseDAL<T> where T : MyModelBase
    {
        //更改为存线程内
        Model1Container db = dbEntities;  //;// = new Model.qds114576568_dbEntities();      
        private static Model1Container dbEntities
        {
            get
            {
                DbContext dbContext = CallContext.GetData(typeof(BaseDAL<T>).Name) as DbContext;
                if (dbContext == null)
                {
                    dbContext = new Model1Container();
                    //dbContext.Configuration.ValidateOnSaveEnabled = false;
                    //将新创建的 ef上下文对象 存入线程
                    CallContext.SetData(typeof(BaseDAL<T>).Name, dbContext);                    
                    dbContext.Database.Log = Blogs.Helper.LogHelper.LogSave.TrackLogSave;
                }
                return dbContext as Model1Container;
            }
        }

        #region 1.0添加数据 + Add(T model)
        /// <summary>
        /// 1.0添加数据
        /// </summary>
        /// <param name="model"></param>
        public void Add(T model)
        {
            model.CreateTime = model.CreateTime == null ? DateTime.Now : model.CreateTime;
            model.UpTime = DateTime.Now;
            model.IsDel = false;
            db.Set<T>().Add(model);
        }
        #endregion

        #region  2.0 删除方法1 删除给定的对象 +Del(T model)
        /// <summary>
        /// 2.0 删除方法1 删除给定的对象
        /// </summary>
        /// <param name="model"></param>        
        /// <param name="IsSoftDel">是否软删除[*如果是软删除 则自动保存*]</param>
        /// <param name="Pro">软删除 要修改的删除标志字段</param>
        /// <param name="DelTag">软删除 删除标志值(一般true为软删除标志)</param>
        /// <returns>如果非软删除则永远返回true 可以忽略</returns>
        public void Del(T model, bool IsSoftDel, string Pro = "IsDel", bool DelTag = true)
        {
            if (!IsSoftDel)
            {
                //将实体 添加到上下文
                db.Set<T>().Attach(model);
                //把实体 标记为删除
                db.Set<T>().Remove(model);
            }
            else
            {
                var listPro = typeof(T).GetProperties().ToList();
                for (int i = 0; i < listPro.Count; i++)
                {
                    if (listPro[i].Name == Pro)
                    {
                        listPro[i].SetValue(model, DelTag, null);
                    }
                }
                Up(model, Pro);
            }
        }
        #endregion

        #region  2.1 删除方法2 根据条件删除对象 +Del(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 2.1 删除方法2 根据条件删除对象 
        /// </summary>
        /// <param name="delWhere">删除条件</param>
        /// <param name="IsSoftDel">是否软删除</param>
        /// <param name="Pro">软删除 要修改的删除标志字段</param>
        /// <param name="DelTag">软删除 删除标志值</param>
        public void Del(Expression<Func<T, bool>> delWhere, bool IsSoftDel, string Pro = "IsDel", bool DelTag = true)
        {
            //查询所有满足条件的实体对象
            var modelS = db.Set<T>().Where(delWhere).ToList();
            if (!IsSoftDel)
            {
                modelS.ForEach(m =>
                {
                    //附加到 上下文
                    db.Set<T>().Attach(m);
                    //标记为 删除状态
                    db.Set<T>().Remove(m);
                });
            }
            else //软删除
            {
                var listPro = typeof(T).GetProperties().ToList();
                foreach (var mymodel in modelS)
                {
                    for (int i = 0; i < listPro.Count; i++)
                    {
                        if (listPro[i].Name == Pro)
                        {
                            listPro[i].SetValue(mymodel, DelTag, null);
                        }
                        if (listPro[i].Name == "UpTime")
                        {
                            listPro[i].SetValue(mymodel, DateTime.Now, null);
                        }
                    }
                }
                #region 注释勿看
                //var listPro = typeof(T).GetProperties().ToList();
                //foreach (var mymodel in modelS)
                //{
                //    for (int i = 0; i < listPro.Count; i++)
                //    {
                //        if (listPro[i].Name == Pro)
                //        {
                //            listPro[i].SetValue(mymodel, DelTag, null);
                //        }
                //    }
                //}

                //Up(delWhere, null, Pro); 
                #endregion
            }
        }
        #endregion

        #region 3.0 修改方法1 修改某个实体的 某些属性 +Up(T model, params string[] strparams)
        /// <summary>
        /// 3.0 修改方法1 修改某个实体的 某些属性(根据id修改)【*用这个需要注意关闭检查,并且已经自动保存】
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strparams">可变参数</param>
        public void Up(T model, params string[] strparams)
        {
            //附加到上下文
            model.UpTime = DateTime.Now;
            var m = db.Entry<T>(model);
            //把全部属性标记为 没有修改
            m.State = System.Data.Entity.EntityState.Unchanged; //System.Data.EntityState.Unchanged;
            for (int i = 0; i < strparams.Length; i++)
            {
                //标记要修改的属性                
                m.Property(strparams[i]).IsModified = true;
            }
            m.Property("UpTime").IsModified = true;
        }

        public void Up(T model)
        {
            //var listPro = typeof(T).GetProperties().ToList();
            //var m = db.Entry<T>(model);
            //m.State = System.Data.Entity.EntityState.Unchanged; 
            //for (int i = 0; i < listPro.Count; i++)
            //{
            //    m.Property(listPro[i].Name).IsModified = true;
            //}

        }
        #endregion

        #region 3.1 修改方法2 根据条件 修改指定的 属性 值 +Up(Expression<Func<T, bool>> upWhere, T model, params string[] strparame)
        /// <summary>
        /// 3.1 修改方法2 根据条件 修改指定的 属性 值
        /// </summary>
        /// <param name="upWhere">要修改的数据的 条件</param>
        /// <param name="IsUpDelData">是否修改已经软删除过的数据</param>
        /// <param name="model">要修改的model对象</param>
        /// <param name="strparame">要修改的字段名</param>
        public void Up(Expression<Func<T, bool>> upWhere, bool IsUpDelData, T model, params string[] strparame)
        {

            model.UpTime = DateTime.Now;

            //查询出满足条件的所有实体
            var modelS = GetList(upWhere, IsUpDelData).ToList();  //db.Set<T>().Where(upWhere).ToList();
            //利用反射 获取 类 对象 的所有公共 属性 默认是[GetProperties(BindingFlags.Instance | BindingFlags.Public)]
            var listPro = typeof(T).GetProperties().ToList();
            // 属性对象 键值对
            List<PropertyInfo> dic = new List<PropertyInfo>();
            listPro.ForEach(l =>
            {
                for (int i = 0; i < strparame.Length; i++)
                {
                    //循环 判断 添加 需要修改的 属性对象
                    if (l.Name == strparame[i].Trim() || l.Name == "UpTime")
                    {
                        dic.Add(l);
                        break;
                    }
                }
            });

            if (dic.Count > 0)//判断 属性对象集合  是否 有 数据
            {
                foreach (var property in dic)
                {
                    //取 传过来的对象 里面的值
                    var newValue = property.GetValue(model, null);
                    foreach (var mymodel in modelS)
                    {
                        //修改到 对象集合
                        property.SetValue(mymodel, newValue, null);
                    }
                }
            }
        }
        #endregion

        #region  4.0 查询方法 +GetList<Tkey>(Expression<Func<T, bool>> strWhere, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true)
        /// <summary>
        /// 4.0 查询方法 
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序条件</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> strWhere, bool SelectDelData = false, bool isAsNoTracking = true, string tableName = null)
        {
            Expression<Func<T, bool>> exp = strWhere;
            if (!SelectDelData)
                exp = AddLinq.And(exp, GetWhereIsDel());//合并 排除一删除查询条件 

            IQueryable<T> t = null;
            //if (isAsNoTracking)
            //    t = db.Set<T>().AsNoTracking().Where(exp);
            //else
            //    t = db.Set<T>().Where(exp);

            if (isAsNoTracking)
                if (string.IsNullOrEmpty(tableName))
                    t = db.Set<T>().AsNoTracking().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).AsNoTracking().Where(exp);
            else
                if (string.IsNullOrEmpty(tableName))
                    t = db.Set<T>().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).Where(exp);

            return t;
        }

        /// <summary>
        /// 4.0.2 查询方法 
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序条件</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList<TTb>(Expression<Func<T, bool>> strWhere, bool SelectDelData = false, bool isAsNoTracking = true, Expression<Func<T, TTb>> tableName = null)
        {
            Expression<Func<T, bool>> exp = strWhere;
            if (!SelectDelData)
                exp = AddLinq.And(exp, GetWhereIsDel());//合并 排除一删除查询条件 

            IQueryable<T> t = null;
            if (isAsNoTracking)
                if (null == tableName)
                    t = db.Set<T>().AsNoTracking().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).AsNoTracking().Where(exp);
            else
                if (null == tableName)
                    t = db.Set<T>().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).Where(exp);

            return t;
        }
        #endregion

        #region 4.1 查询方法2 分页查询 +GetList<Tkey>(int indexPage, int sizePage, Expression<Func<T, bool>> strWhere, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true)
        /// <summary>
        /// 4.1 查询方法2 分页查询
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="indexPage">页码(从1开始)</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序字段</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList<Tkey>(int indexPage, int sizePage, out int total, Expression<Func<T, bool>> strWhere, bool SelectDelData = false, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true, bool isAsNoTracking = true, string tableName = null)
        {
            Expression<Func<T, bool>> exp = strWhere;
            if (!SelectDelData)
                exp = AddLinq.And(exp, GetWhereIsDel());//合并 排除一删除查询条件 

            IQueryable<T> t = null;
            if (isAsNoTracking)
                if (string.IsNullOrEmpty(tableName))
                    t = db.Set<T>().AsNoTracking().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).AsNoTracking().Where(exp);
            else
                if (string.IsNullOrEmpty(tableName))
                    t = db.Set<T>().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).Where(exp);

            if (strOrederBy != null)
            {
                if (order)
                    t = t.OrderBy(strOrederBy);
                else
                    t = t.OrderByDescending(strOrederBy);
            }
            int count = t.Count();
            total = count / sizePage + (count % sizePage > 0 ? 1 : 0);
            return t.Skip((indexPage - 1) * sizePage).Take(sizePage);
        }

        /// <summary>
        /// 4.1.2 查询方法2 分页查询
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="indexPage">页码(从1开始)</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序字段</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名拉姆达表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetList<Tkey, TTb>(int indexPage, int sizePage, out int total, Expression<Func<T, bool>> strWhere, bool SelectDelData = false, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true, bool isAsNoTracking = true, Expression<Func<T, TTb>> tableName = null)
        {
            Expression<Func<T, bool>> exp = strWhere;
            if (!SelectDelData)
                exp = AddLinq.And(exp, GetWhereIsDel());//合并 排除一删除查询条件 

            IQueryable<T> t = null;
            if (isAsNoTracking)
                if (null == tableName)
                    t = db.Set<T>().AsNoTracking().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).AsNoTracking().Where(exp);
            else
                if (null == tableName)
                    t = db.Set<T>().Where(exp);
                else
                    t = db.Set<T>().Include(tableName).Where(exp);

            if (strOrederBy != null)
            {
                if (order)
                    t = t.OrderBy(strOrederBy);
                else
                    t = t.OrderByDescending(strOrederBy);
            }
            int count = t.Count();
            total = count / sizePage + (count % sizePage > 0 ? 1 : 0);
            return t.Skip((indexPage - 1) * sizePage).Take(sizePage);
        }


        #endregion

        #region 5.0 返回 已经删除的 linq 拼接条件
        /// <summary>
        /// 返回 已经删除的 linq 拼接条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetWhereIsDel()
        {
            return t => t.IsDel == false;
        }
        #endregion

        #region 提交 +save()
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ValidateOnSaveEnabled">是否打开验证</param>
        /// <returns></returns>
        public int save(bool ValidateOnSaveEnabled = true)
        {
            db.Configuration.ValidateOnSaveEnabled = ValidateOnSaveEnabled;
            return db.SaveChanges();
        }

        public static int StaticSave(bool ValidateOnSaveEnabled = true)
        {
            dbEntities.Configuration.ValidateOnSaveEnabled = ValidateOnSaveEnabled;
            return dbEntities.SaveChanges();
        } 

        #endregion
    }
}
