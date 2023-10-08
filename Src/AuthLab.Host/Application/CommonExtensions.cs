using AuthLab.DataAccess.EfCore;
using AuthLab.DataAccess.Mapping;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace AuthLab.Host.Application;

public static class CommonExtensions
{
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

    public static void ApplyDbEncryptionOnShutdown(this IServiceProvider serviceProvider, DbAuthorization dbAuth, WebApplication appppp)
    {
        IHostApplicationLifetime applicationLifeTime;
        using (var scope = serviceProvider.CreateScope())
        {
            applicationLifeTime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        }
        applicationLifeTime.ApplicationStopped.Register(() =>
        {
            Console.WriteLine("Производится шифрование базы данных.");

            SqliteConnection.ClearAllPools();

            dbAuth.DbLogout();

            Console.WriteLine("База данных зашифрована.");
        });
    }

    public static string GetDbPathFromSqliteConnectionString(this AppSettings appSettings)
    {
        var connectionString = appSettings.DatabaseSettings.ConnectionString;

        return connectionString.Substring(connectionString.IndexOf("=") + 1);
    }
}
