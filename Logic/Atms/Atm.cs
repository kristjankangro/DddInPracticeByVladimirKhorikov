using Logic.Common;
using Logic.SharedKernel;
using static Logic.SharedKernel.Money;

namespace Logic.Atms;

public class Atm : AggregateRoot
{
	private const decimal CommissionRate = 0.01m;
	private const decimal MinimumCommission = 0.01m;
	public virtual Money MoneyInside { get; protected set; } = Zero;
	public virtual decimal MoneyCharged { get; protected set; }

	public virtual string CanWithdraw(decimal amount)
	{
		if (amount <= 0) return "Cannot withdraw 0";
		if (MoneyInside.Amount < amount) return "Cannot withdraw, not enough money inside";
		if (!MoneyInside.CanAllocate(amount))
		{
			return "Not enough change";
		}

		return string.Empty;
	}

	public virtual void Withdraw(decimal amount)
	{
		if (CanWithdraw(amount) != string.Empty) 
			throw new InvalidOperationException($"Cannot withdraw {amount}");

		Money output = MoneyInside.AllocateCore(amount);
		MoneyInside -= output;

		decimal moneyWithCharge = CalculateAmountWithComission(amount);
		MoneyCharged += moneyWithCharge;
	}

	private static decimal CalculateAmountWithComission(decimal amount)
	{
		var c = amount * CommissionRate;
		return amount +
		       (c < MinimumCommission ? MinimumCommission : Math.Round(c, 2, MidpointRounding.ToPositiveInfinity));
	}

	public void LoadMoney(Money money)
	{
		MoneyInside += money;
	}
}