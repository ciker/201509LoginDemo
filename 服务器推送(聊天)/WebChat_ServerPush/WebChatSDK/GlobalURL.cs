using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK
{
    public class GlobalURL
    {
        /*
         *用户参数：
         *  ?UserId=dbadmin&
         *  PassWord=gdyy175
         */

        /// <summary>
        /// 当前版本
        /// </summary>
        public const int Version = 1;
        /// <summary>
        /// 公共地址(避免多次变更)
        /// </summary>
        private const string PUBLICURL = "http://localhost:62641/";
        /// <summary>
        /// 用户上线
        /// 参数如下：
        ///     用户参数
        ///     姓名,性别,年龄,邮箱
        /// </summary>
        public const string Register_User = PUBLICURL + "UserService.asmx/Register_User";
        /// <summary>
        /// 用户上线
        /// 参数如下：
        ///     用户参数
        /// </summary>
        public const string Verify_User = PUBLICURL + "UserService.asmx/Verify_User";
        /// <summary>
        /// 用户下线
        /// 参数如下：
        ///     用户参数
        /// </summary>
        public const string Downline_User = PUBLICURL + "UserService.asmx/Downline_User";
        /// <summary>
        /// 获取好友
        /// 参数如下：
        ///     用户参数
        /// </summary>
        public const string Get_Friends = PUBLICURL + "UserService.asmx/Get_Friends";
        /// <summary>
        /// 添加好友
        /// 参数如下：
        ///     用户参数
        /// </summary>
        public const string Add_Friend = PUBLICURL + "UserService.asmx/Add_Friend";
        /// <summary>
        /// 发送消息
        /// 参数如下：
        ///     用户参数
        ///     发送消息参数
        /// </summary>
        public const string Send_Msg = PUBLICURL + "MessageService.asmx/Send_Msg";
    }
}
