using FluentAssertions;
using Logic;
using Xunit;

namespace Tests;

public class MoneySpecs
{
	[Fact]
	public void Sum_of_moneys_produces_correct_amount()
	{
		var money1 = new Money(1, 1, 1, 1, 1, 1);
		var money2 = new Money(1, 1, 1, 1, 1, 1);

		var sum = money1 + money2;

		sum.Cents1Count.Should().Be(2);
		sum.Cents10Count.Should().Be(2);
		sum.Cents25Count.Should().Be(2);
		sum.Dollar1Count.Should().Be(2);
		sum.Dollar5Count.Should().Be(2);
		sum.Dollar20Count.Should().Be(2);
	}

	[Fact]
	public void Two_money_instances_are_equal_if_same_amounts()
	{
		var money1 = new Money(1, 1, 1, 1, 1, 1);
		var money2 = new Money(1, 1, 1, 1, 1, 1);

		money1.Should().Be(money2);
		money1.GetHashCode().Should().Be(money2.GetHashCode());
	}

	[Fact]
	public void Two_money_instances_are_not_equal_if_different_amounts()
	{
		var cents100 = new Money(100, 0, 0, 0, 0, 0);
		var dollar = new Money(0, 0, 0, 1, 0, 0);

		cents100.Should().NotBe(dollar);
		cents100.GetHashCode().Should().NotBe(dollar.GetHashCode());
	}

	[Theory]
	[InlineData(-1, 0, 0, 0, 0, 0)]
	[InlineData(0, -1, 0, 0, 0, 0)]
	[InlineData(0, 0, -1, 0, 0, 0)]
	[InlineData(0, 0, 0, -1, 0, 0)]
	[InlineData(0, 0, 0, 0, -1, 0)]
	[InlineData(0, 0, 0, 0, 0, -1)]
	public void Cannot_create_negative_money(
		int cents1,
		int cents10,
		int cents25,
		int dollar1,
		int dollar5,
		int dollar20
	)
	{
		Action action = () => new Money(cents1, cents10, cents25, dollar1, dollar5, dollar20);

		action.Should().Throw<InvalidOperationException>();
	}

	[Theory]
	[InlineData(0, 0, 0, 0, 0, 0, 0)]
	[InlineData(1, 0, 0, 0, 0, 0, 0.01)]
	[InlineData(1, 1, 0, 0, 0, 0, 0.11)]
	[InlineData(1, 1, 1, 0, 0, 0, 0.36)]
	[InlineData(1, 1, 1, 1, 0, 0, 1.36)]
	[InlineData(1, 1, 1, 1, 1, 0, 6.36)]
	[InlineData(1, 1, 1, 1, 1, 1, 26.36)]
	public void Amount_is_calculated_correctly(
		int cents1,
		int cents10,
		int cents25,
		int dollar1,
		int dollar5,
		int dollar20,
		decimal expected)
	{
		
		new Money(cents1, cents10, cents25, dollar1, dollar5, dollar20).Amount.Should().Be(expected);
		
	}

	[Fact]
	public void Substraction_of_money_produces_correct_amount()
	{
		var money1 = new Money(10, 10, 10, 10, 10, 10);
		var money2 = new Money(1, 1, 1, 1, 1, 1);

		Money result = money1 - money2;
		
		result.Cents1Count.Should().Be(9);
		result.Cents10Count.Should().Be(9);
		result.Cents25Count.Should().Be(9);
		result.Dollar1Count.Should().Be(9);
		result.Dollar5Count.Should().Be(9);
		result.Dollar20Count.Should().Be(9);
	}
	
	[Fact]
	public void Cannot_substract_more_than_exists()
	{
		var money1 = new Money(1, 1, 1, 1, 1, 1);
		var money2 = new Money(10, 10, 10, 10, 10, 10);

		Action action = () =>
		{
			var money = money1 - money2;
		};
		
		action.Should().Throw<InvalidOperationException>();
	}
}