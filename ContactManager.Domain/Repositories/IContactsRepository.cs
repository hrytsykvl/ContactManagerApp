using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Repositories
{
    public interface IContactsRepository
    {
        Task AddContacts(IEnumerable<Contact> contacts);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> GetContactById(int id);
        Task DeleteContact(Contact contact);
        Task SaveChanges();
    }
}
