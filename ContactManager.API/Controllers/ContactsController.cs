using ContactManager.Application.Contacts;
using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Csv;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactsService contactsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var streamReader = new StreamReader(file.OpenReadStream()))
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
                    return BadRequest("CSV file has invalid headers. Please ensure the CSV contains the correct fields.");
                }
                catch (TypeConverterException)
                {
                    return BadRequest("CSV file contains invalid data types. Please ensure the data is properly formatted.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while processing the file: {ex.Message}");
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
                    return BadRequest(new { Errors = validationErrors });
                }

                await contactsService.AddContacts(contacts);

                return Ok("File uploaded and data saved successfully.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await contactsService.GetContacts();
            return Ok(contacts);
        }

    }
}
