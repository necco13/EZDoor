using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class Azuredb : IdentityDbContext<User>
    {
        public Azuredb(DbContextOptions<Azuredb> options) : base(options)
        {
        }

        public DbSet<Door> Doors { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<Review> Review { get; set; }
        override public DbSet<User> Users { get; set; } //override ker skrije inherited memberje? (ime geslo in ostalo)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Door>().ToTable("Door");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<Rent>().ToTable("Rent");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Token>().ToTable("Token");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<web.Models.Token> Token { get; set; } = default!;
    }
    
}