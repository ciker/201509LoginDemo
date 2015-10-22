using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.UnitOfWork;

namespace UoW_MultipleDBContext.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<FirstDbContext> _unitOfWork;

        public UserService(IUnitOfWork<FirstDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Entity.User> GetAll()
        {
            var ds = _unitOfWork.UserRepository.GetAll();
            _unitOfWork.Commit();
            return ds;
        }

        public Entity.User Get(Expression<Func<Entity.User, bool>> where)
        {
            return _unitOfWork.UserRepository.Get(where);
        }

        public IEnumerable<Entity.User> GetMany(Expression<Func<Entity.User, bool>> where)
        {
            return _unitOfWork.UserRepository.GetMany(where);
        }

        public int Insert(Entity.User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            return _unitOfWork.Commit();
        }

        public int Update(Entity.User user)
        {
            _unitOfWork.UserRepository.Update(user);
            return _unitOfWork.Commit();
        }

        public int Delete(int Id)
        {
            _unitOfWork.UserRepository.Delete(Id);
            return _unitOfWork.Commit();
        }

        public int Delete(Entity.User user)
        {
            _unitOfWork.UserRepository.Delete(user);
            return _unitOfWork.Commit();
        }

        public int Delete(Expression<Func<Entity.User, bool>> where)
        {
            _unitOfWork.UserRepository.Delete(where);
            return _unitOfWork.Commit();
        }
    }
}
