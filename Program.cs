using DemoApplication.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //// Add services to the container.
    //builder.Services.AddControllersWithViews();

    //// NLog: Setup NLog for Dependency injection
    //builder.Logging.ClearProviders();
    //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    //builder.Host.UseNLog();

    //依赖注入
    //builder.Services.AddSingleton<IStudentRepository, MockStudentRepository>();
    builder.Services.AddScoped<IStudentRepository, SQLStudentRepository>();

    //AddSingleton 创建单个实例，整个生命周期一直复用
    //AddTransient 每次请求创建一个新实例
    //AddScoped 在同个请求中复用实例

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContextPool<AppDbContext>(
           options => options.UseSqlServer(configuration["ConnectionStrings:StudentDBConnection"]));


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Home/Error");
        app.UseExceptionHandler("/Error");
        app.UseStatusCodePagesWithReExecute("/Error/{0}");
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }


    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();


}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

