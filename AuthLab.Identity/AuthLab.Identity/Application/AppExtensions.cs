using AuthLab.Identity.DataAccess;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthLab.Identity.Application;

public static class AppExtensions
{
    public static AppSettings ApplyAppSettings(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.Get<AppSettings>()!;
        builder.Services.AddSingleton(appSettings);
        return appSettings;
    }
}
