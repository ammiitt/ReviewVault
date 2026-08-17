using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewVault.Infrastructure.Entities;


namespace ReviewVault.Infrastructure.Data.Configurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<BookmarkEntity>
    {
        public void Configure(EntityTypeBuilder<BookmarkEntity> builder)
        {
            builder.ToTable("Bookmarks");
            builder.HasKey(b => b.Id);

            // One bookmark per user per post
            builder.HasIndex(b => new { b.UserId, b.PostId }).IsUnique();

            builder.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Post)
                .WithMany()
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }



}
