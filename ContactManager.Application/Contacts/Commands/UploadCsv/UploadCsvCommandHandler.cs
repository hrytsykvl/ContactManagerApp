using ContactManager.Application.Csv;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ContactManager.Application.Contacts.Commands.UploadCsv
{
    public class UploadCsvCommandHandler(IContactsRepository contactsRepository) : IRequestHandler<UploadCsvCommand>
    {
        public async Task Handle(UploadCsvCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
            {
                throw new Exception("No file uploaded.");
            }

            using (var streamReader = new StreamReader(request.File.OpenReadStream()))
            using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var contacts = new List<Contact>();
                var validationErrors = new List<string>();

                
                csvReader.Context.RegisterClassMap<ContactMap>();
                contacts = csvReader.GetRecords<Contact>().ToList();

                foreach (var contact in contacts)
                {
                    var validationContext = new ValidationContext(contact);
                    var results = new List<ValidationResult>();

                    if (!Validator.TryValidateObject(contact, validationContext, results, true))
                    {
                        foreach (var error in results)
                        {
                            validationErrors.Add(error.ErrorMessage);
                        }
                    }
                }

                if (validationErrors.Any())
                {
                    throw new Exception($"Validation errors occurred: {string.Join(", ", validationErrors)}");
                }

                await contactsRepository.AddContacts(contacts);
            }
        }
    }
}
