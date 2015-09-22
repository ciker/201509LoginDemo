using System;
using LoginDemo.Commom;
using LoginDemo.DAL;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginDemo.BLL.Test
{
    [TestClass]
    public class UserBLLTest
    {
        private readonly UserBLL _userBll = new UserBLL(new UserDAL());
        private readonly UserAccountBLL _userAccountBll = new UserAccountBLL(new UserAccountDAL());
        #region UserTest
        [TestMethod]
        public void UserRegister()
        {
            var num = new Random().Next(3, 300);
            var user = new User()
            {
                UserName = "1_Unit_Test" + num,
                UserPWD = ("1_Unit_Test2" + num).ToBase64String(),
                Mobile = "13678900123",
                Email = "13678900123@qq.com"
            };
            var response = _userBll.Register(user);
            Assert.AreEqual(true, response != null && response.IsSuccess);
        }

        [TestMethod]
        public void GetUserListbyParameterTestMethod()
        {
            var ret = _userBll.GetUserListbyParameter(new UserQueryParameter());
            Assert.AreEqual(true, ret.IsSuccess);
        }

        [TestMethod]
        public void LoginTestMethod()
        {

            var ret = _userBll.Login(new User()
            {
                UserName = "1_Unit_Test2",
                UserPWD = "1_Unit_Test2",
            });
            Assert.AreEqual(false, ret.IsSuccess);
        }
        #endregion

        #region UserInfoTest
        [TestMethod]
        public void UserInfoRegister()
        {
            _userAccountBll.Register(new UserInfoAndAccount()
            {
                Account = "UserInfo_Account_Test1",
                Password = "UserInfo_Account_Test1",
                CompanyName = "company 1",
                Address = "address 1"
            });
        }

        [TestMethod]
        public void UserInfoLogin()
        {
            _userAccountBll.Login(new UserInfoAndAccount()
            {
                Account = "UserInfo_Account_Test1",
                Password = "UserInfo_Account_Test1"
            });
        }

        [TestMethod]
        public void UserInfoQuery()
        {
            _userAccountBll.Query(new UserInfoQueryParameter());
        }
        #endregion
    }
}
