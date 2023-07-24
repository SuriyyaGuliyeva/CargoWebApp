using CargoApi.Mappers.Abstract;
using CargoApi.Mappers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApi.Extensions
{
    public static class MapperExtension
    {
        public static void AddMapperExtension(this IServiceCollection services)
        {
            services.AddTransient<IOrderMapper, OrderMapper>();
            services.AddTransient<IUpdateOrderMapper, UpdateOrderMapper>();
        }
    }
}
