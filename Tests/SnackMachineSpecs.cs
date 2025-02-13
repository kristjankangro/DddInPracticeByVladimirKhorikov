using System;
using FluentAssertions;
using Logic;
using Xunit;
using static Logic.Money;

namespace Tests;

public class SnackMachineSpecs
{
	[Fact]
	public void ReturnMoney_EmptiesMoneyInTransaction()
	{
		var snackMachine = new SnackMachine();
		snackMachine.InsertMoney(Cent);
		snackMachine.ReturnMoney();

		snackMachine.MoneyInTransaction.Amount.Should().Be(0);
	}

	[Fact]
	public void InsertMoney_GoesToMoneyInTransaction()
	{
		var snackMachine = new SnackMachine();
		snackMachine.InsertMoney(Cent);
		snackMachine.InsertMoney(Dollar);
		
		snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
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
		snackMachine.LoadSnacks(1, new SnackPile(new Snack("mars bar"), 10, 1));
		snackMachine.InsertMoney(Dollar);
		snackMachine.BuySnack(1);
		
		snackMachine.MoneyInTransaction.Should().Be(Zero);
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
			.LoadSnacks(1, new SnackPile(new Snack("mars bar"), 10, 3))
			.InsertMoney(Dollar);
		
		Action action = () => snackMachine.BuySnack(1);
		
		action.Should().Throw<InvalidOperationException>();
	}
}