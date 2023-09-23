using AuthLab.DataAccess.Implementations;
using AuthLab.DataAccess.Mapping;
using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Entities;
using AuthLab.Security.Implementations;
using System.Reflection;

namespace AuthLab.Host.Application;

public static class DiExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var servicesAssembly = Assembly.Load(typeof(UsersProfile).Assembly.GetName());
        services.AddAutoMapper(servicesAssembly);

        return services;
    }

    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddTransient<Admin>()
            .AddTransient<Defender>();

        return services;
    }

    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddTransient<IUsers, UsersRepository>();

        return services;
    }

    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddTransient<ISecurityService, SecurityService>()
            .AddTransient<IPasswordValidator, PasswordValidator>();

        return services;
    }
}
