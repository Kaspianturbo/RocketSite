using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Repositories;
using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RocketSiteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddTransient<ICRUDRepository<Rocket>, RocketRepository>(provider => new RocketRepository(connectionString));
            services.AddTransient<ICRUDRepository<Location>, LocationRepository>(provider => new LocationRepository(connectionString));
            services.AddTransient<ICRUDRepository<Cargo>, CargoRepository>(provider => new CargoRepository(connectionString));
            services.AddTransient<ICRUDRepository<Customer>, CustomerRepository>(provider => new CustomerRepository(connectionString));
            services.AddTransient<ICRUDRepository<Employee>, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
            services.AddTransient<ICRUDRepository<TrainingProgram>, TrainingProgramRepository>(provider => new TrainingProgramRepository(connectionString));
            services.AddTransient<ICRUDRepository<Resources>, ResourcesRepository>(provider => new ResourcesRepository(connectionString));
            services.AddTransient<ICRUDRepository<Equipment>, EquipmentRepository>(provider => new EquipmentRepository(connectionString));
            services.AddTransient<ICRUDRepository<Cosmodrome>, CosmodromeRepository>(provider => new CosmodromeRepository(connectionString));
            services.AddTransient<ICRUDRepository<Purchase>, PurchaseRepository>(provider => new PurchaseRepository(connectionString));
            services.AddTransient<ICRUDRepository<SpaceMission>, SpaceMissionRepository>(provider => new SpaceMissionRepository(connectionString));
            services.AddTransient<IRocketSiteRepository, RocketSiteRepository>(provider => new RocketSiteRepository(connectionString));
            
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Rocket}/{action=Index}/{name?}");
            });
        }
    }
}
