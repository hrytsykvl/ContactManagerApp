using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Repositories
{
    public interface IContactsRepository
    {
        Task AddContacts(IEnumerable<Contact> contacts);
    }
}
