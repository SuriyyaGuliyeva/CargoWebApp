using Cargo.AdminPanel.Identity;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Mappers.Implementation;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.Services.Implementation;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.AdminPanel.Infrastructure
{
    public static class InjectConfig
    {
        public static void ConfigServices(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<ICountryMapper, CountryMapper>();

            services.AddTransient<IShopMapper, ShopMapper>();
            services.AddTransient<IAddShopMapper, AddShopMapper>();
            services.AddTransient<IUpdateShopMapper, UpdateShopMapper>();

            services.AddTransient<ICategoryMapper, CategoryMapper>();

            services.AddTransient<IUserMapper, UserMapper>();

            services.AddTransient<IRoleMapper, RoleMapper>();        

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();
        }
    }
}
