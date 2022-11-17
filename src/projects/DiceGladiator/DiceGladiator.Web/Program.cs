using Blazored.LocalStorage;
using DiceGladiator.Domain.Services;
using DiceGladiator.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<GameService>();
builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();
