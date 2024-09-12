using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Repositories
{
    public class ContactsRepository(ContactsDbContext dbContext) : IContactsRepository
    {
        public async Task AddContacts(IEnumerable<Contact> contacts)
        {
            await dbContext.Contacts.AddRangeAsync(contacts);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var contacts = await dbContext.Contacts.ToListAsync();
            return contacts;
        }
    }
}
