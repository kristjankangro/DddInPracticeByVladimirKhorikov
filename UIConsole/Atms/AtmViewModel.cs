using ConsoleApp.Common;
using Logic.Atms;
using Logic.SharedKernel;

namespace ConsoleApp.Atms;

public class AtmViewModel : ViewModel
{
	private Atm _atm;

	public override string Caption => "Atm";
	public Money MoneyInside => _atm.MoneyInside;
	public string MoneyCharged => _atm.MoneyCharged.ToString("C2");

	public AtmViewModel(Atm atm)
	{
		_atm = atm;
	}
}