using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularShop.Modules.Orders.Domain.Entities;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Configurations;

internal sealed class ProductSnapshotConfiguration : IEntityTypeConfiguration<ProductSnapshot>
{
    public void Configure(EntityTypeBuilder<ProductSnapshot> builder)
    {
        builder.ToTable("ProductSnapshots");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IsActive);

        builder.ComplexProperty(x => x.UnitPrice, money =>
        {
            money.Property(x => x.Amount)
                .HasColumnName("UnitPrice");

            money.Property(x => x.Currency)
                .HasColumnName("Currency");
        });
    }
}