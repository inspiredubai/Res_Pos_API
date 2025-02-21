using Microsoft.EntityFrameworkCore;
using InspirePO.Data;
using System;
using InspirePO.Repositories.CategoryRepository;
using InspirePO.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add DB Context with Windows Authentication
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
var app = builder.Build();

// Enable Swagger only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
