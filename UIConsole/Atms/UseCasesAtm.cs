namespace ConsoleApp.Atms;

public class UseCasesAtm
{
	private readonly AtmViewModel _viewModel;

	public UseCasesAtm(AtmViewModel atmViewModel)
	{
		_viewModel = atmViewModel;
	}

	public void TakeOneDollar()
	{
		Console.WriteLine(">>take one dollar usecase");
		_viewModel.TakeMoneyCommand.Execute(1m);
		Summary();
	}
	
	private void Summary()
	{
		Console.WriteLine(_viewModel.MoneyCharged);
		Console.WriteLine(_viewModel.MoneyInside);
	}
}