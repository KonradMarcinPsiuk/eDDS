using DailyTrigger.Repository;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter()))
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()));
builder.Services.AddDbContext<NewbridgeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<DailyTriggerRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();