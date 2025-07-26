using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LordSheo.Analytics.Client;
using LordSheo.Analytics.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient()
{
	BaseAddress = new Uri("https://localhost:7002/")
});

builder.Services.AddScoped<AnalyticsService>();

await builder.Build().RunAsync();