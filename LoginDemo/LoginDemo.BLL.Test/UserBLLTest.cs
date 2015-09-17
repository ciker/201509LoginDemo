using System;
using LoginDemo.BLL.Interface;
using LoginDemo.Commom;
using LoginDemo.DAL;
using LoginDemo.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginDemo.BLL.Test
{
    [TestClass]
    public class UserBLLTest
    {
        public UserBLL UserBll = new UserBLL(new UserDAL());
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
            var response = UserBll.Register(user);
            Assert.AreEqual(true, response != null && response.IsSuccess);
        }

        [TestMethod]
        public void GetUserListbyParameterTestMethod()
        {
            var ret = UserBll.GetUserListbyParameter(new UserQueryParameter());
            Assert.AreEqual(true, ret.IsSuccess);
        }

        [TestMethod]
        public void LoginTestMethod()
        {

            var ret = UserBll.Login(new User()
            {
                UserName = "1_Unit_Test2",
                UserPWD = "1_Unit_Test2",
            });
            Assert.AreEqual(false, ret.IsSuccess);
        }
    }
}
