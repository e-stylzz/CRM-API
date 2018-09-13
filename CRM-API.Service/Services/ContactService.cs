using System;
using System.Collections.Generic;
using CRMAPI.Domain.Models;
using CRMAPI.Infrastructure.Repository;

namespace CRMAPI.Service
{

    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(Guid id);
    }

    public class ContactService : IContactService
    {
        private IRepository<Contact> contactRepository;
        public ContactService(IRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return contactRepository.GetAll();
        }

        public Contact GetContact(Guid id)
        {
            return contactRepository.Get(id);
        }
    }
}
