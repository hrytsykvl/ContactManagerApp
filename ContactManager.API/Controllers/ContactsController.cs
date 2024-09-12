using ContactManager.Application.Contacts;
using ContactManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return BadRequest("No file uploaded.");

            using var reader = new StreamReader(file.OpenReadStream());
            var contacts = new List<Contact>();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                contacts.Add(new Contact
                {
                    Name = values[0],
                    BirthDate = DateOnly.Parse(values[1]),
                    Married = bool.Parse(values[2]),
                    Phone = values[3],
                    Salary = decimal.Parse(values[4])
                });
            }

            await contactsService.AddContacts(contacts);

            return Ok();
        }

    }
}
