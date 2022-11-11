using DailyResults.Repository;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NewbridgeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddScoped<DailyResultsRepository>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();