using Microsoft.EntityFrameworkCore;
using NanoSurveyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__NANOSURVEY")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.Run();
