using ERD.Models;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.OpenApi.Models;
using ERD.Services;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Identity;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ERDContextConnection");;

//builder.Services.AddDbContext<ERDContext>(options => options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) .AddEntityFrameworkStores<ERDContext>();;

builder.Services.AddDbContext<ERDContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ERDConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();
// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ActivitiesService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();



    //var path = Directory.GetCurrentDirectory();
    //LoggerFactory.AddFile($"{path}\\Logs\\Log.txt");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

