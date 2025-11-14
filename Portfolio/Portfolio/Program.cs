using Cafe.BLL;
using Cafe.Core.Interfaces.Application;
using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
using Cafe.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio;
using Portfolio.Utilities;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
builder.Services.AddSingleton<ServiceFactory>(provider =>
{
    var appConfig = provider.GetRequiredService<IAppConfiguration>();
    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
    var jwtConfig = provider.GetRequiredService<IConfiguration>();
    return new ServiceFactory(appConfig, loggerFactory, jwtConfig);
});

var appConfig = new AppConfiguration(builder.Configuration);
var connectionString = appConfig.GetConnectionString();

builder.Services.AddDbContext<IdentityCafeContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityCafeContext>()
.AddDefaultTokenProviders();

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

builder.Services.AddScoped<IPaymentService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreatePaymentService();
});

builder.Services.AddScoped<IMVPaymentService>(provider =>
{
    var serviceFactory = provider.GetRequiredService<ServiceFactory>();
    return serviceFactory.CreateMVCPaymentService();
});

builder.Services.AddScoped<ISelectListBuilder, SelectListBuilder>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
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

builder.Services.AddOpenApiDocument(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.Title = "Cafe API";
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

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseOpenApi();
app.UseSwaggerUi(o =>
{
    o.DocumentTitle = "Cafe API";
});

app.Run();