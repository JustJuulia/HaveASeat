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
///<summary>
/// Adds services to the container.
/// </summary>
builder.Services.AddControllers();
/// <summary>
/// Configures CORS policy to allow any origin, header, and method.
/// </summary>
builder.Services.AddCors(policy => 
    policy.AddPolicy("corsapp",builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    })
);
/// <summary>
/// Adds scoped services for dependency injection.
/// </summary>
/// <seealso cref="ReservationRepository"/>
/// <seealso cref="MapRepository"/>
/// <seealso cref="AuthenticationRepository"/>
/// <seealso cref="ForbiddenDateRepository"/>
/// <seealso cref="DataContext"/>
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IForbiddenDateRepository, ForbiddenDateRepository>();
builder.Services.AddScoped<DataContext>(); 



builder.Services.AddEndpointsApiExplorer();
/// <summary>
/// Configures Swagger for API documentation.
/// </summary>
builder.Services.AddSwaggerGen(options=>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "HaveASeat_api", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

var app = builder.Build();

///<summary>
/// Configure the HTTP request pipeline.
///</summary>


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
/// <summary>
/// Seeds the map data.
/// </summary>
/// <seealso cref="haveaseat.Seeders.MapSeeder"/>
app.SeedMap();

/// <summary>
/// Seeds the desk data.
/// </summary>
/// /// <seealso cref="haveaseat.Seeders.DeskSeeder"/>
app.SeedDesks();

/// <summary>
/// Applies the configured CORS policy.
/// </summary>
app.UseCors("corsapp");
app.Run();