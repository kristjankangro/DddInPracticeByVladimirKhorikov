using DddInPractice.Logic;
using DomainDrivenDesign.Logic.Management;
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
		
		HeadOfficeInstance.Init();
		HeadOffice headOffice = HeadOfficeInstance.Instance;
	}
}