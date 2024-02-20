namespace Ordering.Infrastructure.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> clientConfiguration)
    {
        ConfigureTable(clientConfiguration);
        ConfigureKeys(clientConfiguration);
        ConfigureIgnoredFields(clientConfiguration);
        ConfigureIdentityGuid(clientConfiguration);
        ConfigureProperties(clientConfiguration);
    }

    private void ConfigureTable(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.ToTable("clients", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureKeys(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.HasKey(c => c.Id);
        clientConfiguration.Property(c => c.Id)
            .UseHiLo("clientseq", OrderingContext.DEFAULT_SCHEMA);
    }

    private void ConfigureIgnoredFields(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.Ignore(c => c.DomainEvents);
    }

    private void ConfigureIdentityGuid(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.Property(c => c.IdentityGuid)
            .HasMaxLength(200)
            .IsRequired();
        clientConfiguration.HasIndex("IdentityGuid")
            .IsUnique(true);
    }
    
    private void ConfigureProperties(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.Property(c => c.Name)
            .IsRequired();
    }
}
