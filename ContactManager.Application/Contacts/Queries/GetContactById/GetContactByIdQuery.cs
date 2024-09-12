using ContactManager.Domain.Entities;
using MediatR;

namespace ContactManager.Application.Contacts.Queries.GetContactById
{
    public class GetContactByIdQuery : IRequest<Contact?>
    {
        public GetContactByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
