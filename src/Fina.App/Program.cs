using Fina.App;
using Fina.App.Handlers;
using Fina.Core;
using Fina.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();

builder.Services.AddHttpClient(
    WebConfiguration.HttpClientName,
    options => 
    { 
        options.BaseAddress = new Uri(Configuration.BackendURL);
    });
                               
await builder.Build().RunAsync();

