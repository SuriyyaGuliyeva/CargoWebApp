using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Mappers.Implementation;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.AdminPanel.Infrastructure
{
    public static class InjectConfig
    {
        public static void ServiceConfig(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<ICountryMapper, CountryMapper>();
            services.AddTransient<IShopMapper, ShopMapper>();
            services.AddTransient<ICategoryMapper, CategoryMapper>();
        }
    }
}
