using MediatR;

namespace ContactManager.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
