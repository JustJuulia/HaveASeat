using haveaseat.Entities;
using haveaseat.Models;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.DbContexts;
/// <summary>
/// This class represents the database context for the application, providing access to the various entities in the database.
/// </summary>
/// <seealso cref="ForbiddenDate"/>
/// <seealso cref="Desk"/>
/// <seealso cref="Reservation"/>
/// <seealso cref="Room"/>
/// <seealso cref="User"/>
/// <seealso cref="Cell"/>
/// <seealso cref="haveaseat.Entities.Area"/>

public class DataContext : DbContext
{
    /// <summary>
    /// Gets or sets the ForbiddenDates table.
    /// </summary>
    public DbSet<ForbiddenDate> ForbiddenDates { get; set; }
    /// <summary>
    /// Gets or sets the Desks table.
    /// </summary>
    public DbSet<Desk> Desks { get; set; }
    /// <summary>
    /// Gets or sets the Reservations table.
    /// </summary>
    public DbSet<Reservation> Reservations { get; set; }
    /// <summary>
    /// Gets or sets the Rooms table.
    /// </summary>
    public DbSet<Room> Rooms { get; set; }
    /// <summary>
    /// Gets or sets the Users table.
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// Gets or sets the Cells table.
    /// </summary>
    public DbSet<Cell> Cells { get; set; }
    /// <summary>
    /// Gets or sets the Area table.
    /// </summary>
    public DbSet<Area> Area { get; set; }

    private readonly IConfiguration _configuration;
    /// <summary>
    /// Initializes a new instance of the DataContext class.
    /// </summary>
    /// <param name="configuration">The IConfiguration instance used to access configuration settings.</param>
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Configures the database context to use a PostgreSQL database.
    /// The connection string is retrieved from the appsettings.json file.
    /// </summary>
    /// <remarks>The connection string is retrieved from the <c>appsettings.json</c> file.</remarks>
    /// <param name="optionsBuilder">The options builder used to configure the database context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDatabase"));
    }
    /// <summary>
    /// Configures the entity mappings for the database context.
    /// </summary>
    ///<remarks>
    /// This method configures the following entity mappings:
    /// <list type="bullet">
    /// <item>
    /// <description><c>ForbiddenDate</c>: Configures the primary key, alternate key, and index on the Date property.</description>
    /// </item>
    /// <item>
    /// <description><c>User</c>: Configures the one-to-many relationship with <c>Reservations</c> and sets the default value for the <c>Role</c> property.</description>
    /// </item>
    /// <item>
    /// <description><c>Desk</c>: Configures the one-to-many relationship with Reservations.</description>
    /// </item>
    /// <item>
    /// <description><c>Room</c>: Configures the one-to-many relationships with <c>Desks</c> and <c>Cells</c>.</description>
    /// </item>
    /// <item>
    /// <description><c>Area</c>: Seeds initial data for the <c>Area</c> entity.</description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="modelBuilder">The model builder used to configure the entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ForbiddenDate>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<ForbiddenDate>()
            .HasAlternateKey(e => e.Date);

        modelBuilder.Entity<ForbiddenDate>()
            .HasIndex(e => e.Date);

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