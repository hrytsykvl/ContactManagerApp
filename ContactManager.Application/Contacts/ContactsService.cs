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

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contacts = await contactsRepository.GetAllContacts();
            return contacts;
        }
    }
}
