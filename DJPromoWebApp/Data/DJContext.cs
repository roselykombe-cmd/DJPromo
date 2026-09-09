using Microsoft.EntityFrameworkCore;
using DJPromoWebApp.Models;

namespace DJPromoWebApp.Data
{
    public class DJContext : DbContext
    {
        public DJContext(DbContextOptions<DJContext> options)
            : base(options)
        {
        }

        public DbSet<DJ> DJs { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Gig> Gigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DJ>().ToTable("DJ");
            modelBuilder.Entity<Venue>().ToTable("Venue");
            modelBuilder.Entity<Gig>().ToTable("Gig");
        }
    }
}