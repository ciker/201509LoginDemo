namespace LoginDemo.BLL
{
    #region Using
    using Interface;
    using Commom;
    using DAL.Interface;
    using Entity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion
    public class UserBLL : IUserBLL
    {
        #region properties

        private readonly IUserDAL _iUserDal;
        //private ContainerBuilder _builder;
        //private IContainer _IContainer;
        #endregion

        #region constructor
        public UserBLL(IUserDAL userDal)
        {
            _iUserDal = userDal;
            #region regist examp
            //_builder = new ContainerBuilder();
            //
            //////如果为Winform类型，请使用以下获取Assembly方法
            ////_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())//查找程序集中以services结尾的类型
            ////.Where(t => t.Name.EndsWith("Services"))
            ////.AsImplementedInterfaces();
            ////_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())//查找程序集中以Repository结尾的类型
            ////.Where(t => t.Name.EndsWith("Repository"))
            ////.AsImplementedInterfaces();

            ////如果有web类型，请使用如下获取Assenbly方法
            ////var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            ////_builder.RegisterAssemblyTypes(assemblys.ToArray())//查找程序集中以Repository结尾的类型
            ////.Where(t => t.Name.EndsWith("Repository"))
            ////.AsImplementedInterfaces();
            //
            ////_builder.RegisterType<UserQueryDAL>().As<IUserQueryDAL>();
            //_IContainer = _builder.Build();
            #endregion
        }
        #endregion

        /// <summary>
        /// regist 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ReturnResponse<User> Register(User user)
        {
            var response = new ReturnResponse<User>();
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserName can't be empty";
                return response;
            }
            if (string.IsNullOrWhiteSpace(user.UserPWD))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserPWD can't be empty";
                return response;
            }
            user.UserPWD = user.UserPWD.Md5Compute32();

            var usersRes = GetUserListbyParameter(new UserQueryParameter() { UserName = user.UserName, Skip = 0, Take = 1, IsPage = false });

            if (usersRes.IsSuccess && usersRes.Body.Items.Any())
            {
                response.Body = new User();
                response.ResponseCode = 400;
                response.Message = "UserName was exist";
                return response;
            }
            var res = _iUserDal.Save(user); //_IContainer.Resolve<IUserDAL>().Save(user);
            if (res != null && res.Id > 0)
            {
                response.Body = res;
                response.ResponseCode = 1;
                response.Message = "regist success";
            }
            else
            {
                response.Body = res;
                response.ResponseCode = -1;
                response.Message = " Insert failed";
            }
            return response;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ReturnResponse<User> Login(User user)
        {
            var response = new ReturnResponse<User>();
            var para = new UserQueryParameter() { UserName = user.UserName, UserPWD = user.UserPWD.Md5Compute32(), Skip = 0, Take = 1, IsPage = false };
            var res = GetUserListbyParameter(para);
            if (res.IsSuccess && res.Body.Items.Any())
            {
                response.Body = res.Body.Items.FirstOrDefault();
                if (response.Body != null && response.Body.DataStatus == 1)
                {
                    response.ResponseCode = 403;
                    response.Message = "user has been forbidden";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "login success";
                }
            }
            else
            {
                response.ResponseCode = 400;
                response.Message = "user name or password error";
            }
            return response;
        }

        public async void LoginAsync(User user)
        {
            Func<Task<ReturnResponse<User>>> taskFunc = () =>
            {
                return Task.Run(() =>
                    {
                        var response = new ReturnResponse<User>();
                        var paras = new UserQueryParameter() { UserName = user.UserName, UserPWD = user.UserPWD.Md5Compute32() };
                        var res = GetUserListbyParameter(paras);
                        if (res.IsSuccess && res.Body.Items.Any())
                        {
                            response.Body = res.Body.Items.FirstOrDefault();
                            if (response.Body.DataStatus == 1)
                            {
                                response.ResponseCode = 403;
                                response.Message = "user has been forbidden";
                            }
                            else
                            {
                                response.ResponseCode = 1;
                                response.Message = "login success";
                            }
                        }
                        else
                        {
                            response.ResponseCode = 400;
                            response.Message = "user name or password error";
                        }
                        return response;
                    });
            };
            await taskFunc();
        }

        public ReturnResponse<Pager<User>> GetUserListbyParameter(UserQueryParameter para)
        {
            para.Skip = para.PageIndex - 1;
            para.Take = para.PageSize;
            return new ReturnResponse<Pager<User>>
            {
                Body = _iUserDal.QueryUsersByParameter(para),
                ResponseCode = 1,
                Message = "Success"
            };

            #region get from redis

            //if (para != null && !para.IsLogin)
            //    using (var client = RedisManager.GetClient())
            //    {
            //        var dataList = client.Get<Pager<User>>("UsersDataPager_" + para.PageIndex);
            //        if (dataList != null && dataList.Items.Any())
            //        {
            //            response.Body = dataList;
            //            response.ResponseCode = 1;
            //            response.Message = "Success";
            //            return response;
            //        }
            //    }

            #endregion

            //_IContainer.Resolve<IUserDAL>().QueryUsersByParameter(para);

            #region set redis

            //if (para != null && !para.IsLogin)
            //    using (var redisClient = RedisManager.GetClient())
            //    {
            //        redisClient.Set("UsersDataPager_" + para.PageIndex, data);
            //    }

            #endregion

        }

        /// <summary>
        /// forbideen avaliable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ReturnResponse<bool> AvaliableOrForbiddenUser(User user)
        {
            throw new NotImplementedException();
        }



    }
}
