using DomainDrivenDesign.Logic.Common;
using Logic.Common;
using Logic.SharedKernel;
using static Logic.SharedKernel.Money;

namespace Logic;

public class SnackMachine : AggregateRoot
{
	public SnackMachine()
	{
		MoneyInside = None;
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
		Money returnMoney = MoneyInside.AllocateCore(MoneyInTransaction);
		MoneyInside -= returnMoney;
		MoneyInTransaction = 0;
	}

	public virtual string CanBuySnack(int pos)
	{
		SnackPile snackPile = GetSnackPile(pos);
		if (snackPile.Quantity == 0)
		{
			return "No snack available";
		}

		if (MoneyInTransaction < snackPile.Price)
		{
			return "Not enough money";
		}
		
		if(!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
			return "Not enough change";

		return string.Empty;
	}

	public virtual SnackMachine BuySnack(int position)
	{
		if(CanBuySnack(position) != string.Empty)
			throw new InvalidOperationException("Can't buy a snack");
		
		var slot = GetSlot(position);
		slot.SnackPile = slot.SnackPile.SubtractOne();

		var change = MoneyInside.AllocateCore(MoneyInTransaction - slot.SnackPile.Price);
		MoneyInside -= change;
		MoneyInTransaction = 0;
		return this;
	}

	private Slot GetSlot(int position)
	{
		return Slots.Single(s => s.Position == position);
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

	public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
	{
		return Slots.OrderBy(x => x.Position)
			.Select(x => x.SnackPile)
			.ToList();
	}

	public virtual SnackMachine LoadMoney(Money money)
	{
		MoneyInside += money;
		return this;
	}

	public virtual Money UnloadMoney()
	{
		if (MoneyInTransaction > 0)
			throw new InvalidOperationException();

		Money money = MoneyInside;
		MoneyInside = Money.None;
		return money;
	}
}