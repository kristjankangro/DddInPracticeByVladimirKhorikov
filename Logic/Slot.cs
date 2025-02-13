namespace Logic;

public class Slot : Entity
{
	
	public virtual SnackPile SnackPile { get; set; }
	public virtual SnackMachine SnackMachine { get; protected set; }
	public virtual int Position { get; protected set; }

	public Slot()
	{
	}

	public Slot(SnackMachine snackMachine, int position) : this()
	{
		SnackPile = new SnackPile(null, 0,0);
		SnackMachine = snackMachine;
		Position = position;
	}
}