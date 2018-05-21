using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Database;
using ConcertVenueApp.Repositories.Events;
using ConcertVenueApp.Repositories.Tickets;
using ConcertVenueApp.Repositories.Users;
using ConcertVenueApp.Services.Events;
using ConcertVenueApp.Services.Tickets;
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
            services.AddScoped<IEventRepository, EventRepositoryMySQL>();
            services.AddScoped<ITicketRepository, TicketRepositoryMySQL>();
            services.AddScoped<IAuthenticationService, AuthenticationServiceMySQL>();
            services.AddScoped<IAdminService, AdminServiceMySQL>();
            services.AddScoped<IEventService, EventServiceMySQL>();
            services.AddScoped<ITicketService, TicketServiceMySQL>();
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
