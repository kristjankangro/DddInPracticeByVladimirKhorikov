using FluentNHibernate.Mapping;

namespace Logic;

public class SnackMap : ClassMap<Snack>
{
	public SnackMap()
	{
		Id(x => x.Id);
		Map(x => x.Name);
	}
}