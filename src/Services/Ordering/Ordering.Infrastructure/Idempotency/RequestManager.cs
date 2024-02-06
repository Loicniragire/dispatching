namespace Ordering.Infrastructure.Idempotency;

public class RequestManager : IRequestManager
{
    public Task CreateRequestForCommandAsync<T>(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
