using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Persistence;

namespace ContactManager.Infrastructure.Repositories
{
    public class ContactsRepository(ContactsDbContext dbContext) : IContactsRepository
    {
        public async Task AddContacts(IEnumerable<Contact> contacts)
        {
            await dbContext.Contacts.AddRangeAsync(contacts);
            await dbContext.SaveChangesAsync();
        }
    }
}
