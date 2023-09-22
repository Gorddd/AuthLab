using AuthLab.Identity;
using AuthLab.Identity.Application;
using AuthLab.Identity.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

const string assemblyName = "AuthLab.Identity";

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.ApplyAppSettings();

if (appSettings.DatabaseSettings.SeedData)
    SeedData.EnsureSeedData(appSettings.DatabaseSettings.ConnectionString);

builder.Services.AddDbContext<AuthDbContext>(options => 
    options.UseSqlite(appSettings.DatabaseSettings.ConnectionString, 
        b => b.MigrationsAssembly(assemblyName)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options => options.ConfigureDbContext = 
        b => b.UseSqlite(appSettings.DatabaseSettings.ConnectionString,
        opt => opt.MigrationsAssembly(assemblyName)))
    .AddOperationalStore(options => options.ConfigureDbContext = 
        b => b.UseSqlite(appSettings.DatabaseSettings.ConnectionString,
        opt => opt.MigrationsAssembly(assemblyName)))
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapDefaultControllerRoute();

app.MapGet("/", () => "Hello World!");

app.Run();
