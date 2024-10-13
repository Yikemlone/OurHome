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
    options.AddPolicy("HomeOwner", policy => policy.RequireRole("HomeOwner"));
    options.AddPolicy("HomeUser", policy => policy.RequireRole("HomeUser"));
    options.AddPolicy("HomeAdmin", policy => policy.RequireRole("HomeAdmin", "HomeOwner"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();