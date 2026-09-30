using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;



namespace Shopping.Infra.Data.Mappings;

internal sealed class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.HasKey(x => x.Id)
            .HasName("pk_product_id");

        builder.Property(x=>x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");

        builder.HasOne(x => x.Category)
            .WithMany(x=>x.Products)
            .HasForeignKey(x => x.CategoryId)
            .HasConstraintName("fk_category_id")
            .IsRequired();
        
        builder.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Summary)
            .HasColumnName("summary")
            .HasColumnType("varchar")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("money")
            .IsRequired();

        builder.Property(x => x.Discount)
            .HasColumnName("discount")
            .HasColumnType("numeric(14, 2)")
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .HasColumnType("interger")
            .IsRequired();

        builder.Property(x => x.ImageThumbUrl)
            .HasColumnName("image_thumb_url")
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(x => x.ImagesUrl)
            .HasColumnName("images")
            .HasColumnType("jsonb")
            .HasConversion<string>(x=>
                JsonSerializer.Serialize(x),
                x=>
                    x == null ?
                        new List<string>() : JsonSerializer.Deserialize<List<string>>(x) ?? new())
            .IsRequired();

        builder.Property(x => x.CreateAt)
            .HasColumnName("create_at")
            .HasColumnType("timestamptz")
            .IsRequired();
    }
}