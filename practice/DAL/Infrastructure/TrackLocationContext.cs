using Microsoft.EntityFrameworkCore;
using practice.DAL.Infrastructure.Configurations;
using practice.DAL.Models;

namespace practice.DAL.Infrastructure
{
    public class TrackLocationContext : DbContext
    {
        public const string Default_Schema = "dbo";

        public DbSet<TrackLocation> TrackLocations { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public TrackLocationContext()
        {
        }

        public TrackLocationContext(DbContextOptions<TrackLocationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrackLocationConfiguration());
            modelBuilder.ApplyConfiguration(new WalkConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
