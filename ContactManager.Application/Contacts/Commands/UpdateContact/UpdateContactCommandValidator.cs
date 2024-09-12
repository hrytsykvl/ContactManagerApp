using FluentValidation;

namespace ContactManager.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.BirthDate)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Length(10);

            RuleFor(x => x.Salary)
                .GreaterThan(0);
        }
    }
}
