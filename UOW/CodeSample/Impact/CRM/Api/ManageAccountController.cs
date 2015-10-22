using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.ViewModels;
using Domain;
using Service;

namespace CRM.Api
{
    public class ManageAccountController : ApiController
    {
        private IAccountService _accountService;
        private IContactService _contactService;

        public ManageAccountController(IAccountService accountService, IContactService contactService)
        {
            this._accountService = accountService;
            this._contactService = contactService;
        }
        [AcceptVerbs("POST")]
        public bool EditAccount(AccountViewModel viewModel)
        {
            var account = new Account();
            account.Name = viewModel.Name;
            account.Email = viewModel.Email;
            account.Phone = viewModel.Phone;
            account.Id = viewModel.Id;
            account.Url = viewModel.Url;
            account.AccountType = (AccountTypeEnum)viewModel.AccountType;
            account.AccountStatus = (AccountStatusEnum)viewModel.AccountStatus;

            return _accountService.Update(account);
        }
        [AcceptVerbs("POST")]
        public bool DeleteAccount(AccountViewModel viewModel)
        {
            return _accountService.Delete(viewModel.Id);
        }
        [AcceptVerbs("POST")]
        public bool AddAccount(AccountViewModel viewModel)
        {
            var account = new Account();
            account.Name = viewModel.Name;
            account.Email = viewModel.Email;
            account.Url = viewModel.Url;
            account.Phone = viewModel.Phone;
            account.AccountType = (AccountTypeEnum)viewModel.AccountType;
            account.AccountStatus = (AccountStatusEnum)viewModel.AccountStatus;

            return _accountService.Add(account);
        }

    }
}
