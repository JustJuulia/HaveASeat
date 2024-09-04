using haveaseat.Entities;
using haveaseat.Models;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.DbContexts;

public class DataContext : DbContext
{
    public DbSet<Desk> Desks { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cell> Cells { get; set; }
    public DbSet<Area> Area { get; set; }
    
    private readonly IConfiguration _configuration;
    
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Reservations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);
        
        modelBuilder.Entity<Desk>()
            .HasMany(e => e.Reservations)
            .WithOne(e => e.Desk)
            .HasForeignKey(e => e.DeskId)
            .HasPrincipalKey(e => e.Id);
        
        modelBuilder.Entity<Room>()
            .HasMany(e => e.Desks)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .HasPrincipalKey(e => e.Id);
        
        modelBuilder.Entity<Room>()
            .HasMany(e => e.Cells)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .HasPrincipalKey(e => e.Id);
        
        modelBuilder.Entity<User>()
            .Property(b => b.Role)
            .HasDefaultValue(Role.EMPLOYEE);
        
        modelBuilder.Entity<User>()
            .HasIndex(b => b.Email)
            .IsUnique();

        modelBuilder.Entity<Area>()
            .HasData(new Area
            {
                Id = 1,
                Height = 11,
                Width = 15
            });
    }
}