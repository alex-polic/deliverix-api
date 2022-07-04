using Deliverix.Common.Configurations;
using Deliverix.DAL.Models.Contexts;
using DispensaryGreen.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeliverixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Entities")));

AppConfiguration.Initialize(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseMiddleware<HttpExceptionMiddleware>();

app.MapControllers();

app.Run();