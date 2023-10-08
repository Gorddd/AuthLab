using AuthLab.Host.Data;
using AuthLab.Host.Application;
using AuthLab.DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AuthLab.Host.Authentication;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.SetAppSettings();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services
    .AddMapper()
    .AddDomain()
    .AddSecurity()
    .AddDataAccess();

builder.Services.AddDbContext<UsersDbContext>(opt => 
    opt.UseSqlite(appSettings.DatabaseSettings.ConnectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Services.ApplyDbMigration(appSettings);

app.Run();
