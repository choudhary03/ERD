using ERD.Models;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.OpenApi.Models;
using ERD.Services;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ERD;
using ERD.Service;
using ERD.Automapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ERDContextConnection");

//builder.Services.AddDbContext<ERDContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ERDContext>();

//builder.Services.AddDbContext<ERDContext>(x => x.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ERDContext>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ERDContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ERDConnection")));
//builder.Services.AddDbContext<ERDContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ActivitiesService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<TeamsService>();
builder.Services.AddScoped<VenueService>();
builder.Services.AddScoped<BookingService>();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Identity/Account/Login"; });
builder.Services.ConfigureApplicationCookie(options => { options.LoginPath = "/Identity/Account/Login"; });
builder.Services.ConfigureApplicationCookie(options => { options.AccessDeniedPath = "/Identity/Account/AccessDenied"; });

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");

//}
//else
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        options.RoutePrefix = string.Empty;
//    });
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
else
{
    {
        app.UseStatusCodePagesWithRedirects("/Error/{0}");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

