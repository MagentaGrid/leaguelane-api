using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly LeaguelaneDbContext _context;

        public ContactRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> GetContactDetails(CancellationToken cancellationToken)
        {
            return await _context.Contacts.Where(x => x.Active == true).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Contact> UpdateContactDetails(Contact contact, CancellationToken cancellationToken)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return contact;
        }

        public async Task<Contact> CreateContactDetails(Contact contact, CancellationToken cancellationToken)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return contact;
        }
    }
}
