using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.AggregatesModel.ClientAggregate;

namespace Ordering.Infrastructure.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> buyerConfiguration)
    {
        buyerConfiguration.ToTable("clients", OrderingContext.DEFAULT_SCHEMA);

        buyerConfiguration.HasKey(b => b.Id);

        buyerConfiguration.Ignore(b => b.DomainEvents);

        buyerConfiguration.Property(b => b.Id)
            .UseHiLo("buyerseq", OrderingContext.DEFAULT_SCHEMA);

        buyerConfiguration.Property(b => b.IdentityGuid)
            .HasMaxLength(200)
            .IsRequired();

        buyerConfiguration.HasIndex("IdentityGuid")
            .IsUnique(true);

        buyerConfiguration.Property(b => b.Name);
    }
}
