using CargoApi.Services.Abstract;
using CargoApi.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtension(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
