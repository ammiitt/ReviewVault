using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewVault.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Data.Configurations
{
    public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaTypeEntity>
    {
        public void Configure(EntityTypeBuilder<MediaTypeEntity> builder)
        {
            builder.ToTable("MediaTypes");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Description).HasMaxLength(200);
            builder.Property(m => m.IsActive).HasDefaultValue(true);

            builder.HasIndex(m => m.Name).IsUnique();

            // Seed data — your lookup table values
            builder.HasData(
                new MediaTypeEntity { Id = 1, Name = "Movie", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new MediaTypeEntity { Id = 2, Name = "Anime", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new MediaTypeEntity { Id = 3, Name = "Series", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new MediaTypeEntity { Id = 4, Name = "K-Drama", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new MediaTypeEntity { Id = 5, Name = "Manhua", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new MediaTypeEntity { Id = 6, Name = "Manga", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
