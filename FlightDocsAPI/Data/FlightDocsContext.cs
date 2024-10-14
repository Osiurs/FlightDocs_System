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
        public DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa khóa chính và các quan hệ giữa các bảng

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID); // Đặt khóa chính cho User

            modelBuilder.Entity<Flight>()
                .HasKey(f => f.FlightID); // Đặt khóa chính cho Flight

            modelBuilder.Entity<UserFlightAssignment>()
                .HasKey(a => a.AssignmentID); // Đặt khóa chính cho UserFlightAssignment

            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentID); // Đặt khóa chính cho Document
        }
    }
}
