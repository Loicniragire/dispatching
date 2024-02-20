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
        orderConfiguration.Property<int?>("_clientId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ClientId")
            .IsRequired(false);

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
        // Assuming both PickupAddress and DropoffAddress have similar configurations
        Action<OwnedNavigationBuilder<Order, Address>> configureAddress = a => 
        {
            a.Property<int>("OrderId")
            .UseHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);
            a.WithOwner();
        };

        orderConfiguration.OwnsOne(o => o.PickupAddress, configureAddress);
        orderConfiguration.OwnsOne(o => o.DropoffAddress, configureAddress);
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
