using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Mappings;

internal sealed class VoucherMap : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("voucher");

        builder.HasKey(x => x.Id)
            .HasName("pk_voucher_id");
        
        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasColumnName("id");

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasColumnType("char")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnName("value")
            .HasColumnType("numeric(19,2)")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.EndDate)
            .HasColumnName("end_date")
            .HasColumnType("timestamp")
            .IsRequired();
        
        builder.Property(x => x.StartDate)
            .HasColumnName("start_date")
            .HasColumnType("timestamp")
            .IsRequired();
        
        
    }
}