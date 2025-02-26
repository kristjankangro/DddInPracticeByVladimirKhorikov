using DddInPractice.Logic;
using Logic;
using NHibernate;
using Xunit;
using static Logic.SharedKernel.Money;

namespace Tests;

public class TemporaryTest
{
	[Fact]
	public void Test()
	{
		SessionFactory.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");
		var repo = new SnackMachineRepo();
		// SnackMachine sm = repo.GetById(1);
		// sm.InsertMoney(Dollar);
		// sm.InsertMoney(Dollar);
		// sm.InsertMoney(Dollar);
		// sm.BuySnack(1);
		// repo.Save(sm);
	}
}