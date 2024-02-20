namespace Ordering.Infrastructure.EntityConfigurations;

public class DeliveryEntityConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        ConfigureTable(deliveryConfiguration);
        ConfigureKeys(deliveryConfiguration);
        ConfigureIgnoredFields(deliveryConfiguration);
        ConfigureProperties(deliveryConfiguration);
        ConfigureRelationships(deliveryConfiguration);
    }

    private void ConfigureTable(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        deliveryConfiguration.ToTable("deliveries", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureKeys(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        deliveryConfiguration.HasKey(d => d.Id);
        deliveryConfiguration.Property(d => d.Id)
            .UseHiLo("deliveryseq", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureIgnoredFields(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        deliveryConfiguration.Ignore(d => d.DomainEvents);
    }

    private void ConfigureProperties(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        deliveryConfiguration.Property<int>("OrderId")
            .IsRequired();
		// configure the _route field
		deliveryConfiguration.Property<string>("_route")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("Route")
			.IsRequired();
		deliveryConfiguration.Property<DateTime>("_deliveryDate")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("DeliveryDate")
			.IsRequired();

		deliveryConfiguration.Property<DateTime>("_startDate")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("StartDate")
			.IsRequired();

		deliveryConfiguration.Property<TimeSpan>("_elapsedTime")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("ElapsedTime")
			.IsRequired();

		deliveryConfiguration.Property<decimal>("_gasCost")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("GasCost")
			.IsRequired();

		deliveryConfiguration.Property<decimal>("_tollsCost")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("TollsCost")
			.IsRequired();

		deliveryConfiguration.Property<decimal>("_additionalCosts")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("AdditionalCosts")
			.IsRequired();

		deliveryConfiguration.Property<double>("_startOdometer")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("StartOdometer")
			.IsRequired();

		deliveryConfiguration.Property<double>("_endOdometer")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("EndOdometer")
			.IsRequired();

		deliveryConfiguration.Property<double>("Distance")
			.HasColumnName("Distance")
			.IsRequired()
			.HasComputedColumnSql("_endOdometer - _startOdometer");

		deliveryConfiguration.Property<decimal>("TotalCost")
			.HasColumnName("TotalCost")
			.IsRequired()
			.HasComputedColumnSql("_gasCost + _tollsCost + _additionalCosts");
    }

    private void ConfigureRelationships(EntityTypeBuilder<Delivery> deliveryConfiguration)
    {
        // Assuming Delivery is created without foreign key property, hence using shadow property
        deliveryConfiguration.HasOne<Order>()
            .WithOne()
            .HasForeignKey<Delivery>("OrderId")
            .IsRequired();
    }
}
