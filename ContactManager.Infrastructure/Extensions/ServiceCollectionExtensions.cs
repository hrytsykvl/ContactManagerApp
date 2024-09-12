using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Persistence;
using ContactManager.Infrastructure.Repositories;
using ContactManager.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ContactsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IContactSeeder, ContactSeeder>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
        }
    }
}
