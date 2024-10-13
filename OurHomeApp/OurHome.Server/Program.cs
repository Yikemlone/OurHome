using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Models.Models;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Db Context
//builder.Services.AddDbContext<OurHomeContainerDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ContainerConnection"))
//);

builder.Services.AddDbContext<OurHomeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnectionLocal"))
);

// Custom Services
builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

// Mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<OurHomeDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("HomeOwner", policy => policy.RequireRole("HomeOwner"));
    options.AddPolicy("HomeUser", policy => policy.RequireRole("HomeUser"));
    options.AddPolicy("HomeAdmin", policy => policy.RequireRole("HomeAdmin", "HomeOwner"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();  //set the allowed origin  
        });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OurHomeDbContext>();
    db.Database.Migrate();
}

app.Run();