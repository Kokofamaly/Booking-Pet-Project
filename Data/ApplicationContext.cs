using Microsoft.EntityFrameworkCore;
using BookingPetProject.Models;

namespace BookingPetProject.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<MasterClass> MasterClasses { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(u => u.Bookings).WithOne(b => b.User).HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MasterClass>().HasMany(mc => mc.Bookings).WithOne(b => b.MasterClass).HasForeignKey(b => b.MasterClassId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    Name = "Admin",
                    Email = "admin@site.com",
                    Password = "1234",
                    Role = "admin"
                },

                new User
                {
                    Id = "2",
                    Name = "User",
                    Email = "user@site.com",
                    Password = "1234",
                    Role = "user"
                }
            );

            modelBuilder.Entity<MasterClass>().HasData(
                new MasterClass
                {
                    Id = 1,
                    Name = "Painting Basics",
                    Description = "Intro to painting for beginners",
                    DateTimeEvent = new DateTime(2025, 12, 1, 15, 0, 0),
                    Capacity = 10
                },

                new MasterClass
                {
                    Id = 2,
                    Name = "Cooking Workshop",
                    Description = "Learn to cook simple dishes",
                    DateTimeEvent = new DateTime(2025, 12, 2, 15, 0, 0),
                    Capacity = 8
                }
            );
        }
    }
}