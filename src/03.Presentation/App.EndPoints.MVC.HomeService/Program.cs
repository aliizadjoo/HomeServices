using App.Domain.AppServices.AccountAgg;
using App.Domain.AppServices.AdminAgg;
using App.Domain.AppServices.CategoryAgg;
using App.Domain.AppServices.CityAgg;
using App.Domain.AppServices.CustomerAgg;
using App.Domain.AppServices.ExpertAgg;
using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.AppServices.OrderAgg;
using App.Domain.AppServices.ProposalAgg;
using App.Domain.AppServices.ReviewAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.AccountAgg.Services;
using App.Domain.Core.Contract.AdminAgg.AppService;
using App.Domain.Core.Contract.AdminAgg.Repository;
using App.Domain.Core.Contract.AdminAgg.Service;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CityAgg.Service;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Contract.ExpertAgg.Repository;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Contract.HomeServiceAgg.Service;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Contract.ReviewAgg.AppService;
using App.Domain.Core.Contract.ReviewAgg.Repository;
using App.Domain.Core.Contract.ReviewAgg.Service;
using App.Domain.Core.Entities;
using App.Domain.Services.AccountAgg;
using App.Domain.Services.AdminAgg;
using App.Domain.Services.CityAgg;
using App.Domain.Services.CustomerAgg;
using App.Domain.Services.ExpertAgg;
using App.Domain.Services.HomeserviceServiceAgg;
using App.Domain.Services.OrderAgg;
using App.Domain.Services.ProposalAgg;
using App.Domain.Services.ReviewAgg;
using App.EndPoints.MVC.HomeService.Middlwares;
using App.Framework;
using App.Infra.Cache.Contracts;
using App.Infra.Cache.InMemoryCache;
using App.Infra.Data.Repos.Ef.AdminAgg;
using App.Infra.Data.Repos.Ef.CategoryAgg;
using App.Infra.Data.Repos.Ef.CityAgg;
using App.Infra.Data.Repos.Ef.CustomerAgg;
using App.Infra.Data.Repos.Ef.ExpertAgg;
using App.Infra.Data.Repos.Ef.HomeServiceAgg;
using App.Infra.Data.Repos.Ef.OderAgg;
using App.Infra.Data.Repos.Ef.ProposalAgg;
using App.Infra.Data.Repos.Ef.ReviewAgg;
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


#region DI Container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HomeService;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();

builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminAppService, AdminAppService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IHomeserviceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IHomeserviceService, HomeserviceService>();
builder.Services.AddScoped<IHomeserviceAppService, HomeserviceAppService>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityAppService, CityAppService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderAppService, OrderAppService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewAppService, ReviewAppService>();

builder.Services.AddScoped<IAccountAppService, AccountAppService>();


builder.Services.AddScoped<ICacheService, InMemoryCacheService>();

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

builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomUserClaimsPrincipalFactory>();


builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IProposalService, ProposalService>();
builder.Services.AddScoped<IProposalAppService, ProposalAppService>();
builder.Services.AddScoped<IAccountService, AccountService>();


#endregion


var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();

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
