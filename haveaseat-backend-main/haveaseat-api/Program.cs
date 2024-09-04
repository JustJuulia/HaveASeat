global using haveaseat.Entities;
using haveaseat_api.Seeders;
using haveaseat.DbContexts;
using haveaseat.Repositories;
using haveaseat.Repositories.Interfaces;

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
builder.Services.AddScoped<DataContext>(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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