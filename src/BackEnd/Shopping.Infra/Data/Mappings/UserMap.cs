using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BackOffice.Entities;
using System.Text.Json;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Infra.Data.Mappings;

internal sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        
        builder.HasKey(x => x.Id)
            .HasName("pk_user_id");

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(160)
            .IsRequired();

        builder.OwnsOne(x => x.Password, x =>
        {
            x.Property(x => x.Hash)
                .HasColumnName("password")
                .HasColumnType("varchar")
                .HasMaxLength(480)
                .IsRequired();
        });
        
        builder.OwnsOne(x => x.Email, x =>
        {
            x.Property(x => x.Address)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(256)
                .IsRequired();

            x.HasIndex(x => x.Address, "ix_user_email");
        });

        builder.OwnsOne(x => x.RefreshToken, x =>
        {
            x.Property(x => x.Code)
                .HasColumnName("refresh_token")
                .HasColumnType("text")
                .IsRequired(false);

            x.Property(x => x.ExpiredAt)
                .HasColumnName("refresh_token_expired")
                .HasColumnType("timestamptz")
                .IsRequired(false);
        });
        
        builder.Property(x => x.Localization)
            .HasConversion<string>(
                x=>
                    JsonSerializer.Serialize(x),
                x=>
                    JsonSerializer.Deserialize<Localization>(x)!)
            .HasColumnName("localization")
            .HasColumnType("jsonb")
            .IsRequired();
        
        builder.Property(x => x.Roles)
            .HasConversion<string>(
                x=>
                    JsonSerializer.Serialize(x),
                x=>
                    JsonSerializer.Deserialize<List<Role>>(x)!)
            .HasColumnType("jsonb")
            .HasColumnName("roles")
            .IsRequired();

        builder.HasMany(x => x.Vouchers)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                x=>
                                x.HasOne<Voucher>()
                                    .WithMany()
                                    .HasForeignKey("voucher_id")
                                    .OnDelete(DeleteBehavior.Cascade)
                                    .IsRequired(),
                         x => 
                                x.HasOne<User>()
                                    .WithMany()
                                    .HasForeignKey("user_id")
                                    .OnDelete(DeleteBehavior.Cascade)
                                    .IsRequired(),
               
                x =>
                                {
                                    x.HasKey("user_id", "voucher_id");
                                    x.ToTable("userVoucher");
                                }
                );
    }
}