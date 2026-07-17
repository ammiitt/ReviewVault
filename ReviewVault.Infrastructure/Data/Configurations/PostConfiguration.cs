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
    public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Slug).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Body).IsRequired();
            builder.Property(p => p.Summary).HasMaxLength(500);
            builder.Property(p => p.CoverImageUrl).HasMaxLength(500);
            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.IsPublished).HasDefaultValue(false);

            builder.HasIndex(p => p.Slug).IsUnique();

            builder.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.MediaType)
                .WithMany(m => m.Posts)
                .HasForeignKey(p => p.MediaTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Categories)
                .WithMany(c => c.Posts)
                .UsingEntity(j => j.ToTable("PostCategories"));
        }
    }
}

