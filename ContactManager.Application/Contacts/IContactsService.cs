using ContactManager.Domain.Entities;

namespace ContactManager.Application.Contacts
{
    public interface IContactsService
    {
        Task AddContacts(IEnumerable<Contact> contacts);
        Task<IEnumerable<Contact>> GetContacts();
    }
}