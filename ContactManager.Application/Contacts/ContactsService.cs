using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;

namespace ContactManager.Application.Contacts
{
    public class ContactsService(IContactsRepository contactsRepository) : IContactsService
    {
        public async Task AddContacts(IEnumerable<Contact> contacts)
        {
            await contactsRepository.AddContacts(contacts);
        }
    }
}
