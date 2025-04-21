using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Charts;
using GDB.Web.BLL.Implementation;
using GDB.Web.BLL.Interface;
using GDB.Web.Client.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var host = builder.Build();

builder.Services
    .AddBlazorise(options => { options.Immediate = true; }) // Ensures components render instantly
    .AddBootstrap5Providers();   

builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient("httpClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<TokenHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));


await builder.Build().RunAsync();
