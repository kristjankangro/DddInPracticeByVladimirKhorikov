using FluentNHibernate.Mapping;

namespace Logic
{
    public class SnackMachineMap : ClassMap<SnackMachine>
    {
        public SnackMachineMap()
        {
            Id(x => x.Id);
            
            Component(x => x.MoneyInside, y =>
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
