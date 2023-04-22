using Microsoft.EntityFrameworkCore;
using practice.BLL.Interfaces;
using practice.BLL.Services;
using practice.DAL;
using practice.DAL.Infrastructure;
using practice.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Reflection.PortableExecutable;
using practice.Middleware;

namespace practice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Database");

            services.AddDbContext<TrackLocationContext>(options =>
                options.UseSqlServer(connectionString, providerOptions => 
                    providerOptions.EnableRetryOnFailure()
                    ));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITrackLocationService, TrackLocationService>();
            services.AddScoped<IViberBotService, ViberBotService>();

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionMiddleware();
            app.ConfigureViberWebhookMiddleware();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
