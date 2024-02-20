namespace Ordering.Infrastructure.EntityConfigurations;

public class LoadEntityTypeConfiguration : IEntityTypeConfiguration<Load>
{
    public void Configure(EntityTypeBuilder<Load> loadConfiguration)
    {
        ConfigureTable(loadConfiguration);
        ConfigureKeys(loadConfiguration);
        ConfigureIgnoredFields(loadConfiguration);
        ConfigureProperties(loadConfiguration);
    }

    private void ConfigureTable(EntityTypeBuilder<Load> loadConfiguration)
    {
        loadConfiguration.ToTable("loads", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureKeys(EntityTypeBuilder<Load> loadConfiguration)
    {
        loadConfiguration.HasKey(l => l.Id);
        loadConfiguration.Property(l => l.Id)
            .UseHiLo("orderitemseq");
    }

    private void ConfigureIgnoredFields(EntityTypeBuilder<Load> loadConfiguration)
    {
        loadConfiguration.Ignore(l => l.DomainEvents);
    }

    private void ConfigureProperties(EntityTypeBuilder<Load> loadConfiguration)
    {
        loadConfiguration.Property<int>("OrderId")
            .IsRequired();

        loadConfiguration.Property<int>("ProductId")
            .IsRequired();

        loadConfiguration
            .Property<decimal>("_discount")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Discount")
            .IsRequired();

        loadConfiguration
            .Property<string>("_productName")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ProductName")
            .IsRequired();

        loadConfiguration
            .Property<decimal>("_unitPrice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitPrice")
            .IsRequired();

        loadConfiguration
            .Property<int>("_units")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Units")
            .IsRequired();
    }
}
