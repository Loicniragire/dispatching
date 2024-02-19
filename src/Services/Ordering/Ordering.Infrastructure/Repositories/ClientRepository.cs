namespace Ordering.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly OrderingContext _context;

    public ClientRepository(OrderingContext context)
    {
        _context = context;
    }

    public Client Add(Client client)
    {
        if (client.IsTransient())
        {
            return _context.Clients.Add(client).Entity;
        }
        return client;
    }


    public async Task<Client> FindAsync(Guid id)
    {
        return await _context.Clients.Where(c => c.IdentityGuid == id.ToString()).SingleOrDefaultAsync();
    }

    public async Task<Client> FindByIdAsync(int id)
    {
        return await _context.Clients.Where(c => c.Id == id).SingleOrDefaultAsync();
    }

    public Client Update(Client client)
    {
        return _context.Clients.Update(client).Entity;
    }
}

