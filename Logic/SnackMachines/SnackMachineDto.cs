namespace DomainDrivenDesign.Logic.SnackMachines;

public class SnackMachineDto
{
	public long Id { get; protected set; }

	public decimal MoneyInside { get; protected set; }

	public SnackMachineDto(long id, decimal moneyInside)
	{
		Id = id;
		MoneyInside = moneyInside;
	}
}