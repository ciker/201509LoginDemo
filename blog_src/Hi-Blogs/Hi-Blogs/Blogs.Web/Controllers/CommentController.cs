using Blogs.BLL.Common;
using Blogs.Common.CustomModel;
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    /// <summary>
    /// 评论操作和显示
    /// </summary>
    public class CommentController : Controller
    {
        #region 01写入评论内容
        /// <summary>
        /// 写入评论内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string WriteContent()
        {
            if (null == BLLSession.UserInfoSessioin)
            {
                return new JSData()
                {
                    Messg = "您还未登录~",
                    State = EnumState.异常或Session超时
                }.ToJson();
            }
            if (BLLSession.UserInfoSessioin.IsLock)
            {
                return new JSData()
                {
                    Messg = "您的账户已经被锁定,请联系管理员~",
                    State = EnumState.失败
                }.ToJson();
            }
            //|| MySession.UserInfoSessioin.IsLock)
            //    return "no";

            var BlogId = int.Parse(Request.Form["BlogId"]);
            var UserId = BLLSession.UserInfoSessioin.Id; //int.Parse(Request.Form["UserId"]);
            var CommentID = int.Parse(Request.Form["CommentID"]);
            var Content = Request.Form["Content"];
            var ReplyUserID = int.Parse(Request.Form["ReplyUser"]);

            if (Content.Length >= 1000)
            {
                return new JSData()
                {
                    State = EnumState.失败
                }.ToJson();
            }

            var ReplyUserName = string.Empty;
            var User = BLL.Common.CacheData.GetAllUserInfo().Where(t => t.Id == ReplyUserID).FirstOrDefault();
            if (null != User)
            {
                ReplyUserName = string.IsNullOrEmpty(User.UserNickname) ? User.UserName : User.UserNickname;
            }

            BLL.BlogCommentSetBLL comment = new BLL.BlogCommentSetBLL();
            comment.Add(new BlogCommentSet()
            {
                BlogUsersId = UserId,
                BlogsId = BlogId,
                Content = Content,
                CommentID = CommentID,
                ReplyUserID = ReplyUserID,
                ReplyUserName = ReplyUserName,
                IsInitial = CommentID == -1
            });

            BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
            var blogmode = blogbll.GetList(t => t.Id == BlogId).FirstOrDefault();
            if (null == blogmode.BlogCommentNum)
            {
                blogmode.BlogCommentNum = comment.GetList(t => t.BlogsId == BlogId).Count() + 1;
            }
            else
            {
                blogmode.BlogCommentNum++;
            }
            blogbll.Up(blogmode, "BlogCommentNum");

            comment.save();

            return new JSData()
            {
                //这里发表成功    就不提示了。
                State = EnumState.成功
            }.ToJson();
        }
        #endregion

        #region 02加载 分布试图 （评论部分）
        /// <summary>
        /// 加载 分布试图 （评论部分）
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadComment()
        {
            int blogId = int.Parse(Request.Form["blogID"]);
            int pageIndex = int.Parse(Request.Form["pageIndex"]);
            BLL.CommentHandle com = new BLL.CommentHandle();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var comObj = com.GetComment(blogId, pageIndex);
            if (null == comObj)
                return PartialView("Null");
            dic.Add("commentList", comObj);//对应的评论
            dic.Add("SessionUser", BLL.Common.BLLSession.UserInfoSessioin);
            return PartialView(dic);
        }
        #endregion

        public ActionResult Message()
        {
            return View();
        }

    }
}
