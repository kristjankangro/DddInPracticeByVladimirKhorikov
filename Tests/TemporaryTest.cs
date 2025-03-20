using DomainDrivenDesign.Logic.Management;
using DomainDrivenDesign.Logic.Utils;
using Xunit;

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