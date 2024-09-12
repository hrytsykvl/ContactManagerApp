using MediatR;

namespace ContactManager.Application.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}
