using Front_EndAPI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Point the Blazor WebAssembly app to the backend API base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5285/")
});

await builder.Build().RunAsync();
