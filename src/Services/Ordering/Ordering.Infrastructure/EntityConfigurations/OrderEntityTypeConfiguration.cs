namespace Ordering.Infrastructure.EntityConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderConfiguration)
    {
        ConfigureTable(orderConfiguration);
        ConfigureKeys(orderConfiguration);
        ConfigureIgnoredFields(orderConfiguration);
        ConfigureProperties(orderConfiguration);
        ConfigureNavigation(orderConfiguration);
        ConfigureOwnsOne(orderConfiguration);
        ConfigureOrderStatusRelationship(orderConfiguration);
        ConfigureDeliveryRelationship(orderConfiguration);
    }

    private void ConfigureTable(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.ToTable("orders", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureKeys(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.HasKey(o => o.Id);
        orderConfiguration.Property(o => o.Id)
            .UseHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureIgnoredFields(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.Ignore(b => b.DomainEvents);
    }

    private void ConfigureProperties(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.Property<DateTime>("_orderDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OrderDate")
            .IsRequired();

        orderConfiguration.Property<string>("_description")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();
    }

    private void ConfigureNavigation(EntityTypeBuilder<Order> orderConfiguration)
    {
        var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Order.OrderItems));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureOwnsOne(EntityTypeBuilder<Order> orderConfiguration)
    {
        // Configure a foreign key to PickupAddress
        orderConfiguration.HasOne(o => o.PickupAddress)
            .WithMany()
            .HasForeignKey(o => o.PickupAddressId)
            .IsRequired(true)
               .OnDelete(DeleteBehavior.Restrict);

        // Configure a foreign key to DropoffAddress
        orderConfiguration.HasOne(o => o.DropoffAddress)
            .WithMany()
            .HasForeignKey(o => o.DropoffAddressId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure a foreign key to Client
        orderConfiguration.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

    }

    private void ConfigureOrderStatusRelationship(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.Property<int>("_orderStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OrderStatusId")
            .IsRequired();

        orderConfiguration.HasOne(o => o.OrderStatus)
            .WithMany()
            .HasForeignKey("_orderStatusId");
    }

    private void ConfigureDeliveryRelationship(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.HasOne(o => o.Delivery)
            .WithOne()
            .HasForeignKey<Delivery>("OrderId")
            .IsRequired(false);
    }
}
