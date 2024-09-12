using ContactManager.Application.Contacts;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IContactsService, ContactsService>();
        }
    }
}
