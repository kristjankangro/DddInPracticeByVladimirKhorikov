using static Logic.Money;

namespace Logic;

public class SnackMachine : Entity
{
	public SnackMachine()
	{
	}

	public virtual Money MoneyInside { get; protected set; } = Zero;
	public virtual Money MoneyInTransaction { get; protected set; } = Zero;

	public virtual void InsertMoney(Money money)
	{
		Money[] allowed = [Cent, Cent10, Cent25, Dollar, Dollar5, Dollar20];
		if (!allowed.Contains(money)) throw new InvalidOperationException("Invalid money");
		
		MoneyInTransaction += money;
	}

	public virtual void ReturnMoney()
	{
		MoneyInTransaction = Zero;
	}

	public virtual void BuySnack()
	{
		MoneyInside += MoneyInTransaction;
		MoneyInTransaction = Zero;
	}
}