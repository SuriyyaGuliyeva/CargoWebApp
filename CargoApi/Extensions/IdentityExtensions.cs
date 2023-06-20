using Cargo.Core.Constants;
using Cargo.Core.Domain.Entities;
using Cargo.Core.Identity;
using CargoApi.Services.Abstract;
using CargoApi.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CargoApi.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddIdentityExtension(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.AddIdentity<User, Role>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKeyConstant.Key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
