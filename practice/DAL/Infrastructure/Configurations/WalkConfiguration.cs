using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using practice.DAL.Models;

namespace practice.DAL.Infrastructure.Configurations
{
    public class WalkConfiguration : IEntityTypeConfiguration<Walk>
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Walk> builder)
        {
            builder.ToTable("Walk", TrackLocationContext.Default_Schema);
            builder.HasKey(data => data.Id);

            builder.Property(data => data.Imei);
            builder.Property(data => data.Name);
            builder.Property(data => data.Duration);
            builder.Property(data => data.Distance);
        }
    }
}
