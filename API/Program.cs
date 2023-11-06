using API.Middleware;
using Application.Core;
using Application.CQRS.Admins;
using Application.CQRS.CategoryOfDiets;
using Application.CQRS.DayWeeks;
using Application.CQRS.Dieticians;
using Application.CQRS.MealTimes;
using Application.CQRS.Patients;
using Application.CQRS.SingleDiets;
using DietDB;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Dodaje us³ugi do kontenera.

/// <summary>
/// Dodaje kontrolery i konfiguruje opcje serializacji JSON.
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

/// <summary>
/// Konfiguruje Swagger/OpenAPI.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Dodaje i konfiguruje bazê danych.
/// </summary>
builder.Services.AddDbContext<DietContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/// <summary>
/// Dodaje wsparcie dla CORS.
/// </summary>
builder.Services.AddCors();

/// <summary>
/// Dodaje i konfiguruje MediatR.
/// </summary>
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    //typeof(ExampleList.Handler).Assembly,
    typeof(DayWeekList.Handler).Assembly,
    typeof(CategoryOfDietList.Handler).Assembly,
    typeof(MealTimeList.Handler).Assembly,
    typeof(SingleDietList.Handler).Assembly,
    typeof(PatientList.Handler).Assembly,
    typeof(AdminList.Handler).Assembly,
    typeof(DieticianList.Handler).Assembly
    ));

/// <summary>
/// Dodaje i konfiguruje AutoMapper.
/// </summary>
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

/// <summary>
/// Dodaje wsparcie dla walidacji z FluentValidation.
/// </summary>
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<Create>();

var app = builder.Build();

/// <summary>
/// Konfiguruje potok ¿¹dañ HTTP.
/// </summary>
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthorization();

app.MapControllers();

/// <summary>
/// Tworzy zakres dla us³ug i inicjuje bazê danych.
/// </summary>
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DietContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
