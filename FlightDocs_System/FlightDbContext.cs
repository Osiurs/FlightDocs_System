public class FlightDbContext : DbContext
{
    public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options) { }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserFlightAssignment> UserFlightAssignments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình các quan hệ trong database
        modelBuilder.Entity<UserFlightAssignment>()
            .HasKey(ufa => new { ufa.UserID, ufa.FlightID });
    }
}
