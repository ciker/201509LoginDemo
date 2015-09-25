using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.DataModel;
using Autofac.Repository;
using Autofac.UnitOfWork;
using Autofac.ViewModel;

namespace Autofac.CoreService.Impl
{
    public class UserManage : IUserManage
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private IUnitOfWork _unitOfWork;
        public UserManage(IUserRepository userRepository, IUserRoleRepository userRoleRepository,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public Sys_User LoadUser(int id)
        {
            return _userRepository.Entities().FirstOrDefault(s => s.Id == id);
        }

        public object Users()
        {
            return _userRepository.Entities().Take(10)
                .GroupJoin(_userRoleRepository.Entities(), a => a.Id, b => b.UserId, (a, b) =>
                    new
                    {
                        a.Id,
                        a.UserName,
                        a.UserTrueName,
                        a.Password,
                        a.CreateDate,
                        Roles = b.Select(m => new { RoleName = "RoleId_"+m.RoleId })
                    }).ToList();
        }

        /// <summary>
        /// 添加、修改 User
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveUser(UserModel userModel , out string message)
        {
            message = null;
           
            var userEntity = new Sys_User
            {
                UserName = userModel.UserName,
                Password = userModel.Password,
                UserTrueName = userModel.UserTrueName,
                CreateDate = DateTime.Now,
               
            };
            //添加用户
            if (userModel.Id < 1)
            {
                _userRepository.Insert(userEntity);
                _unitOfWork.Commit();
                if (userEntity.Id < 1)
                {
                    message = "添加用户失败";
                    return false;
                }
            }
             //修改操作
            else
            {
                //删除用户角色关系
                var userRoleIdArray = _userRoleRepository.Entities()
                    .Where(m => m.UserId == userModel.Id)
                    .Select(s => s.Id).ToList();
                foreach (var roleId in userRoleIdArray)
                {
                    _userRoleRepository.Delete(new Sys_User_Roles {Id = roleId});
                }
            }
            var userRoles = new List<Sys_User_Roles>();
            foreach (var roleId in userModel.Role)
            {
                userRoles.Add(new Sys_User_Roles { UserId = userModel.Id, RoleId = roleId });
            }
            //添加用户角色关系
            _userRoleRepository.Insert(userRoles);
            return  _unitOfWork.Commit() > 0;
        }
    }
}
