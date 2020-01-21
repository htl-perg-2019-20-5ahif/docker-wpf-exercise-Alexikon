using CarBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace CarBooking
{
    public class CarBookingContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Booking { get; set; }

        public CarBookingContext(DbContextOptions<CarBookingContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CarBooking");

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Car)
                .HasForeignKey(b => b.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
