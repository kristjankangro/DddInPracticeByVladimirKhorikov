using DomainDrivenDesign.Logic.Common;
using NHibernate.Event;

namespace DomainDrivenDesign.Logic.Utils;

internal class EventListener :
	IPostInsertEventListener,
	IPostDeleteEventListener,
	IPostUpdateEventListener,
	IPostCollectionUpdateEventListener
{
	public void OnPostInsert(PostInsertEvent @event) =>
		DispatchEvents(@event.Entity as AggregateRoot);

	public void OnPostDelete(PostDeleteEvent @event) => 
		DispatchEvents(@event.Entity as AggregateRoot);

	public void OnPostUpdate(PostUpdateEvent @event) =>
		DispatchEvents(@event.Entity as AggregateRoot);

	public void OnPostUpdateCollection(PostCollectionUpdateEvent @event) =>
		DispatchEvents(@event.AffectedOwnerOrNull as AggregateRoot);

	private void DispatchEvents(AggregateRoot? aggregateRoot)
	{
		foreach (var domainEvent in aggregateRoot.DomainEvents)
		{
			// Dispatch domain event
			DomainEvents.Dispatch(domainEvent);
		}

		aggregateRoot.ClearDomainEvents();
	}

	public Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken) => throw new NotImplementedException();

	public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken) => throw new NotImplementedException();

	public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken) => throw new NotImplementedException();

	public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken) => throw new NotImplementedException();
	private async Task DispatchEventsAsync(AggregateRoot? aggregateRoot, CancellationToken cancellationToken) => throw new NotImplementedException();
}