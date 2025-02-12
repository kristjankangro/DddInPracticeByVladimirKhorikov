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
	public void Buysnack_MoneyInTransactionGoesToMoneyInMachine()
	{
		var snackMachine = new SnackMachine();
		snackMachine.InsertMoney(Dollar);
		snackMachine.InsertMoney(Dollar);
		snackMachine.BuySnack();
		
		snackMachine.MoneyInTransaction.Should().Be(Zero);
		snackMachine.MoneyInside.Amount.Should().Be(2);
	}
}