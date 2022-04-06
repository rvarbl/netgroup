using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);


/*
 * Services
 * Database: PostgreSQL -> using nuget package: Npgsql.EntityFrameworkCore.PostgreSQL
 * Identity: Configured for both MVC and Rest Api.
 * Authentication: Cookie for MVC, Token for Api.
 */
//Database
var connectionString = builder.Configuration.GetConnectionString("NpgSqlConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//Custom Model binders (currently not used)
//builder.Services.AddControllersWithViews();

//Authentication TODO!
builder.Services.AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; });

var app = builder.Build();

/*
 * Setup Pipeline
 */

// - Custom setup automation helper
ApplicationSetupDataHelper.SetupAppData(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    "default",
    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "areas",
        areaName: "areas",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapRazorPages();

app.Run();