using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using practice.DAL.Models;

namespace practice.DAL.Infrastructure.Configurations
{
    public class TrackLocationConfiguration : IEntityTypeConfiguration<TrackLocation>
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<TrackLocation> builder)
        {
            builder.ToTable("TrackLocation", TrackLocationContext.Default_Schema);
            builder.HasKey(data => data.Id);

            builder.Property(data => data.Imei);
            builder.Property(data => data.Latitude);
            builder.Property(data => data.Longitude);
            builder.Property(data => data.date_track);
            builder.Property(data => data.TypeSource);
        }
    }
}
