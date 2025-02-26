using Logic.Common;

namespace Logic;

public class Snack : AggregateRoot
{
	public static readonly Snack None = new Snack(0, "None");
	public static readonly Snack MarsBar = new Snack(2, "Mars Bar");
	public static readonly Snack Coca = new Snack(3, "Coca");
	public static readonly Snack Hubba = new Snack(4, "Hubba");
	
	public virtual string Name { get; protected set; }

	protected Snack()
	{
	}

	private Snack(long id, string name)
		: this()
	{
		Id = id;
		Name = name;
	}
}