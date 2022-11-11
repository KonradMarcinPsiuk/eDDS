using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

using PlantModel.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<PlantModelRepository>();


//var options = new DbContextOptionsBuilder<PlantModelContext>();
//options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
builder.Services.AddDbContext<NewbridgeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
//builder.Services.AddSingleton(p => new PlantModelContext(options.Options));
//builder.Services.AddSingleton<PlantModelContext>();
//builder.Services.AddSingleton<QueueRepository>();

builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddHostedService<MessageBroker>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();