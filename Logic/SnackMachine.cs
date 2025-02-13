using static Logic.Money;

namespace Logic;

public class SnackMachine : AggregateRoot
{
	public SnackMachine()
	{
		MoneyInside = Zero;
		MoneyInTransaction = Zero;
		Slots = new List<Slot>
		{
			new Slot(this, 1),
			new Slot(this, 2),
			new Slot(this, 3),
		};
	}

	public virtual Money MoneyInside { get; protected set; }
	public virtual Money MoneyInTransaction { get; protected set; }
	protected virtual IList<Slot> Slots { get; set; }

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

	public virtual void BuySnack(int position)
	{
		var slot = Slots.FirstOrDefault(s => s.Position == position);
		
		if (MoneyInTransaction.Amount < slot.SnackPile.Price) throw new InvalidOperationException();
		
		slot.SnackPile = slot.SnackPile.SubtractOne();
		
		MoneyInside += MoneyInTransaction;
		MoneyInTransaction = Zero;
	}

	public virtual SnackMachine LoadSnacks(int position, SnackPile snack)
	{
		var slot = Slots.Single(s => s.Position == position);
		slot.SnackPile = snack;
		return this;
	}

	public virtual SnackPile GetSnackPile(int postion)
	{
		return Slots.Single(s => s.Position == postion).SnackPile;
	}
}