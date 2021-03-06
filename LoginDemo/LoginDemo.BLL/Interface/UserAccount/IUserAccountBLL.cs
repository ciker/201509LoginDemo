﻿using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount.QueryParameter;
using LoginDemo.ViewModels.UserInfo;

// ReSharper disable once CheckNamespace
namespace LoginDemo.BLL.Interface
{
    // ReSharper disable once InconsistentNaming
    public interface IUserAccountBLL : IDependency
    {
        ReturnResponse<UserInfoViewModels> Login(UserInfoViewModels user);

        ReturnResponse<UserInfoViewModels> Register(UserInfoViewModels user);

        ReturnResponse<Pager<UserInfoViewModels>> Query(UserInfoQueryParameter parameter);

    }
}
