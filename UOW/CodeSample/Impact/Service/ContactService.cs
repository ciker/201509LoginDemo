using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure;


namespace Service
{
    public interface IContactService
    {
    }
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _contactRepository;
        public ContactService( IRepository<Contact> contactRepository)
        {
            this._contactRepository = contactRepository;
        }
    }
}
