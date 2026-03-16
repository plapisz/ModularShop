using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularShop.Modules.Orders.Domain.Aggregates;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.ComplexProperty(x => x.Number, number =>
        {
            number.Property(x => x.Value)
                .HasColumnName("OrderNumber")
                .IsRequired();
        });

        builder.OwnsMany(x => x.Items, items =>
        {
            items.ToTable("OrderItems");

            items.Property<Guid>("Id");
            items.HasKey("Id");

            items.Property<Guid>("OrderId");

            items.Property(x => x.ProductId);

            items.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            items.Property(x => x.Quantity)
                .IsRequired();

            items.OwnsOne(x => x.UnitPrice, money =>
            {
                money.Property(x => x.Amount)
                    .HasColumnName("UnitPrice");

                money.Property(x => x.Currency)
                    .HasColumnName("Currency");
            });

            items.Ignore(x => x.TotalPrice);
            
            items.WithOwner()
                .HasForeignKey("OrderId");
        });

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}