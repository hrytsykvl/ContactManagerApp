using ContactManager.Application.Csv;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
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

                try
                {
                    csvReader.Context.RegisterClassMap<ContactMap>();
                    contacts = csvReader.GetRecords<Contact>().ToList();
                }
                catch (HeaderValidationException)
                {
                    throw new Exception("CSV file has invalid headers. Please ensure the CSV contains the correct fields.");
                }
                catch (TypeConverterException)
                {
                    throw new Exception("CSV file contains invalid data types. Please ensure the data is properly formatted.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while processing the file: {ex.Message}");
                }

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
