namespace DomainDrivenDesign.Logic.Atms;

public class AtmDto
{
	public long Id { get; protected set; }

	public decimal Cash { get; protected set; }

	public AtmDto(long id, decimal cash)
	{
		Id = id;
		Cash = cash;
	}
}