using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Mappings;

internal sealed class ShoppingCartMap : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("shoppingCart");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasKey(x => x.Id)
            .HasName("pk_shoppingCart");
        
        builder.Property(x => x.Total)
            .HasColumnName("total")
            .HasColumnType("numeric(14,2)")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.UriPayment)
            .HasColumnName("uri_payment")
            .HasColumnType("varchar(1200)")
            .IsRequired(false);

        builder.HasOne(x => x.User)
            .WithOne(x => x.ShoppingCart)
            .HasForeignKey<ShoppingCart>(x => x.UserId)
            .HasConstraintName("fk_shoppingCart_user")
            .IsRequired();
        
        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId)
            .HasConstraintName("shoppingCart_id")
            .IsRequired();
    }
}