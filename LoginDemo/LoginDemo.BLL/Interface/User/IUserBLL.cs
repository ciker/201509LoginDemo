using System;
using System.Threading.Tasks;
using LoginDemo.Commom;
using LoginDemo.Entity;


namespace LoginDemo.BLL.Interface
{
    public interface IUserBLL : IDependency
    {
        /// <summary>
        /// regist 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ReturnResponse<User> Register(User user);

        /// <summary>
        /// login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ReturnResponse<User> Login(User user);
        
        /// <summary>
        /// query User
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ReturnResponse<Pager<User>> GetUserListbyParameter(UserQueryParameter para);

        /// <summary>
        /// avaliable or forbidden
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ReturnResponse<bool> AvaliableOrForbiddenUser(User user);

    }
}
