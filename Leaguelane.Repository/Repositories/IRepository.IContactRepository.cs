using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetContactDetails(CancellationToken cancellationToken);
        Task<Contact> UpdateContactDetails(Contact contact, CancellationToken cancellationToken);
        Task<Contact> CreateContactDetails(Contact contact, CancellationToken cancellationToken);
    }
}
