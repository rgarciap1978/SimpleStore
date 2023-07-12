using Microsoft.AspNetCore.ResponseCompression;
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

var builder = WebApplication.CreateBuilder(args);

// SERILOG
var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
    .WriteTo.File(
        "..\\musicstorage_.log",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Warning
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

// REPOSITORIES
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// SERVICES
builder.Services.AddTransient<ICategoryService, CategoryService>();

// MAPPERS
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoryProfile>();
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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
