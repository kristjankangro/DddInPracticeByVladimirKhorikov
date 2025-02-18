namespace Logic;

public sealed class SnackPile : ValueObject<SnackPile>
{
	public static SnackPile Empty = new SnackPile(Snack.None, 0, 0);
	public Snack Snack { get; }
	public int Quantity { get; private set; }
	public decimal Price { get; }

	public SnackPile()
	{
	}

	public SnackPile(Snack snack, int quantity, decimal price)
	{
		if (quantity < 0 
		    || price < 0 
		    || price % 0.01m > 0) 
			throw new InvalidOperationException();

		Snack = snack;
		Quantity = quantity;
		Price = price;
	}

	protected override bool EqualsCore(SnackPile other)
	{
		return Snack == other.Snack
		       && Quantity == other.Quantity
		       && Price == other.Price;
	}

	protected override int GetHashCodeCore()
	{
		unchecked
		{
			var hashCode = Snack.GetHashCode();
			hashCode = (hashCode * 397) ^ Quantity;
			hashCode = (hashCode * 397) ^ Price.GetHashCode();
			return hashCode;
		}
	}

	public SnackPile SubtractOne()
	{
		return new SnackPile(Snack, --Quantity, Price);
	}
}