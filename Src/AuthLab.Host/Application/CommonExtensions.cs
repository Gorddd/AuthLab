using AuthLab.DataAccess.EfCore;
using AuthLab.DataAccess.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthLab.Host.Application;

public static class CommonExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var servicesAssembly = Assembly.Load(typeof(UsersProfile).Assembly.GetName());
        services.AddAutoMapper(servicesAssembly);

        return services;
    }

    public static AppSettings SetAppSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration);
        return builder.Configuration.Get<AppSettings>()!;
    }

    public static void ApplyDbMigration(this IServiceProvider serviceProvider, AppSettings appSettings)
    {
        if (appSettings.DatabaseSettings.ApplyMigration)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            context.Database.Migrate();
        }
    }
}
