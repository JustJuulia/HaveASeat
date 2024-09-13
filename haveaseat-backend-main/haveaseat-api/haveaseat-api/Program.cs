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

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(policy => 
    policy.AddPolicy("corsapp",builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    })
);
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IForbiddenDateRepository, ForbiddenDateRepository>();
builder.Services.AddScoped<DataContext>(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
app.SeedMap();
app.SeedDesks();
app.UseCors("corsapp");
app.Run();