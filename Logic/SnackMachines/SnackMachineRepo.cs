using DomainDrivenDesign.Logic.SnackMachines;
using Logic.Common;

namespace Logic;

public class SnackMachineRepo : Repository<SnackMachine>
{
	public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
	{
		throw new NotImplementedException();
	}
}