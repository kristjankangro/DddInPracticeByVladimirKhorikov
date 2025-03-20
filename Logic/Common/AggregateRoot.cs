using Logic;

namespace DomainDrivenDesign.Logic.Common;

public abstract class AggregateRoot : Entity
{
	private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();	
	public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
	
	protected virtual void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
	
	public virtual void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}
}