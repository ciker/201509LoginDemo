
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Blogs.BLL
{
    #region 01  BlogCommentSetBLL
	public partial class BlogCommentSetBLL : BaseBLL<Blogs.ModelDB.BlogCommentSet>
    {        
        public Blogs.DAL.BlogCommentSetDAL My_BlogCommentSetDAL = new DAL.BlogCommentSetDAL();         
    }
    #endregion

    #region 02  BlogReadInfoBLL
	public partial class BlogReadInfoBLL : BaseBLL<Blogs.ModelDB.BlogReadInfo>
    {        
        public Blogs.DAL.BlogReadInfoDAL My_BlogReadInfoDAL = new DAL.BlogReadInfoDAL();         
    }
    #endregion

    #region 03  BlogsBLL
	public partial class BlogsBLL : BaseBLL<Blogs.ModelDB.Blogs>
    {        
        public Blogs.DAL.BlogsDAL My_BlogsDAL = new DAL.BlogsDAL();         
    }
    #endregion

    #region 04  BlogTagsBLL
	public partial class BlogTagsBLL : BaseBLL<Blogs.ModelDB.BlogTags>
    {        
        public Blogs.DAL.BlogTagsDAL My_BlogTagsDAL = new DAL.BlogTagsDAL();         
    }
    #endregion

    #region 05  BlogTypesBLL
	public partial class BlogTypesBLL : BaseBLL<Blogs.ModelDB.BlogTypes>
    {        
        public Blogs.DAL.BlogTypesDAL My_BlogTypesDAL = new DAL.BlogTypesDAL();         
    }
    #endregion

    #region 06  BlogUsersSetBLL
	public partial class BlogUsersSetBLL : BaseBLL<Blogs.ModelDB.BlogUsersSet>
    {        
        public Blogs.DAL.BlogUsersSetDAL My_BlogUsersSetDAL = new DAL.BlogUsersSetDAL();         
    }
    #endregion

    #region 07  UserInfoBLL
	public partial class UserInfoBLL : BaseBLL<Blogs.ModelDB.UserInfo>
    {        
        public Blogs.DAL.UserInfoDAL My_UserInfoDAL = new DAL.UserInfoDAL();         
    }
    #endregion

}