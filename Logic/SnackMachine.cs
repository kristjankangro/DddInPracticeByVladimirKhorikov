using static Logic.Money;

namespace Logic;

public sealed class SnackMachine : Entity
{
	public Money MoneyInside { get; private set; } = Zero;
	public Money MoneyInTransaction { get; private set; } = Zero;

	public void InsertMoney(Money money)
	{
		Money[] allowed = [Cent, Cent10, Cent25, Dollar, Dollar5, Dollar20];
		if (!allowed.Contains(money)) throw new InvalidOperationException("Invalid money");
		
		MoneyInTransaction += money;
	}

	public void ReturnMoney()
	{
		MoneyInTransaction = Zero;
	}

	public void BuySnack()
	{
		MoneyInside += MoneyInTransaction;
		MoneyInTransaction = Zero;
	}
}