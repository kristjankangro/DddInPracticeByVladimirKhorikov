using static Logic.Money;

namespace Logic;

public class SnackMachine : AggregateRoot
{
	public SnackMachine()
	{
		MoneyInside = Zero;
		MoneyInTransaction = 0;
		Slots = new List<Slot>
		{
			new Slot(this, 1),
			new Slot(this, 2),
			new Slot(this, 3),
		};
	}

	public virtual Money MoneyInside { get; protected set; }
	public virtual decimal MoneyInTransaction { get; protected set; }
	protected virtual IList<Slot> Slots { get; set; }

	public virtual SnackMachine InsertMoney(Money money)
	{
		Money[] allowed = [Cent, Cent10, Cent25, Dollar, Dollar5, Dollar20];
		if (!allowed.Contains(money)) throw new InvalidOperationException("Invalid money");

		MoneyInTransaction += money.Amount;
		MoneyInside += money;
		return this;
	}

	public virtual void ReturnMoney()
	{
		Money returnMoney = MoneyInside.Allocate(MoneyInTransaction);
		MoneyInside -= returnMoney;
		MoneyInTransaction = 0;
	}

	public virtual SnackMachine BuySnack(int position)
	{
		var slot = Slots.FirstOrDefault(s => s.Position == position);

		if (MoneyInTransaction < slot.SnackPile.Price) throw new InvalidOperationException();

		slot.SnackPile = slot.SnackPile.SubtractOne();

		var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
		if (change.Amount < MoneyInTransaction - slot.SnackPile.Price) throw new InvalidOperationException();
		
		MoneyInside -= change;
		MoneyInTransaction = 0;
		return this;
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

	public virtual SnackMachine LoadMoney(Money money)
	{
		MoneyInside += money;
		return this;
	}
}