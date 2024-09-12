using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using MediatR;

namespace ContactManager.Application.Contacts.Queries.GetContactById
{
    public class GetContactByIdQueryHandler(IContactsRepository contactsRepository) : IRequestHandler<GetContactByIdQuery, Contact>
    {
        public async Task<Contact> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await contactsRepository.GetContactById(request.Id);
            return contact;
        }
    }
}
