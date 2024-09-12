using ContactManager.Domain.Repositories;
using MediatR;

namespace ContactManager.Application.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler(IContactsRepository contactsRepository) : IRequestHandler<DeleteContactCommand, bool>
    {
        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await contactsRepository.GetContactById(request.Id);
            if (contact == null)
            {
                return false;
            }

            await contactsRepository.DeleteContact(contact);
            return true;
        }
    }
}
