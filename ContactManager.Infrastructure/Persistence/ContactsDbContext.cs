using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Persistence
{
    public class ContactsDbContext(DbContextOptions<ContactsDbContext> options) : DbContext(options)
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
