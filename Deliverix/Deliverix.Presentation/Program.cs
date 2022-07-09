using System.Text;
using Deliverix.Common.Configurations;
using Deliverix.DAL.Models.Contexts;
using DispensaryGreen.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeliverixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Entities")));

AppConfiguration.Initialize(builder.Configuration);

//ConfigureServices
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()        
    );
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        var serverSecret = AppConfiguration.GetConfiguration("ServerSecret");
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(serverSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
        };
    } 
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Any",
        policy => policy.RequireRole("Buyer", "Courier", "Admin"));
    options.AddPolicy("Buyer",
        policy => policy.RequireRole("Buyer"));
    options.AddPolicy("Courier",
        policy => policy.RequireRole("Courier"));
    options.AddPolicy("Admin",
        policy => policy.RequireRole("Admin"));
});

builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<HttpExceptionMiddleware>();

app.MapControllers();

app.Run();