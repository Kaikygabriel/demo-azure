using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Mappings;

internal sealed class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        
        builder.HasKey(x=>x.Id)
            .HasName("pk_category_id");

        builder.Property(x=>x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("varchar")
            .HasMaxLength(160)
            .IsRequired();
    }
}