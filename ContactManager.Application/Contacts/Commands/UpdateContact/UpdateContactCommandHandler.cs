using ContactManager.Domain.Repositories;
using MediatR;

namespace ContactManager.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler(IContactsRepository contactsRepository) : IRequestHandler<UpdateContactCommand, bool>
    {
        public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await contactsRepository.GetContactById(request.Id);
            if (contact == null)
            {
                return false;
            }

            contact.Name = request.Name;
            contact.BirthDate = request.BirthDate;
            contact.Married = request.Married;
            contact.Phone = request.Phone;
            contact.Salary = request.Salary;

            await contactsRepository.SaveChanges();

            return true;
        }
    }
}
