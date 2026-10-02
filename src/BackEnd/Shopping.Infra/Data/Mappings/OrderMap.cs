using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Mappings;

internal sealed class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");
        
        builder.HasKey(x => x.Id)
            .HasName("pk_order_id");
        
        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Uri)
            .HasColumnName("uri")
            .HasColumnType("varchar(380)")
            .IsRequired(false);
        
        builder.Property(x => x.StatePayment)
            .HasConversion<string>()
            .HasColumnName("state_payment")
            .HasColumnType("varchar(150)")
            .IsRequired();
        
        builder.Property(x => x.CreateAt)
            .HasColumnName("create_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_order_user_id")
            .IsRequired();
        
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .HasConstraintName("fk_order_product_id")
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(x => x.VoucherId)
            .HasColumnName("voucher_id")
            .HasColumnType("uuid")
            .IsRequired(false);
        
        builder.HasOne(x => x.Voucher)
            .WithMany()
            .HasForeignKey(x => x.VoucherId)
            .HasConstraintName("fk_order_voucher_id")
            .IsRequired(false);
    }
}