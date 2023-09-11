using Application.CategoryOfDiets;
using Application.Core;
using Application.DayWeeks;
using Application.Examples;
using Application.MealTimes;
using Application.SingleDiets;
using Application.Tooltips;
using DietDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DietContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(
    typeof(ExampleList.Handler).Assembly,
    typeof(DayWeekList.Handler).Assembly,
    typeof(CategoryOfDietList.Handler).Assembly,
    typeof(MealTimeList.Handler).Assembly,
    typeof(SingleDietList.Handler).Assembly,
    typeof(TooltipList.Handler).Assembly
    
    ));
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
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
