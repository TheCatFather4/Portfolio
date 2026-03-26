using Cafe.BLL;
using Cafe.Core.Interfaces.Application;
using Cafe.Core.Interfaces.Services;
using Cafe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Portfolio.Models.Airport;
using Airport.Core.Repositories;
using Academy.Core.Interfaces.Application;
using Academy.BLL;
using Academy.Core.Interfaces.Services;
using Portfolio.Configuration;
using Portfolio.Areas.Cafe.Utilities;
using Portfolio.Areas.Academy.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();

// 4th Wall Café Configuration
builder.Services.AddSingleton<ICafeConfiguration, CafeConfiguration>();
builder.Services.AddSingleton<ServiceFactory>(provider =>
{
    var appConfig = provider.GetRequiredService<ICafeConfiguration>();
    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
    var jwtConfig = provider.GetRequiredService<IConfiguration>();
    return new ServiceFactory(appConfig, loggerFactory, jwtConfig);
});

var appConfig = new CafeConfiguration(builder.Configuration);
var connectionString = appConfig.GetConnectionString();

// 4th Wall Academy Configuration
builder.Services.AddSingleton<IAcademyConfiguration, AcademyConfiguration>();
builder.Services.AddSingleton<AcademyServiceFactory>(provider =>
{
    var acaConfig = provider.GetRequiredService<IAcademyConfiguration>();
    return new AcademyServiceFactory(acaConfig);
});

// ASP.NET Core Identity
builder.Services.AddDbContext<IdentityCafeContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityCafeContext>()
.AddDefaultTokenProviders();

// 4th Wall Café Services
builder.Services.AddScoped<ICustomerService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateCustomerService();
});

builder.Services.AddScoped<IMenuManagerService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateMenuManagerService();
});

builder.Services.AddScoped<IMenuRetrievalService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateMenuRetrievalService();
});

builder.Services.AddScoped<IOrderService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateOrderService();
});

builder.Services.AddScoped<IPaymentService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreatePaymentService();
});

builder.Services.AddScoped<ISalesReportService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateSalesReportService();
});

builder.Services.AddScoped<IServerManagerService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateServerManagerService();
});

builder.Services.AddScoped<IShoppingBagService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateShoppingBagService();
});

builder.Services.AddScoped<IWebTokenService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateWebTokenService();
});

builder.Services.AddScoped<ICafeSelectList, CafeSelectList>();

// Airport Locker Rental Services
builder.Services.AddSingleton<LockerManager>(provider =>
{
    return new LockerManager(new LockerRepository());
});

// 4th Wall Academy Services
builder.Services.AddScoped<IAdmissionsService>(provider =>
{
    var academyServiceFactory = provider.GetRequiredService<AcademyServiceFactory>();
    return academyServiceFactory.CreateAdmissionsService();
});

builder.Services.AddScoped<IStudentService>(provider =>
{
    return provider.GetRequiredService<AcademyServiceFactory>().CreateStudentService();
});

builder.Services.AddScoped<IAcademySelectList, AcademySelectList>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddOpenApiDocument(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.Title = "Café API";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "AcademyArea",
    areaName: "Academy",
    pattern: "Academy/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AirportArea",
    areaName: "Airport",
    pattern: "Airport/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "BattleshipArea",
    areaName: "Battleship",
    pattern: "Battleship/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "CafeArea",
    areaName: "Cafe",
    pattern: "Cafe/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseOpenApi();

app.UseSwaggerUi(o =>
{
    o.DocumentTitle = "Café API";
});

app.Run();