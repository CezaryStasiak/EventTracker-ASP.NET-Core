using System.IO;
using EventTracker.UserData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            Configuration = builder.Build();
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // database as service, direct injection of connection string
            services.Add(new ServiceDescriptor(typeof(IDbConnection), new SqlDbConnection(Configuration.GetSection("ConnectionStrings")["testDb"])));

            // adding session for users
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.Name = "EventTracker";
            });
        }
        
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
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Events}/{action=Index}");

                routes.MapRoute(
                    name: "events",
                    template: "{controller=Events}/{action=Index}");

                routes.MapRoute(
                    name: "getEvents",
                    template: "Events/Index/{year}-{month}-{day}");
            });
        }
    }
}
