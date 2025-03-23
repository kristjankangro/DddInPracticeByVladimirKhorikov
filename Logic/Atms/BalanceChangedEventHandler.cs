using DomainDrivenDesign.Logic.Common;

namespace DomainDrivenDesign.Logic.Atms;

public class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
{
	public void Handle(BalanceChangedEvent domainEvent)
	{
		// EsbGateway.Instance.SendBalanceChangedMessage(domainEvent.Delta);
	}
}