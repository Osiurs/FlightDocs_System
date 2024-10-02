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
            // Định nghĩa khóa chính và các quan hệ giữa các bảng

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID); // Đặt khóa chính cho User

            modelBuilder.Entity<Flight>()
                .HasKey(f => f.FlightID); // Đặt khóa chính cho Flight

            modelBuilder.Entity<UserFlightAssignment>()
                .HasKey(a => a.AssignmentID); // Đặt khóa chính cho UserFlightAssignment

            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentID); // Đặt khóa chính cho Document

            // Thiết lập quan hệ giữa User và UserFlightAssignment
            modelBuilder.Entity<UserFlightAssignment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa người dùng sẽ xóa các phân công liên quan

            // Thiết lập quan hệ giữa Flight và UserFlightAssignment
            modelBuilder.Entity<UserFlightAssignment>()
                .HasOne(a => a.Flight)
                .WithMany(f => f.Assignments)
                .HasForeignKey(a => a.FlightID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa chuyến bay sẽ xóa các phân công liên quan

            // Thiết lập quan hệ giữa Flight và Document
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Flight)
                .WithMany(f => f.Documents)
                .HasForeignKey(d => d.FlightID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa chuyến bay sẽ xóa các tài liệu liên quan
        }
    }
}
