using API.Middleware;
using Application.Core;
using Application.CQRS.Admins;
using Application.CQRS.CategoryOfDiets;
using Application.CQRS.DayWeeks;
using Application.CQRS.Dieticians;
using Application.CQRS.Diets;
using Application.CQRS.Dishes;
using Application.CQRS.MealTimes;
using Application.CQRS.Patients;
using Application.Services;
using DietDB;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using ModelsDB;
using Application.CQRS.Measures;
using Application.CQRS.Units;
using ModelsDB.Functionality;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Application.Services.EmailSends;
using Application.CQRS.CountryStates;
using Application.CQRS.DieticiansBusinessesCards;
using Application.CQRS.DieticiansPatients;
using Application.CQRS.DietsForPatients;
using Application.CQRS.Diplomas;
using Application.CQRS.FoodCatalogs;
using Application.CQRS.Ingredients;
using Application.CQRS.Meals;
using Application.CQRS.Offices;
using Application.CQRS.ReportTemplates;
using Application.CQRS.Sexes;

var builder = WebApplication.CreateBuilder(args);

// Dodaje uslugi do kontenera.

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
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put Barer + your token in the box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<DietContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/// <summary>
/// Dodaje wsparcie dla CORS.
/// </summary>
builder.Services.AddCors();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    // TODO : można poniższe wprowadzić
    //opt.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DietContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

/// <summary>
/// Dodaje i konfiguruje MediatR.
/// </summary>
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(AdminList.Handler).Assembly,
    typeof(AdminMessageList.Handler).Assembly,
    typeof(CategoryOfDietList.Handler).Assembly,
    typeof(CountryStateList.Handler).Assembly,
    typeof(DayWeekList.Handler).Assembly,
    typeof(DieticianBusinessCardFilterList.Handler).Assembly,
    typeof(DieticianList.Handler).Assembly,
    typeof(DieticianMessageList.Handler).Assembly,
    typeof(DieticianBusinessCardList.Handler).Assembly,
    typeof(FromDieticianToPatientList.Handler).Assembly,
    typeof(FromPatientToDieteticianList.Handler).Assembly,
    typeof(DietList.Handler).Assembly,
    typeof(DietsForDieticianList.Handler).Assembly,
    typeof(DietsForPatientFromDieticianList.Handler).Assembly,
    typeof(DietsForPatientList.Handler).Assembly,
    //typeof(DiplomasDieticianList.Handler).Assembly,
    typeof(DishesList.Handler).Assembly,
    typeof(Application.CQRS.Dieticians.MessagesFilters.FilterList.Handler).Assembly,
    typeof(Application.CQRS.Patients.MessagesFilters.FilterList.Handler).Assembly,
    typeof(FoodCatalogList.Handler).Assembly,
    //typeof(IngredientDieticianList.Handler).Assembly,
    //typeof(IngredientONLYDieticianList.Handler).Assembly,
    typeof(IngredientsAllListNOpagination.Handler).Assembly,
    //typeof(InvitationsDieticianList.Handler).Assembly,
    //typeof(InvitationsList.Handler).Assembly,
    //typeof(InvitationsPatientList.Handler).Assembly,
    //typeof(MealList.Handler).Assembly,
    typeof(MealTimeList.Handler).Assembly,
    typeof(MeasureList.Handler).Assembly,
    typeof(OfficeList.Handler).Assembly,
    typeof(PatientList.Handler).Assembly,
    typeof(PatientMessageList.Handler).Assembly,
    typeof(ReportTemplatesList.Handler).Assembly,
    typeof(SexesList.Handler).Assembly,
    //typeof(SpecializationsList.Handler).Assembly,
    typeof(UnitList.Handler).Assembly
    ));

/// <summary>
/// Dodaje i konfiguruje AutoMapper.
/// </summary>
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ReportService>();

/// <summary>
/// Dodaje wsparcie dla walidacji z FluentValidation.
/// </summary>
builder.Services.AddFluentValidationAutoValidation();

var emailSendConfiguration = builder.Configuration
    .GetSection("EmailSenderConfiguration")
    .Get<EmailSenderConfiguration>();
builder.Services.AddSingleton(emailSendConfiguration);
builder.Services.AddScoped<IEmailSender, EmailService>();

var app = builder.Build();

/// <summary>
/// Konfiguruje potok ¿¹dañ HTTP.
/// </summary>
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/// <summary>
/// Tworzy zakres dla uslug i inicjuje baze danych.
/// </summary>
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

try
{
    var context = services.GetRequiredService<DietContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();