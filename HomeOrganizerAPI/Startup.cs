using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeOrganizerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AutoMapperConfig.RegisterMappings();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost", "http://webapp.zapto.org")
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
            });
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddDbContext<HomeOrganizerContext>(options => options.UseMySQL(Configuration.GetConnectionString("Database")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(

                );
            });
        }
    }
}
