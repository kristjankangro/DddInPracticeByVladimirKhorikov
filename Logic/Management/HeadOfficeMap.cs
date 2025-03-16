using FluentNHibernate.Mapping;

namespace DomainDrivenDesign.Logic.Management
{
    public class HeadOfficeMap : ClassMap<HeadOffice>
    {
        public HeadOfficeMap()
        {
            Id(x => x.Id);

            Map(x => x.Balance);

            Component(x => x.Cash, y =>
            {
                y.Map(x => x.Cents1Count);
                y.Map(x => x.Cents10Count);
                y.Map(x => x.Cents25Count);
                y.Map(x => x.Dollar1Count);
                y.Map(x => x.Dollar5Count);
                y.Map(x => x.Dollar20Count);
            });
        }
    }
}
