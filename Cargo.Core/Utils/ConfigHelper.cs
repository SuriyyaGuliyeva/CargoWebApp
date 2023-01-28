using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.DataAccessLayer.Implementation.SqlServer;
using Cargo.Core.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.Core.Utils
{
    public static class ConfigHelper
    {
        public static void DatabaseConfig(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                .Build();

            string dbNameValue = configuration.GetSection("DbVendorNames").GetSection("DbName").Value;

            if (dbNameValue.Equals(DbName.SqlServer))
            {
                services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            }           
        }
    }
}
