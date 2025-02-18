using System;
using FluentAssertions;
using Logic;
using Xunit;
using static Logic.Money;
using static Logic.Snack;

namespace Tests;

public class SnackMachineSpecs
{
	[Fact]
	public void ReturnMoney_EmptiesMoneyInTransaction()
	{
		var snackMachine = new SnackMachine();
		snackMachine.InsertMoney(Cent);
		snackMachine.ReturnMoney();

		snackMachine.MoneyInTransaction.Should().Be(0);
	}

	[Fact]
	public void InsertMoney_GoesToMoneyInTransaction()
	{
		var snackMachine = new SnackMachine();
		snackMachine.InsertMoney(Cent);
		snackMachine.InsertMoney(Dollar);
		
		snackMachine.MoneyInTransaction.Should().Be(1.01m);
	}

	[Fact]
	public void InsertMoney_CannotInsertMoreThanOneCoin()
	{
		var snackMachine = new SnackMachine();
		var twoCents = Cent + Cent;
		var action = () => snackMachine.InsertMoney(twoCents);
		
		action.Should().Throw<InvalidOperationException>();
	}

	[Fact]
	public void BuySnack_TradeInsertedCoinsForSnack()
	{
		var snackMachine = new SnackMachine();
		snackMachine.LoadSnacks(1, new SnackPile(MarsBar, 10, 1));
		snackMachine.InsertMoney(Dollar);
		snackMachine.BuySnack(1);
		
		snackMachine.MoneyInTransaction.Should().Be(0);
		snackMachine.MoneyInside.Amount.Should().Be(1);

		snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
	}

	[Fact]
	public void BuySnack_CannotPurchasewhenNosnacks()
	{
		var snackMachine = new SnackMachine();
		
		Action action = () => snackMachine.BuySnack(1);
		
		action.Should().Throw<InvalidOperationException>();
	}

	[Fact]
	public void CannotBuySnack_NotenoughMoneyInserted()
	{
		var snackMachine = new SnackMachine();
		snackMachine
			.LoadSnacks(1, new SnackPile(MarsBar, 10, 3))
			.InsertMoney(Dollar);
		
		Action action = () => snackMachine.BuySnack(1);
		
		action.Should().Throw<InvalidOperationException>();
	}

	[Fact]

	public void BuySnack_ReturnHighestDenominatorMoneyFirst()
	{
			var snackMachine = new SnackMachine();
			snackMachine
				.LoadMoney(Dollar)
				.InsertMoney(Cent25)
				.InsertMoney(Cent25)
				.InsertMoney(Cent25)
				.InsertMoney(Cent25)
				.ReturnMoney();
			
			snackMachine.MoneyInside.Cents25Count.Should().Be(4);
			snackMachine.MoneyInside.Dollar1Count.Should().Be(0);
	}

	[Fact]
	public void BuySnack_ReturnChangeAfter()
	{
		var snackMachine = new SnackMachine()
			.LoadSnacks(1, new SnackPile(MarsBar, 1, 0.5m))
			.LoadMoney(Cent10*10)
			.InsertMoney(Dollar)
			.BuySnack(1);
		
		snackMachine.MoneyInside.Amount.Should().Be(1.5m);
		snackMachine.MoneyInTransaction.Should().Be(0);
	}
}