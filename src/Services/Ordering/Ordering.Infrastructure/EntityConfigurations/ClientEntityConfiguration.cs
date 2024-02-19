namespace Ordering.Infrastructure.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.ToTable("clients", OrderingContext.DEFAULT_SCHEMA);

        clientConfiguration.HasKey(b => b.Id);

        clientConfiguration.Ignore(b => b.DomainEvents);

        clientConfiguration.Property(b => b.Id)
            .UseHiLo("clientseq", OrderingContext.DEFAULT_SCHEMA);

        clientConfiguration.Property(b => b.IdentityGuid)
            .HasMaxLength(200)
            .IsRequired();

        clientConfiguration.HasIndex("IdentityGuid")
            .IsUnique(true);

        clientConfiguration.Property(b => b.Name);
    }
}
