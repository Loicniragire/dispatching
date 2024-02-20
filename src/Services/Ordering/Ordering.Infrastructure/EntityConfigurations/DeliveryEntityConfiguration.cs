namespace Ordering.Infrastructure.EntityConfigurations;

public class DeliveryEntityConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        deliveryConfiguration.ToTable("deliveries", OrderingContext.DEFAULT_SCHEMA);

        deliveryConfiguration.HasKey(d => d.Id);

        deliveryConfiguration.Ignore(d => d.DomainEvents);

        deliveryConfiguration.Property<int>("OrderId")
            .IsRequired();

        deliveryConfiguration.Property(d => d.Id)
            .UseHiLo("deliveryseq", OrderingContext.DEFAULT_SCHEMA);

        // Configuration of other properties goes here

        // Assuming Delivery is created without foreign key property, hence using shadow property
        deliveryConfiguration.HasOne<Order>()
            .WithOne()
            .HasForeignKey<Delivery>("OrderId")
            .IsRequired();
    }
}
