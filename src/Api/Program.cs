using Api.Helpers;
using Api.Services;
using Application;
using Application.Commons.Interfaces;
using Domain.Entities;
using Elastic.Apm.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    //builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    //    .ReadFrom.Configuration(hostingContext.Configuration));

    builder.Host
     .ConfigureLogging(logging =>
     {
         logging.ClearProviders();
         logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
     })
     .UseNLog();


    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddCors();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    // Add logging services

    builder.Services.AddDbContext<ApplicationDbContext>();
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    //other classes that need the logger 
    builder.Services.AddTransient<GenericLoggerHelper>();

    builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();



    builder.Services.AddOpenApiDocument(c =>
    {
        c.Title = "Product Management API";

        c.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        });

        c.OperationProcessors.Add(
            new AspNetCoreOperationSecurityScopeProcessor("JWT"));
    });

    var app = builder.Build();
    app.UseAuthentication();
    // app.UseIdentityServer();

    app.UseAuthorization();
    // APM agent setup
    app.UseElasticApm(builder.Configuration);

    app.UseOpenApi();
    app.UseSwaggerUi3(settings =>
    {
        settings.Path = "/api";
    });
    app.UseCors(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception.");
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
    Log.CloseAndFlush();
}