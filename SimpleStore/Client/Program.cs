using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SimpleStore.Client;
using SimpleStore.Client.Profiles;
using SimpleStore.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// PROXIES
builder.Services.AddScoped<ProxyCategory>();

// MAPPERS
builder.Services.AddAutoMapper(config => {
    config.AddProfile<CategoryProfile>();
});

// COMPONENTS
builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();

// AUTHORIZATION & AUTHENTICATION

await builder.Build().RunAsync();
