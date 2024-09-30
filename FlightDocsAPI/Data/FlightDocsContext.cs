using Microsoft.EntityFrameworkCore;
using FlightDocsAPI.Models;

namespace FlightDocsAPI.Data
{
    public class FlightDocsContext : DbContext
    {
        public FlightDocsContext(DbContextOptions<FlightDocsContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<UserFlightAssignment> UserFlightAssignments { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Assignments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Assignments)
                .WithOne(a => a.Flight)
                .HasForeignKey(a => a.FlightID);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Documents)
                .WithOne(d => d.Flight)
                .HasForeignKey(d => d.FlightID);
        }
    }
}
