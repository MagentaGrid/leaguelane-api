using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> GetContactDetails(CancellationToken cancellationToken)
        {
            return await _contactRepository.GetContactDetails(cancellationToken);
        }
        public async Task<Contact> UpdateContactDetails(Contact contact, CancellationToken cancellationToken)
        {
            return await _contactRepository.UpdateContactDetails(contact, cancellationToken);
        }

        public async Task<Contact> CreateContactDetails(Contact contact, CancellationToken cancellationToken)
        {
            return await _contactRepository.CreateContactDetails(contact, cancellationToken);
        }
    }
}
