using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.Services.Implementation;
using Cargo.Core.Factories;
using Cargo.Core.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Cargo.AdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();                    

            var configuration = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                               .Build();

            string dbNameValue = configuration.GetSection("VendorTypes").GetSection("VendorType").Value;

            string connectionString = configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;

            services.AddTransient(serviceProvider =>
            {
                return DbFactory.Create(Enum.Parse<VendorTypes>(dbNameValue), connectionString);
            });

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IShopService, ShopService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
