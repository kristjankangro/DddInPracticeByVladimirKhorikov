using DomainDrivenDesign.Logic.Atms;
using DomainDrivenDesign.Logic.Common;
using Logic.Atms;

namespace DomainDrivenDesign.Logic.Management
{
    public class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
    {
        public void Handle(BalanceChangedEvent domainEvent)
        {
            var repository = new HeadOfficeRepository();
            var headOffice = HeadOfficeInstance.Instance;
            headOffice.ChangeBalance(domainEvent.Delta);
            repository.Save(headOffice);
        }
    }
}
