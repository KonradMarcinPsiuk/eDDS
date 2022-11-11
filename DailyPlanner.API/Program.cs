using DailyPlanner.Repository;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NewbridgeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddScoped<DailyPlannerRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter()))
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()));
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();