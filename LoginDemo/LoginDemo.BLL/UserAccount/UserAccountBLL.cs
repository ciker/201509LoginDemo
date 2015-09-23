#region Using

using System.Collections.Generic;
using System.Linq;
using LoginDemo.BLL.Interface;
using LoginDemo.DAL.Interface;
using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount.QueryParameter;
using LoginDemo.ViewModels.UserInfo;
using System.Transactions;
using LoginDemo.Entity.UserAccount;

#endregion
namespace LoginDemo.BLL
{
    public class UserAccountBLL : IUserAccountBLL
    {
        #region properties

        private readonly IUserInfoDAL _userInfoDal;
        private readonly IUserInfoAccountDAL _userInfoAccountDal;
        #endregion

        #region constructor
        public UserAccountBLL(IUserInfoDAL userInfoDal, IUserInfoAccountDAL userInfoAccountDal)
        {
            _userInfoDal = userInfoDal;
            _userInfoAccountDal = userInfoAccountDal;
        }
        #endregion
        public ReturnResponse<UserInfoViewModels> Login(UserInfoViewModels userInfo)
        {
            var response = new ReturnResponse<UserInfoViewModels>();
            var para = new UserInfoQueryParameter()
            {
                Account = userInfo.Account,
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

        public ReturnResponse<UserInfoViewModels> Register(UserInfoViewModels userInfo)
        {
            var response = new ReturnResponse<UserInfoViewModels>();
            if (string.IsNullOrWhiteSpace(userInfo.Account))
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

            var usersRes = Query(new UserInfoQueryParameter() { Account = userInfo.Account, Skip = 0, Take = 1, IsPage = false });

            if (usersRes.ResponseCode == 1 && usersRes.Body.Items.Any())
            {
                response.Body = new UserInfoViewModels();
                response.ResponseCode = 400;
                response.Message = "UserName was exist";
                return response;
            }
            userInfo.Password = userInfo.Password.Md5Compute32();

            using (var trans = new TransactionScope())
            {
                try
                {
                    var resUserInfo = _userInfoDal.Save(new UserInfo()
                    {
                        Password = userInfo.Password,
                        Address = userInfo.Address,
                        CompanyName = userInfo.CompanyName,
                        NickName = userInfo.NickName,
                        Gender = userInfo.Gender,
                        Remark = userInfo.Remark
                    }); //_IContainer.Resolve<IUserDAL>().Save(user);
                    if (resUserInfo.Id > 0)
                    {
                        var account = _userInfoAccountDal.Save(new UserInfoAccount()
                        {
                            UserInfoID = resUserInfo.Id,
                            Account = userInfo.Account,
                            UserAccountType = userInfo.AccountType
                        });
                        userInfo.Id = resUserInfo.Id;
                        trans.Complete();

                    }
                }
                finally
                {
                    trans.Dispose();
                }
            }
            if (userInfo.Id > 0)
            {
                response.Body = userInfo;
                response.ResponseCode = 1;
                response.Message = "regist success";
            }
            else
            {
                response.Body = userInfo;
                response.ResponseCode = -1;
                response.Message = " Insert failed";
            }
            return response;
        }

        public ReturnResponse<Pager<UserInfoViewModels>> Query(UserInfoQueryParameter parameter)
        {
            var userInfoAccount = _userInfoAccountDal.Query(parameter);
            var list = new List<UserInfoViewModels>();
            var pagers = new Pager<UserInfoViewModels>();
            if (userInfoAccount.Items.Any())
            {
                userInfoAccount.Items.Each(account =>
                {
                    var userInfoRes = _userInfoDal.Query(new UserInfoQueryParameter() { ID = account.UserInfoID });
                    var userInfo = userInfoRes.Items.FirstOrDefault();
                    if (userInfo != null)
                        list.Add(new UserInfoViewModels()
                        {
                            Account = account.Account,
                            Password = userInfo.Password,
                            CompanyName = userInfo.CompanyName,
                            Address = userInfo.Address,
                            Gender = userInfo.Gender,
                            NickName = userInfo.NickName,
                            Id = userInfo.Id
                        });
                    pagers.Total = userInfoRes.Total;
                    pagers.Pages = userInfoRes.Pages;
                });
                pagers.Items = list.ToArray();
                return new ReturnResponse<Pager<UserInfoViewModels>>
                {
                    Body = pagers,
                    ResponseCode = 1,
                    Message = "Success"
                };
            }
            return new ReturnResponse<Pager<UserInfoViewModels>>
            {
                Body = pagers,
                ResponseCode = 1,
                Message = "Success"
            };
        }
    }
}
