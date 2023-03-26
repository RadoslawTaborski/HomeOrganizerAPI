using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Pomelo.EntityFrameworkCore.MySql;
using System.Security.Claims;
using HomeOrganizerAPI.Repositories;

namespace HomeOrganizerAPI;

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
        var config = Configuration.GetSection("Config").Get<Config>();

        services.AddApiVersioning();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = config.Authority;
            o.Audience = "homeorganizerapi";
            o.RequireHttpsMetadata = false;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "ho.read"));
            options.AddPolicy("Consumer", policy => policy.RequireClaim(ClaimTypes.Role, "consumer"));
            options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
        });
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
        services.AddDbContext<HomeOrganizerContext>(options =>
        {
            var connectionString = Configuration.GetConnectionString("Database");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        services.AddTransient<CategoryRepository>();
        services.AddTransient<ExpensesSettingsRepository>();
        services.AddTransient<ExpensesRepository>();
        services.AddTransient<ExpenseDetailsRepository>();
        services.AddTransient<GroupRepository>();
        services.AddTransient<ItemRepository>();
        services.AddTransient<PermanentItemsRepository>();
        services.AddTransient<SaldoRepository>();
        services.AddTransient<ShoppingItemsRepository>();
        services.AddTransient<ListCategoryRepository>();
        services.AddTransient<ShoppingListsRepository>();
        services.AddTransient<StatesRepository>();
        services.AddTransient<SubcategoryRepository>();
        services.AddTransient<TemporaryItemsRepository>();
        services.AddTransient<UsersRepository>();
        services.AddTransient<IPermissionChecker, PermissionChecker>();
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
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
