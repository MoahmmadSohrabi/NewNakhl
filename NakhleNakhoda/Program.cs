using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Data.Core.Mvc;
using NakhleNakhoda.Data.Extensions;
using NakhleNakhoda.Data.Models;
using NakhleNakhoda.Data.Models.Rep;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Factories;
using NakhleNakhoda.Services.Catalog;
using NakhleNakhoda.Services.Common;
using NakhleNakhoda.Services.Convertors;
using NakhleNakhoda.Services.Identity;
using NakhleNakhoda.Services.Infrastructure;
using NakhleNakhoda.Services.Install;
using NakhleNakhoda.Services.Logging;
using NakhleNakhoda.Services.Media;
using NakhleNakhoda.Services.Pay;
using NakhleNakhoda.Services.Security;
using NakhleNakhoda.Services.Services;
using NakhleNakhoda.Services.Services.Interfaces;
using NakhleNakhoda.Web.Areas.Admin.Factories;
using NakhleNakhoda.Web.Core.Extensions;
using System.Security.Claims;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Default SignIn settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<Member, Role>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});
builder.Services.ConfigureApplicationCookie(options =>
{
    //    //options.Cookie.SameSite = SameSiteMode.None;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = $"/Account/Login";
    options.LogoutPath = $"/Account/Logout";
    options.AccessDeniedPath = $"/Home/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IPrincipal>(provider =>
    provider.GetRequiredService<IHttpContextAccessor>().HttpContext?.User ?? ClaimsPrincipal.Current!);

builder.Services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();

builder.Services.AddTransient<Home_Rep>();
builder.Services.AddTransient<Page_Rep>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
//builder.Services.AddTransient<IEmailSender, SendEmail>();

builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ICustomFileProvider, CustomFileProvider>();
builder.Services.AddScoped<IPictureBinaryService, PictureBinaryService>();
builder.Services.AddScoped<IEmailSender, SendEmail>();


builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IPictureService, PictureService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddSingleton<ISmsService, SmsService>();
builder.Services.AddScoped<IBaseAdminModelFactory, BaseAdminModelFactory>();
builder.Services.AddScoped<IBaseModelFactory, BaseModelFactory>();
builder.Services.AddScoped<IAppInitializationService, AppInitializationService>();

// Add custome Claim
builder.Services.AddScoped<IUserClaimsPrincipalFactory<Member>, ApplicationClaimsPrincipalFactory>();
builder.Services.AddScoped<UserClaimsPrincipalFactory<Member, Role>, ApplicationClaimsPrincipalFactory>();

//// security dynamic permissions
builder.Services.AddScoped<ISecurityTrimmingService, SecurityTrimmingService>();

//// Catalogs
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomFacilityService, RoomFacilityService>();
builder.Services.AddScoped<IUserBookService, UserBookService>();
builder.Services.AddScoped<IBookRoomCategoryService, BookRoomCategoryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddSingleton<IPayService, PayService>();

builder.Services.AddDynamicPermissions();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    //app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpContext();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
