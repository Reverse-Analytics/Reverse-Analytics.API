using Reverse_Analytics.Api.Extensions;
using Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_logs.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_logs.txt", Serilog.Events.LogEventLevel.Fatal, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.RegisterDependencyInjection();
// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
