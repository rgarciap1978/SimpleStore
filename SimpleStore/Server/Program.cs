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
using SimpleStore.Shared.Response;

var builder = WebApplication.CreateBuilder(args);

// SERILOG
var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
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

// REPOSITORIES
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ISaleRepository, SaleRepository>();

// SERVICES
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<ISaleService, SaleService>();

// MAPPERS
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoryProfile>();
    config.AddProfile<ProductProfile>();
    config.AddProfile<SaleProfile>();
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


//group.MapGet("/", async (ICategoryService categoryService, IProductService productService) =>
//{
//    var response = new ResponseDTOHome();
//    try
//    {
//        var categories = await categoryService.ListAsync(null, 1, 50);
//        var products = await productService.ListAsync(null, 1, 50);

//        response.Categories = categories.Data!;
//        response.Products = products.Data!;
//        response.Success = true;

//        return Results.Ok(response);
//    }
//    catch (Exception ex)
//    {
//        response.Message = ex.Message;
//        return Results.BadRequest(response);
//    }
//});

app.Run();
