using DddInPractice.Logic;
using Logic;
using NHibernate;
using Xunit;

namespace Tests;

public class TemporaryTest
{
	[Fact]
	public void Test()
	{
		SessionFactory.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");
		using (ISession session = SessionFactory.OpenSession())
		{
			long id = 1;
			var sm = session.Get<SnackMachine>(id);
		}
	}
}