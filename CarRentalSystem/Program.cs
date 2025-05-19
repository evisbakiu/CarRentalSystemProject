using System.Globalization;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Services.Interfaces;
using CarRentalSystem.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authentication;
using Stripe;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>() // ← Ky rresht është i rëndësishëm
    .AddEnvironmentVariables();


var supportedCultures = new[] { new CultureInfo("en-US") }; // Use "en-US" or your preferred culture
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};



builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = false;
    options.LoginPath = "/Account/Login"; // Sigurohu që ekziston kjo rrugë

    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            if (context.Request.Path.StartsWithSegments("/Account/Login"))
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            }

            var redirectUri = "/Account/Login?sessionExpired=true&ReturnUrl=" +
                              Uri.EscapeDataString(context.Request.Path + context.Request.QueryString);
            context.Response.Redirect(redirectUri);
            return Task.CompletedTask;
        }
    };
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;  
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPasswordValidator<User>, PasswordValidator<User>>();


//kodi mbi sign in me google, facebook dhe twiter//
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})
.AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
   
})
.AddTwitter(twitterOptions =>
{
    twitterOptions.ConsumerKey = builder.Configuration["Authentication:Twitter:ConsumerKey"];
    twitterOptions.ConsumerSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"];
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
   //await SeedData.SeedDataForTheSystem(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
{
    CheckConsentNeeded = context => false,
    MinimumSameSitePolicy = SameSiteMode.Lax
});


app.UseRouting();

app.UseSession(); // KËTU AKTIVIZOHET SESSION
app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
