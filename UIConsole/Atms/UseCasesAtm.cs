namespace ConsoleApp.Atms;

public class UseCasesAtm
{
	private readonly AtmViewModel _viewModel;

	public UseCasesAtm(AtmViewModel atmViewModel)
	{
		_viewModel = atmViewModel;
	}

	public void DoSomeAtmStuff(AtmViewModel atmViewModel)
	{
		Console.WriteLine("You charged your money");
	}
}