using DomainDrivenDesign.Logic.Common;
using NHibernate.Event;

namespace DomainDrivenDesign.Logic.Utils;

internal class EventListener : 
	IPostInsertEventListener, 
	IPostUpdateEventListener, 
	IPostDeleteEventListener,
	IPostCollectionUpdateEventListener
{
	public async Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
	{
		await DispatchEventsAsync(@event.Entity as AggregateRoot, cancellationToken);
	}

	public void OnPostInsert(PostInsertEvent @event)
	{
		DispatchEvents(@event.Entity as AggregateRoot);
	}

	public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void OnPostUpdate(PostUpdateEvent @event)
	{
		DispatchEvents(@event.Entity as AggregateRoot);
	}

	public Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void OnPostDelete(PostDeleteEvent @event)
	{
		DispatchEvents(@event.Entity as AggregateRoot);
	}

	public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void OnPostUpdateCollection(PostCollectionUpdateEvent @event)
	{
		DispatchEvents(@event.AffectedOwnerOrNull as AggregateRoot);
	}
	private async Task DispatchEventsAsync(AggregateRoot? aggregateRoot, CancellationToken cancellationToken)
	{
		if (aggregateRoot == null) return;

		foreach (var domainEvent in aggregateRoot.DomainEvents)
		{
			// Dispatch domain event asynchronously
			await DomainEvents.DispatchAsync(domainEvent, cancellationToken);
		}
		aggregateRoot.ClearDomainEvents();
	}
	private void DispatchEvents(AggregateRoot? aggregateRoot)
	{
		foreach (var domainEvent in aggregateRoot.DomainEvents)
		{
			// Dispatch domain event
			DomainEvents.Dispatch(domainEvent);
		}
		aggregateRoot.ClearDomainEvents();
	}
}