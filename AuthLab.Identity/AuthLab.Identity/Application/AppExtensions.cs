using AuthLab.Identity.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AuthLab.Identity.Application;

public static class AppExtensions
{
    public static void DoMigrationIfApplied(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var appSettings = services.GetRequiredService<IConfiguration>().Get<AppSettings>()!;
        if (appSettings.DatabaseSettings.ApplyMigration)
        {
            var context = services.GetRequiredService<AuthDbContext>();
            context.Database.Migrate();
        }
    }
}
