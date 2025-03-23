using DomainDrivenDesign.Logic.Atms;
using DomainDrivenDesign.Logic.Common;
using DomainDrivenDesign.Logic.Utils;
using FluentAssertions;
using Logic;
using Logic.Atms;
using Xunit;
using static Logic.SharedKernel.Money;

namespace Tests;

public class AtmSpec
{
	[Fact]
	public void Withdrawal_ExchangesMoneyFromMachineToCommission()
	{
		var atm = new Atm();
		atm.LoadMoney(Dollar);
		atm.TakeMoney(1);
			
		atm.MoneyInside.Should().Be(None);
		atm.MoneyCharged.Should().Be(1.01m);
	}

	[Fact]
	public void Withdrawal_CommissionIsAtLeastOneCent()
	{
		Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");

		var atm = new Atm();
		atm.LoadMoney(Cent);
		
		atm.TakeMoney(0.01m);
		
		atm.MoneyCharged.Should().Be(0.02m);
	}

	[Fact]
	public void Withdrawal_CommissionIsRoundedUpCents()
	{
		var atm = new Atm();
		atm.LoadMoney(Dollar + Cent10);
		
		atm.TakeMoney(1.1m);
		atm.MoneyCharged.Should().Be(1.12m);
	}

	[Fact]
	public void TakeMoney_RaisesAnEvent()
	{
		Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");
		var atm = new Atm();
		atm.LoadMoney(Dollar);
		atm.TakeMoney(1m);

		atm.ShouldContainBalanceChangedEvent(1.01m);
	}
}

internal static class AtmSpecExtensions
{
	public static void ShouldContainBalanceChangedEvent(this Atm atm, decimal delta)
	{
		var balanceChangedEvt = (BalanceChangedEvent) atm.DomainEvents
			.SingleOrDefault(x => x.GetType() == typeof(BalanceChangedEvent))!;
		
		balanceChangedEvt.Should().NotBeNull();
		balanceChangedEvt.Delta.Should().Be(delta);
	}
}