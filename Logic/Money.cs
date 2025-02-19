namespace Logic;

//Value Object
public class Money : ValueObject<Money>
{
	public int Cents1Count { get; }
	public int Cents10Count { get; }
	public int Cents25Count { get; }
	public int Dollar1Count { get; }
	public int Dollar5Count { get; }
	public int Dollar20Count { get; }

	public decimal Amount => Cents1Count * 0.01m +
	                         Cents10Count * 0.1m
	                         + Cents25Count * 0.25m
	                         + Dollar1Count
	                         + Dollar5Count * 5
	                         + Dollar20Count * 20;


	public static Money Zero => new(0, 0, 0, 0, 0, 0);
	public static Money Cent => new Money(1, 0, 0, 0, 0, 0);
	public static Money Cent10 => new Money(0, 1, 0, 0, 0, 0);
	public static Money Cent25 => new Money(0, 0, 1, 0, 0, 0);
	public static Money Dollar => new Money(0, 0, 0, 1, 0, 0);
	public static Money Dollar5 => new Money(0, 0, 0, 0, 1, 0);
	public static Money Dollar20 => new Money(0, 0, 0, 0, 0, 1);

	private Money()
	{
	}

	public Money(
		int cents1Count,
		int cents10Count,
		int cents25Count,
		int dollar1Count,
		int dollar5Count,
		int dollar20Count) : this()
	{
		if (cents1Count < 0) throw new InvalidOperationException();
		if (cents10Count < 0) throw new InvalidOperationException();
		if (cents25Count < 0) throw new InvalidOperationException();
		if (dollar1Count < 0) throw new InvalidOperationException();
		if (dollar5Count < 0) throw new InvalidOperationException();
		if (dollar20Count < 0) throw new InvalidOperationException();

		Cents1Count += cents1Count;
		Cents10Count += cents10Count;
		Cents25Count += cents25Count;
		Dollar1Count += dollar1Count;
		Dollar5Count += dollar5Count;
		Dollar20Count += dollar20Count;
	}

	public static Money operator +(Money left, Money right)
	{
		return new Money(
			left.Cents1Count + right.Cents1Count,
			left.Cents10Count + right.Cents10Count,
			left.Cents25Count + right.Cents25Count,
			left.Dollar1Count + right.Dollar1Count,
			left.Dollar5Count + right.Dollar5Count,
			left.Dollar20Count + right.Dollar20Count);
	}

	public static Money operator -(Money left, Money right)
	{
		return new Money(
			left.Cents1Count - right.Cents1Count,
			left.Cents10Count - right.Cents10Count,
			left.Cents25Count - right.Cents25Count,
			left.Dollar1Count - right.Dollar1Count,
			left.Dollar5Count - right.Dollar5Count,
			left.Dollar20Count - right.Dollar20Count);
	}

	public static Money operator *(Money left, int multiplier)
	{
		return new Money(
			left.Cents1Count * multiplier,
			left.Cents10Count * multiplier,
			left.Cents25Count * multiplier,
			left.Dollar1Count * multiplier,
			left.Dollar5Count * multiplier,
			left.Dollar20Count * multiplier);
	}

	protected override bool EqualsCore(Money other)
	{
		return Cents1Count == other.Cents1Count
		       && Cents10Count == other.Cents10Count
		       && Cents25Count == other.Cents25Count
		       && Dollar1Count == other.Dollar1Count
		       && Dollar5Count == other.Dollar5Count
		       && Dollar20Count == other.Dollar20Count;
	}

	protected override int GetHashCodeCore()
	{
		unchecked
		{
			int hashCode = Cents1Count;
			hashCode = (hashCode * 397) ^ Cents10Count;
			hashCode = (hashCode * 397) ^ Cents25Count;
			hashCode = (hashCode * 397) ^ Dollar1Count;
			hashCode = (hashCode * 397) ^ Dollar5Count;
			hashCode = (hashCode * 397) ^ Dollar20Count;
			return hashCode;
		}
	}

	public override string ToString()
	{
		if (Amount < 1) return "c" + Amount * 100;
		return "$" + Amount.ToString("0.00");
	}

	public bool CanAllocate(decimal amount)
	{
		Money money = AllocateCore(amount);
		return money.Amount == amount;
	}

	public Money AllocateCore(decimal amount)
	{
		int twentyDollarCount = Math.Min((int)(amount / 20), Dollar20Count);
		amount = amount - twentyDollarCount * 20;

		int fiveDollarCount = Math.Min((int)(amount / 5), Dollar5Count);
		amount = amount - fiveDollarCount * 5;

		int oneDollarCount = Math.Min((int)amount, Dollar1Count);
		amount = amount - oneDollarCount;

		int quarterCount = Math.Min((int)(amount / 0.25m), Cents25Count);
		amount = amount - quarterCount * 0.25m;

		int tenCentCount = Math.Min((int)(amount / 0.1m), Cents10Count);
		amount = amount - tenCentCount * 0.1m;

		int oneCentCount = Math.Min((int)(amount / 0.01m), Cents1Count);

		return new Money(
			oneCentCount,
			tenCentCount,
			quarterCount,
			oneDollarCount,
			fiveDollarCount,
			twentyDollarCount);
	}
}