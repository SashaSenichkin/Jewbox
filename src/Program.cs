using Jewbox.Components;
using Jewbox.Repositories;
using Jewbox.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var apiBase = builder.Configuration["ApiBaseUrl"];
var baseUri = string.IsNullOrWhiteSpace(apiBase)
    ? new Uri(builder.HostEnvironment.BaseAddress)
    : new Uri(apiBase.TrimEnd('/') + "/");

builder.Services.AddHttpClient<IApiSenderService, ApiApiSenderService>(client => { client.BaseAddress = baseUri; });

await builder.Build().RunAsync();
