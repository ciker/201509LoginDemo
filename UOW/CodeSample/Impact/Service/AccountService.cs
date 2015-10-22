using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Service
{
    public interface IAccountService
    {
        List<Account> GetAll();
        bool Update(Account account);
        bool Delete(int id);
        bool Add(Account account);

    }
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        public AccountService(IRepository<Account> accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetQuery().Where(a => a.IsActive).ToList();
        }


        public bool Update(Account account)
        {
            var accountToUpdate = _accountRepository.GetById(account.Id);
            accountToUpdate.Name = account.Name;
            accountToUpdate.Email = account.Email;
            accountToUpdate.Url = account.Url;
            accountToUpdate.Phone = account.Phone;
            accountToUpdate.AccountType = account.AccountType;
            accountToUpdate.AccountStatus = account.AccountStatus;

            _accountRepository.Update(accountToUpdate);
            _accountRepository.Save();
            return true;
        }

        public bool Delete(int id)
        {
            //soft delete
            var account = _accountRepository.GetById(id);
            account.IsActive = false;
            _accountRepository.Update(account);
            _accountRepository.Save();


            return true;
        }

        public bool Add(Account account)
        {
            _accountRepository.Insert(account);
            _accountRepository.Save();
            return true;
        }
    }
}
