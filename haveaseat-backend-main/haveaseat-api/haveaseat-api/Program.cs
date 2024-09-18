global using haveaseat.Entities;
using haveaseat.Seeders;
using haveaseat.DbContexts;
using haveaseat.Repositories;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Adds services to the container.

builder.Services.AddControllers();

// Configures CORS policy to allow any origin, header, and method.

builder.Services.AddCors(policy => 
    policy.AddPolicy("corsapp",builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    })
);

// Adds scoped services for dependency injection.

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IForbiddenDateRepository, ForbiddenDateRepository>();
builder.Services.AddScoped<DataContext>(); 



builder.Services.AddEndpointsApiExplorer();

// Configures Swagger for API documentation.

builder.Services.AddSwaggerGen(options=>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "HaveASeat_api", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

var app = builder.Build();


// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seeds the map data.

app.SeedMap();


// Seeds the desk data.
app.SeedDesks();


// Applies the configured CORS policy.

app.UseCors("corsapp");
app.Run();