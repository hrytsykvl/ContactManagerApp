using MediatR;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Application.Contacts.Commands.UploadCsv
{
    public class UploadCsvCommand : IRequest
    {
        public IFormFile File { get; set; }
    }
}
