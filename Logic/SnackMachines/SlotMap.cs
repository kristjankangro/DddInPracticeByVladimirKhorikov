using FluentNHibernate.Mapping;

namespace Logic;

public class SlotMap : ClassMap<Slot>
{
	public SlotMap()
	{
		Id(x => x.Id);
		Map(x => x.Position);

		Component(x => x.SnackPile, y =>
		{
			y.Map(x => x.Quantity);
			y.Map(x => x.Price);
			y.References(x => x.Snack).Not.LazyLoad();
		});
		
		References(x => x.SnackMachine);
	}
	
}