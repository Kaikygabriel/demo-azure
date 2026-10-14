using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shoppping.web;
using MudBlazor.Services;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;
using Shoppping.web.Security;
using Shoppping.web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddLocalStorageServices();

builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<LocalStorage>();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthProvider>();
builder.Services.AddScoped<JwtAuthProvider>();
builder.Services.AddScoped<RequestHandler>();

builder.Services.AddScoped(x=>(JwtAuthProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddHttpClient(Configuration.httpClientAuth, x =>
{
    x.BaseAddress = new Uri(Configuration.BackEndUri);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient(Configuration.HttpClient, x =>
{
    x.BaseAddress = new Uri(Configuration.BackEndUri);
});

var app = builder.Build();

await app.RunAsync();