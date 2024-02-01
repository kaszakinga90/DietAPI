using API.Middleware;
using Application.Core;
using Application.CQRS.Admins;
using Application.CQRS.CategoryOfDiets;
using Application.CQRS.DayWeeks;
using Application.CQRS.Dieticians;
using Application.CQRS.Diets;
using Application.CQRS.Dishes;
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
using Application.Validators.Admin;
using Application.Validators.Messages;
using Application.Validators.Dietician;
using Application.Validators.Diet;
using Application.Validators.UserRole;
using Application.Validators.Diploma;
using Application.Validators.Dish;
using Application.Validators.FoodCatalog;
using Application.Validators.Invitation;
using Application.Validators.Logo;
using Application.Validators.MealTimeToXYAxis;
using Application.Validators.Office;
using Application.Validators.PatientCard;
using Application.Validators.Patient;
using Application.Validators.Role;
using Application.Validators.Specialization;
using Application.Validators.Survey;
using Application.Validators.TestResults;
using Application.Validators.Ingredients;
using Application.BusinessLogic.CalculatesAndStatistics;
using Application.CQRS.Dishes.DishToEdit.Edits;
using Application.Validators.DishEditDetails;

var builder = WebApplication.CreateBuilder(args);

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
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 5;
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
    typeof(DieticianBusinessCardsNoPaginationList.Handler).Assembly,
    typeof(DieticiansFilterList.Handler).Assembly,
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
    typeof(MeasureList.Handler).Assembly,
    typeof(OfficeList.Handler).Assembly,
    typeof(PatientList.Handler).Assembly,
    typeof(PatientMessageList.Handler).Assembly,
    typeof(ReportTemplatesList.Handler).Assembly,
    typeof(SexesList.Handler).Assembly,
    //typeof(SpecializationsList.Handler).Assembly,
    typeof(UnitList.Handler).Assembly,
    typeof(UpdateDishIngredientsDetailsList.Handler).Assembly,
    typeof(UpdateDishFoodCatalogDetailsList.Handler).Assembly
    ));

/// <summary>
/// Dodaje i konfiguruje AutoMapper.
/// </summary>
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<CalculatorService>();

/// <summary>
/// Dodaje wsparcie dla walidacji z FluentValidation.
/// </summary>
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<UserRoleCreateValidate>();
builder.Services.AddTransient<AdminCreateValidator>();
builder.Services.AddTransient<AdminUpdateValidator>();
builder.Services.AddTransient<MessageCreateValidator>();
builder.Services.AddTransient<DieticianUpdateDataValidator>();
builder.Services.AddTransient<DieticianUpdateValidator>();
builder.Services.AddTransient<DietCreateValidator>();
builder.Services.AddTransient<DiplomaCreateValidator>();
builder.Services.AddTransient<DishCreateValidator>();
builder.Services.AddTransient<FoodCatalogDieticianCreateValidator>();
builder.Services.AddTransient<InvitationCreateValidator>();
builder.Services.AddTransient<InvitationUpdateValidator>();
builder.Services.AddTransient<LogoCreateValidator>();
builder.Services.AddTransient<MealTimeToXYAxisUpdateValidator>();
builder.Services.AddTransient<OfficeCreateValidator>();
builder.Services.AddTransient<OfficeUpdateValidator>();
builder.Services.AddTransient<PatientCardCreateValidator>();
builder.Services.AddTransient<PatientUpdateDataValidator>();
builder.Services.AddTransient<PatientUpdateValidator>();
builder.Services.AddTransient<RoleCreateValidator>();
builder.Services.AddTransient<DieticianSpecializationCreateValidator>();
builder.Services.AddTransient<SurveyCreateValidator>();
builder.Services.AddTransient<TestResultCreateValidator>();
builder.Services.AddTransient<SpecializationCreateValidator>();
builder.Services.AddTransient<FoodCatalogDieticianUpdateValidator>();
builder.Services.AddTransient<DishUpdateValidator>();
builder.Services.AddTransient<IngredientUpdateValidator>();
builder.Services.AddTransient<DietPatientCreateValidator>();
builder.Services.AddTransient<DishBaseDetailsUpdateValidator>();
builder.Services.AddTransient<DishIngredientDetailsUpdateValidator>();
builder.Services.AddTransient<DishFoodCatalogDetailsUpdateValidator>();
builder.Services.AddTransient<DishRecipeDetailsUpdateValidator>();


var emailSendConfiguration = builder.Configuration
    .GetSection("EmailSenderConfiguration")
    .Get<EmailSenderConfiguration>();
builder.Services.AddSingleton(emailSendConfiguration);
builder.Services.AddScoped<IEmailSender, EmailService>();

var app = builder.Build();

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