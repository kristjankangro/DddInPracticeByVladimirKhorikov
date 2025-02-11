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
}