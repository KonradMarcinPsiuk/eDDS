using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using PlannedStop.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NewbridgeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<PlannedStopRepository>();
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