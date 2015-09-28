using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BLL
{
    public class BaseBLL<T> where T : MyModelBase
    {
        /// <summary>
        /// 数据层接口对象
        /// </summary>
        public DAL.BaseDAL<T> idal = new DAL.BaseDAL<T>();

        #region 1.0添加数据 + Add(Tclass model)
        /// <summary>
        /// 1.0添加数据
        /// </summary>
        /// <param name="model"></param>
        public void Add(T model)
        {
            idal.Add(model);
        }
        #endregion

        #region  2.0 删除方法1 删除给定的对象 +Del(Tclass model)
        /// <summary>
        /// 2.0 删除方法1 删除给定的对象
        /// </summary>
        /// <param name="model"></param>        
        /// <param name="IsSoftDel">是否软删除[*如果是软删除 则自动保存*]</param>
        /// <param name="Pro">软删除 要修改的删除标志字段</param>
        /// <param name="DelTag">软删除 删除标志值</param>
        /// <returns>如果非软删除则永远返回true 可以忽略</returns>
        public void Del(T model, bool IsSoftDel, string Pro = "IsDel", bool DelTag = true)
        {
            idal.Del(model, IsSoftDel, Pro, DelTag);
        }
        #endregion

        #region  2.1 删除方法2 根据条件删除对象 +Del(Expression<Func<Tclass, bool>> delWhere)
        /// <summary>
        /// 2.1 删除方法2 根据条件删除对象 
        /// </summary>
        /// <param name="delWhere">删除条件</param>
        /// <param name="IsSoftDel">是否软删除</param>
        /// <param name="Pro">软删除 要修改的删除标志字段</param>
        /// <param name="DelTag">软删除 删除标志值</param>
        public void Del(Expression<Func<T, bool>> delWhere, bool IsSoftDel, string Pro = "IsDel", bool DelTag = true)
        {

            idal.Del(delWhere, IsSoftDel, Pro, DelTag);
        }
        #endregion

        #region 3.0 修改方法1 修改某个实体的 某些属性 +Up(Tclass model, params string[] strparams)
        /// <summary>
        /// 3.0 修改方法1 修改某个实体的 某些属性(根据id修改)【*用这个需要注意关闭检查】
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strparams">可变参数</param>
        public void Up(T model, params string[] strparams)
        {
            idal.Up(model, strparams);
        }

        /// <summary>
        /// 3.0 修改方法1.1 修改某个实体的 (根据id修改)【*用这个需要注意关闭检查】
        /// </summary>
        /// <param name="model"></param>
        public void Up(T model)
        {
            idal.Up(model);
        }
        #endregion

        #region 3.1 修改方法2 根据条件 修改指定的 属性 值 +Up(Expression<Func<Tclass, bool>> upWhere, Tclass model, params string[] strparame)
        /// <summary>
        /// 3.1 修改方法2 根据条件 修改指定的 属性 值
        /// </summary>
        /// <param name="upWhere">要修改的数据的 条件</param>
        /// <param name="IsUpDelData">是否修改已经软删除过的数据</param>
        /// <param name="model">要修改的model对象</param>
        /// <param name="strparame">要修改的字段名</param>
        public void Up(Expression<Func<T, bool>> upWhere, bool IsUpDelData, T model, params string[] strparame)
        {
            idal.Up(upWhere, IsUpDelData, model, strparame);
        }
        #endregion

        #region  4.0 查询方法 +GetList<Tkey>(Expression<Func<Tclass, bool>> strWhere, Expression<Func<Tclass, Tkey>> strOrederBy = null, bool order = true)
        /// <summary>
        /// 4.0 查询方法 
        /// </summary>
        /// <typeparam name="Tkey">如果strOrederBy为null Tkey也可以DBNull</typeparam>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序条件</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> strWhere, bool SelectDelData = false, bool isAsNoTracking = true, string tableName = null)
        {
            return idal.GetList(strWhere, SelectDelData, isAsNoTracking, tableName);
        }
        /// <summary>
        /// 4.0.2 查询方法 
        /// </summary>
        /// <typeparam name="Tkey">如果strOrederBy为null Tkey也可以DBNull</typeparam>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序条件</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList<TTb>(Expression<Func<T, bool>> strWhere, bool SelectDelData = false, bool isAsNoTracking = true, Expression<Func<T, TTb>> tableName = null)
        {
            return idal.GetList(strWhere, SelectDelData, isAsNoTracking, tableName);
        }

        #endregion

        #region 4.1 查询方法2 分页查询 +GetList<Tkey>(int indexPage, int sizePage, Expression<Func<Tclass, bool>> strWhere, Expression<Func<Tclass, Tkey>> strOrederBy = null, bool order = true)
        /// <summary>
        /// 4.1 查询方法2 分页查询
        /// </summary>
        /// <typeparam name="Tkey">如果strOrederBy为null Tkey也可以DBNull</typeparam>
        /// <param name="indexPage">页码(从1开始）</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="total">总页数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序字段</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList<Tkey>(int indexPage, int sizePage, out int total, Expression<Func<T, bool>> strWhere, bool SelectDelData = false, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true, bool isAsNoTracking = true, string tableName = null)
        {
            return idal.GetList(indexPage, sizePage, out total, strWhere, SelectDelData, strOrederBy, order, isAsNoTracking, tableName);
        }

        /// <summary>
        /// 4.1.2 查询方法2 分页查询
        /// </summary>
        /// <typeparam name="Tkey">如果strOrederBy为null Tkey也可以DBNull</typeparam>
        /// <param name="indexPage">页码(从1开始）</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="total">总页数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="SelectDelData">查询的结果集中 是否包括 已经软删除的数据</param>
        /// <param name="strOrederBy">排序字段</param>
        /// <param name="order">是否升序</param>
        ///  <param name="tableName">连接查询 的表名</param>
        /// <returns></returns>
        public IQueryable<T> GetList<Tkey, TTb>(int indexPage, int sizePage, out int total, Expression<Func<T, bool>> strWhere, bool SelectDelData = false, Expression<Func<T, Tkey>> strOrederBy = null, bool order = true, bool isAsNoTracking = true, Expression<Func<T, TTb>> tableName = null)
        {
            return idal.GetList(indexPage, sizePage, out total, strWhere, SelectDelData, strOrederBy, order, isAsNoTracking, tableName);
        }


        #endregion

        #region 提交
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ValidateOnSaveEnabled">是否打开验证</param>
        /// <returns></returns>
        public int save(bool ValidateOnSaveEnabled = true)
        {
            return idal.save(ValidateOnSaveEnabled);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ValidateOnSaveEnabled"></param>
        /// <returns></returns>
        public static int StaticSave(bool ValidateOnSaveEnabled = true)
        {
            return DAL.BaseDAL<T>.StaticSave(ValidateOnSaveEnabled);
        }  
        #endregion
    }
}
