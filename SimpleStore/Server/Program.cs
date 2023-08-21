using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Repository.Implementations;
using SimpleStore.Services.Interfaces;
using SimpleStore.Services.Implementations;
using SimpleStore.Services.Profiles;
using SimpleStore.DataAccess.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// SERILOG
var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Verbose)
    .WriteTo.File(
        "..\\simplestore_.log",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Error
    )
    .WriteTo.MSSqlServer(
        builder.Configuration.GetConnectionString("SimpleStoreDb"),
        new MSSqlServerSinkOptions
        {
            AutoCreateSqlDatabase = true,
            AutoCreateSqlTable = true,
            TableName = "Logs"
        },
        restrictedToMinimumLevel: LogEventLevel.Warning
    )
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.Configure<AppConfig>(builder.Configuration);

// Add connection database
builder.Services.AddDbContext<SimpleStoreDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleStoreDb"));

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// ASP.NET IDENTITY
builder.Services.AddIdentity<SimpleStoreUserIdentity, IdentityRole>(p =>
{
    p.Password.RequireDigit = true;
    p.Password.RequireLowercase = true;
    p.Password.RequireUppercase = false;
    p.Password.RequireNonAlphanumeric = false;
    p.Password.RequiredLength = 8;

    p.User.RequireUniqueEmail = true;

    p.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    p.Lockout.MaxFailedAccessAttempts = 3;
    p.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<SimpleStoreDBContext>()
    .AddDefaultTokenProviders();

// REPOSITORIES
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISaleRepository, SaleRepository>();

// SERVICES
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISaleService, SaleService>();
builder.Services.AddTransient<IUserService, UserService>();

// MAPPERS
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoryProfile>();
    config.AddProfile<ProductProfile>();
    config.AddProfile<SaleProfile>();
});

// AUTHENTICATION
builder.Services
    .AddAuthentication(c =>
    {
        c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(c =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// AUTHENTICATION AND AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using(var scope = app.Services.CreateScope())
{
    await UserSeed.Seed(scope.ServiceProvider);
}

app.Run();
