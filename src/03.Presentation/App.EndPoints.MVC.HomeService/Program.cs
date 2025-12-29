using App.Domain.AppServices.AccountAgg;
using App.Domain.AppServices.CityAgg;
using App.Domain.AppServices.CustomerAgg;
using App.Domain.AppServices.ExpertAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CityAgg.Service;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Contract.ExpertAgg.Repositorty;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Entities;
using App.Domain.Services.CityAgg;
using App.Domain.Services.CustomerAgg;
using App.Domain.Services.ExpertAgg;
using App.EndPoints.MVC.HomeService.Middlwares;
using App.Framework;
using App.Infra.Data.Repos.Ef.CategoryAgg;
using App.Infra.Data.Repos.Ef.CityAgg;
using App.Infra.Data.Repos.Ef.CustomerAgg;
using App.Infra.Data.Repos.Ef.ExpertAgg;
using App.Infra.Data.Repos.Ef.HomeServiceAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, configuration) =>
{

    configuration.ReadFrom.Configuration(context.Configuration);
});




// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HomeService;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityAppService, CityAppService>();

builder.Services.AddScoped<IExpertRepositoy, ExpertRepositoy>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();


builder.Services.AddScoped<IAccountAppService, AccountAppService>();

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedAccount = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.User.AllowedUserNameCharacters = null;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<PersianIdentityErrorDescriber>();

var app = builder.Build();

//app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
