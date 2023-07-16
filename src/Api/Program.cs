using Api.Services;
using Application;
using Application.Commons.Interfaces;
using Domain.Entities;
using Elastic.Apm.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration));


    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddCors();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ApplicationDbContext>();
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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
}
finally
{
    Log.CloseAndFlush();
}