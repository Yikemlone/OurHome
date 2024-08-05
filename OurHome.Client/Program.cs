using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OurHome.Client;
using OurHome.DataAccess.Services.AuthorizeServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();

builder.Services.AddAuthorizationCore(options => {
    options.AddPolicy("HomeOwner", policy => policy.RequireClaim("HomeOwner", "homeOwner"));
    options.AddPolicy("HomeUser", policy => policy.RequireClaim("HomeUser", "homeUser"));
    options.AddPolicy("User", policy => policy.RequireClaim("User", "user"));
});

builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();