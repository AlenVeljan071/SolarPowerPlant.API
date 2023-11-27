using FluentValidation.AspNetCore;
using Serilog;
using SolarPowerPlant.Api.Extensions;
using SolarPowerPlant.Api.Middlewares;
using SolarPowerPlant.API.Configuration.Database;
using SolarPowerPlant.API.Configuration.Extensions;
using SolarPowerPlant.API.Extensions;
using SolarPowerPlant.API.Helpers;
using SolarPowerPlant.Core.Configuration.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddApplicationServices();
builder.Services.AddOptionsPattern(builder.Configuration);
builder.Services.AddEFCoreInfrastructure(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy();

var _logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.AddSerilog(_logger);
builder.Services.AddCronJobService();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ErrorMiddleware));
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
