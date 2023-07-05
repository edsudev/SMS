using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Configuration;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();



builder.Services.AddControllersWithViews()
          .AddNewtonsoftJson();
builder.Services.AddMvc()
          .AddRazorRuntimeCompilation();
//builder.Services.AddMvc()
//            .AddCharts();

services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
       options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
   });

services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IHostEnvironment hostingEnvironment)
    {

        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
       .SetBasePath(hostingEnvironment.ContentRootPath)
       .AddEnvironmentVariables();

        IConfiguration configuration = configurationBuilder.Build();

        // Add the configuration to the DI container
        services.AddSingleton(configuration);
    }
}
