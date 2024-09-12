using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using MediatR;

namespace ContactManager.Application.Contacts.Queries.GetAllContacts
{
    public class GetAllContactsQueryHandler(IContactsRepository contactsRepository) : IRequestHandler<GetAllContactsQuery, IEnumerable<Contact>>
    {
        public async Task<IEnumerable<Contact>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await contactsRepository.GetAllContacts();
            return contacts;
        }
    }
}
