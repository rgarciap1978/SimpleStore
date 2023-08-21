using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SimpleStore.Client;
using SimpleStore.Client.Auth;
using SimpleStore.Client.Profiles;
using SimpleStore.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// PROXIES
builder.Services.AddScoped<ProxyCategory>();
builder.Services.AddScoped<ProxyProduct>();
builder.Services.AddScoped<ProxyHome>();
builder.Services.AddScoped<ProxySale>();
builder.Services.AddScoped<ProxyUser>();

// MAPPERS
builder.Services.AddAutoMapper(config => {
    config.AddProfile<CategoryProfile>();
    config.AddProfile<ProductProfile>();
});

// COMPONENTS
builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();

// AUTHORIZATION & AUTHENTICATION
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
