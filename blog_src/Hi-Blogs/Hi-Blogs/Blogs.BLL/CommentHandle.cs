using Blogs.BLL.Common;
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BLL
{
    /// <summary>
    /// 评论数据 操作逻辑类
    /// </summary>
    public class CommentHandle
    {
        #region 根据博客文章id 取相关评论  ()
        /// <summary>
        /// 根据博客文章id 取相关评论  ()
        /// </summary>
        /// <param name="blogId"></param>
        public List<List<BlogCommentSet>> GetComment(int blogId, int pageIndex)
        {

            int total;
            BLL.BlogCommentSetBLL com = new BlogCommentSetBLL();
            //IsInitial == true 父评论 （第一次数据库查询：查询30条父评论）
            List<int> disCom = com.GetList<int>(pageIndex, 30, out total, t => t.IsInitial == true && t.BlogsId == blogId,
                                false, t => t.Id).Select(t => t.Id).ToList();
            if (pageIndex > total)//已经没有评论信息了
            {
                return null;
            }
            //第二次数据库查询：查询30条父评论 和30条父评论下的子评论
            var listCom = com.GetList(t => disCom.Contains(t.CommentID) || disCom.Contains(t.Id)).ToList();
            List<List<BlogCommentSet>> ComObj = new List<List<BlogCommentSet>>();
            var ini = listCom.Where(t => t.IsInitial == true).ToList();//这里就不查数据库了直接进行集合筛选
            //对评论进行分组（以父评论 分组）
            foreach (BlogCommentSet item in ini)
            {
                item.BlogUsersSet = CacheData.GetAllUserInfo().Where(t => t.Id == item.BlogUsersId).FirstOrDefault();
                var userobj = CacheData.GetAllUserInfo().Where(t => t.Id == item.ReplyUserID).FirstOrDefault();
                if (null != userobj)
                    item.ReplyUserName = userobj.UserNickname;
                //添加 以父评论 为一分组 的评论
                ComObj.Add(GetCom(item, listCom));
            }
            return ComObj;
        } 
        #endregion

        #region 取 顶级评论 及下的子评论
        /// <summary>
        /// 取 顶级评论 及下的子评论
        /// </summary>
        /// <param name="com"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<BlogCommentSet> GetCom(BlogCommentSet com, List<BlogCommentSet> list)
        {
            var li = list.Where(t => t.CommentID == com.Id).ToList();
            li.Insert(0, com);
            return li;
        } 
        #endregion
    }
}
