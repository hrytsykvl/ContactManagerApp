using ContactManager.Domain.Entities;
using MediatR;

namespace ContactManager.Application.Contacts.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<IEnumerable<Contact>>
    {
    }
}
