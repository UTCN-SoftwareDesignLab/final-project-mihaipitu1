using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Database;
using ConcertVenueApp.Repositories.Users;
using ConcertVenueApp.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertVenueApp
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
            services.AddMvc();
            services.AddTransient<DBConnectionWrapper>(_ => new DBConnectionFactory().GetConnectionWrapper(false));

            services.AddScoped<IUserRepository, UserRepositoryMySQL>();
            //services.AddScoped<IBaseRepository<Patient>, PatientRepositoryMySQL>();
            //services.AddScoped<IConsultationRepository, ConsultationRepositoryMySQL>();
            services.AddScoped<IAuthenticationService, AuthenticationServiceMySQL>();
            services.AddScoped<IAdminService, AdminServiceMySQL>();
            //services.AddScoped<IPatientService, PatientServiceMySQL>();
            //services.AddScoped<IConsultationService, ConsultationServiceMySQL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
