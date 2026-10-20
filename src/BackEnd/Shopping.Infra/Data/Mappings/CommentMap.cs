using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Mappings;

internal sealed class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comment");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasKey(x => x.Id)
            .HasName("pk_comment");

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ProductId)
            .HasConstraintName("fk_comment_product")
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_comment_user")
            .IsRequired();

        builder.Property(x => x.Message)
            .HasColumnName("message")
            .HasColumnType("varchar")
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(x => x.Stars)
            .HasColumnName("stars")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.CreateAt)
            .HasColumnName("create_at")
            .HasColumnType("timestamptz")
            .IsRequired();

    }
}