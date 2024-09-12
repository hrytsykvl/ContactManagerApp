using ContactManager.Application.Contacts.Commands.DeleteContact;
using ContactManager.Application.Contacts.Commands.UpdateContact;
using ContactManager.Application.Contacts.Commands.UploadCsv;
using ContactManager.Application.Contacts.Queries.GetAllContacts;
using ContactManager.Application.Contacts.Queries.GetContactById;
using ContactManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            try
            {
                var command = new UploadCsvCommand { File = file };
                await mediator.Send(command);
                return Ok("CSV file processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await mediator.Send(new GetAllContactsQuery());
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById([FromRoute]int id)
        {
            var contact = await mediator.Send(new GetContactByIdQuery(id));
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute]int id)
        {
            var isDeleted = await mediator.Send(new DeleteContactCommand(id));

            if(isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute]int id, UpdateContactCommand command)
        {
            command.Id = id;
            var isUpdated = await mediator.Send(command);

            if (isUpdated)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
