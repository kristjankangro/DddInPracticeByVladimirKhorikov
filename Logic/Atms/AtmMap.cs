using FluentNHibernate.Mapping;

namespace Logic.Atms;

public class AtmMap : ClassMap<Atm>
{
	public AtmMap()
	{
		Id(x => x.Id);

		Map(x => x.MoneyCharged);

		Component(x => x.MoneyInside,
			y =>
			{
				y.Map(x => x.Cents1Count);
				y.Map(x => x.Cents10Count);
				y.Map(x => x.Cents25Count);
				y.Map(x => x.Dollar1Count);
				y.Map(x => x.Dollar5Count);
				y.Map(x => x.Dollar20Count);
			}
		);
	}
}