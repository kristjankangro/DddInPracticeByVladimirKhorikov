namespace Logic;

//Value Object
public class Money : ValueObject<Money>
{
	public int Cents1Count { get; private set; }
	public int Cents10Count { get; private set; }
	public int Cents25Count { get; private set; }
	public int Dollar1Count { get; private set; }
	public int Dollar5Count { get; private set; }
	public int Dollar20Count { get; private set; }

	public Money(
		int cents1Count,
		int cents10Count,
		int cents25Count,
		int dollar1Count,
		int dollar5Count,
		int dollar20Count)
	{
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
}