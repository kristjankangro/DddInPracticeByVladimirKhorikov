using DomainDrivenDesign.Logic.Utils;
using Logic.Atms;
using Logic.Common;
using NHibernate;

namespace DomainDrivenDesign.Logic.Atms;

public class AtmRepo : Repository<Atm>
{
	public IReadOnlyList<AtmDto> GetAtmList()
	{
		using (ISession session = SessionFactory.OpenSession())
		{
			return session.Query<Atm>()
				.ToList()//load to memory
				.Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
				.ToList();
		}
	}
}