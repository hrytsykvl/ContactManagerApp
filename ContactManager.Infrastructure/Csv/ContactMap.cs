using ContactManager.Domain.Entities;
using CsvHelper.Configuration;

namespace ContactManager.Infrastructure.Csv
{
    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.BirthDate).Name("BirthDate").TypeConverterOption.Format("yyyy-MM-dd");
            Map(m => m.Married).Name("Married");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Salary).Name("Salary");
        }
    }
}
