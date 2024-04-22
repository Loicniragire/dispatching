namespace Ordering.Infrastructure;

static class MediatorExtension
{
    /// <summary>
    /// Dispatches the domain events.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="ctx">The CTX.</param>
    /// <returns></returns>
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderingContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
