using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Persistence;

namespace ContactManager.Infrastructure.Seeders
{
    public class ContactSeeder(ContactsDbContext dbContext) : IContactSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Contacts.Any())
                {
                    var contacts = GetContacts();
                    await dbContext.Contacts.AddRangeAsync(contacts);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts = [
                new() { Name = "John Doe", BirthDate = new DateOnly(1980, 1, 1), Married = true, Phone = "1234567890", Salary = 1000 },
                new() { Name = "Jane Smith", BirthDate = new DateOnly(1985, 2, 2), Married = false, Phone = "0987654321", Salary = 2000 },
                new() { Name = "Alice Johnson", BirthDate = new DateOnly(1990, 3, 3), Married = true, Phone = "1230984567", Salary = 3000 },
                new() { Name = "Bob Brown", BirthDate = new DateOnly(1995, 4, 4), Married = false, Phone = "0987612345", Salary = 2500 },
                new() { Name = "Charlie White", BirthDate = new DateOnly(2000, 5, 5), Married = true, Phone = "1234789650", Salary = 1500 }
                ];
            return contacts;
        }
    }
}
