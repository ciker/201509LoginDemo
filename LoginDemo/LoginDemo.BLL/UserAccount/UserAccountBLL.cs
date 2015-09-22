#region Using
using System.Linq;
using LoginDemo.BLL.Interface;
using LoginDemo.DAL.Interface;
using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
#endregion
namespace LoginDemo.BLL
{
    public class UserAccountBLL : IUserAccountBLL
    {
        #region properties

        private readonly IUserAccountDAL _userAccountDal;
        #endregion

        #region constructor
        public UserAccountBLL(IUserAccountDAL userAccountDal)
        {
            _userAccountDal = userAccountDal;
        }
        #endregion
        public ReturnResponse<UserInfo> Login(UserInfoAndAccount userInfo)
        {
            var response = new ReturnResponse<UserInfo>();

            userInfo.AccountType = userInfo.DefaultAccount.GetAccountType();
            var para = new UserInfoQueryParameter()
            {
                Account = userInfo.DefaultAccount,
                //Password = userInfo.Password.Md5Compute32(),
                UserAccountType = userInfo.AccountType,
                Skip = 0,
                Take = 1,
                IsPage = false
            };
            var res = Query(para);
            if (res.IsSuccess && res.Body.Items.Any())
            {
                response.Body = res.Body.Items.FirstOrDefault();
                if (response.Body != null && response.Body.DataStatus == 1)
                {
                    response.ResponseCode = 403;
                    response.Message = "user has been forbidden";
                }
                else if (response.Body != null && !userInfo.Password.Md5Compute32().Equals(response.Body.Password))
                {
                    response.ResponseCode = 400;
                    response.Message = "user name or password error";
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
                response.Message = "user name doesn't exist";
            }
            return response;
        }

        public ReturnResponse<UserInfo> Register(UserInfoAndAccount userInfo)
        {
            var response = new ReturnResponse<UserInfo>();
            if (string.IsNullOrWhiteSpace(userInfo.Accounts.FirstOrDefault()))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserName can't be empty";
                return response;
            }
            if (string.IsNullOrWhiteSpace(userInfo.Password))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserPWD can't be empty";
                return response;
            }

            var usersRes = Query(new UserInfoQueryParameter() { Account = userInfo.Accounts.FirstOrDefault(), Skip = 0, Take = 1, IsPage = false });

            if (usersRes.ResponseCode == 1 && usersRes.Body.Items.Any())
            {
                response.Body = new UserInfo();
                response.ResponseCode = 400;
                response.Message = "UserName was exist";
                return response;
            }
            userInfo.AccountType = userInfo.Accounts.FirstOrDefault().GetAccountType();
            userInfo.Password = userInfo.Password.Md5Compute32();

            var res = _userAccountDal.Save(userInfo); //_IContainer.Resolve<IUserDAL>().Save(user);
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

        public ReturnResponse<Pager<UserInfoAndAccount>> Query(UserInfoQueryParameter parameter)
        {
            return new ReturnResponse<Pager<UserInfoAndAccount>>
            {
                Body = _userAccountDal.Query(parameter),
                ResponseCode = 1,
                Message = "Success"
            };
        }
    }
}
