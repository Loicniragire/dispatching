using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.EntityConfigurations;

public class LoadEntityTypeConfiguration : IEntityTypeConfiguration<Load>
{
    public void Configure(EntityTypeBuilder<Load> orderItemConfiguration)
    {
        orderItemConfiguration.ToTable("loads", OrderingContext.DEFAULT_SCHEMA);

        orderItemConfiguration.HasKey(o => o.Id);

        orderItemConfiguration.Ignore(b => b.DomainEvents);

        orderItemConfiguration.Property(o => o.Id)
            .UseHiLo("orderitemseq");

        orderItemConfiguration.Property<int>("OrderId")
            .IsRequired();

        orderItemConfiguration
            .Property<decimal>("_discount")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Discount")
            .IsRequired();

        orderItemConfiguration.Property<int>("ProductId")
            .IsRequired();

        orderItemConfiguration
            .Property<string>("_productName")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ProductName")
            .IsRequired();

        orderItemConfiguration
            .Property<decimal>("_unitPrice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitPrice")
            .IsRequired();

        orderItemConfiguration
            .Property<int>("_units")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Units")
            .IsRequired();
    }
}

