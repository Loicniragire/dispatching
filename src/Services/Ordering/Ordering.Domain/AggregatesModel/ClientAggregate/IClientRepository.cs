namespace Ordering.Domain.AggregatesModel.ClientAggregate;

public interface IClientRepository
{
	Client Add(Client client);
	Client Update(Client client);
	Task<Client> FindAsync(Guid id);
	Task<Client> FindByIdAsync(int id);
}
