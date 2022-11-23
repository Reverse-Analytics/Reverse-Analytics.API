using Reverse_Analytics.Api.Extensions;
using Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_logs.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_logs.txt", Serilog.Events.LogEventLevel.Fatal, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.RegisterDependencyInjection();

// Identity
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson(options =>
    {
        // Use the default property (Pascal) casing
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
    })
    .AddXmlDataContractSerializerFormatters();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.SeedDatabase();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
