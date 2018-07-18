using System.IO;
using EventTracker.UserData;
using EventTracker.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // json with string
                .AddJsonFile("appsettings.json", false, true);
            Configuration = builder.Build();
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // database as service
            services.Add(new ServiceDescriptor(typeof(IDbConnection), new DbConnection()));
            // connection string
            services.Configure<AppSettings>(Configuration.GetSection("ConnectionStrings"));
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Events}/{action=Index}/{id?}");
            });
        }
    }
}
