using Logic;

namespace ConsoleApp;

public class UseCases
{
	private SnackMachineViewModel _viewModel;

	public UseCases(SnackMachineViewModel viewModel)
	{
		_viewModel = viewModel;
	}


	public void ReturnMoney() => ReturnAndReport();
	public void BuySnack() => BuySnackAndReport();
	public void Report() => Summary();

	private void ReturnAndReport()
	{
		Console.WriteLine(">>insert money but return");
		_viewModel.InsertDollarCommand.Execute("igno");
		_viewModel.InsertCent25Command.Execute("igno");
		_viewModel.InsertDollar20Command.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.ReturnMoneyCommand.Execute("igno");

		Summary();
	}

	private void BuySnackAndReport()
	{
		Console.WriteLine(">>insert money and buy a snack");
		_viewModel.InsertCent25Command.Execute("igno");
		_viewModel.InsertDollar20Command.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.BuySnackCommand.Execute("igno");
		Summary();
	}
	
	public void InsertDollarBuy3DollarSnack()
	{
		Console.WriteLine(">>insert money and buy a snack");
		_viewModel.InsertDollarCommand.Execute("igno");
		_viewModel.BuySnackCommand.Execute("3");
		Summary();
	}

	private void Summary()
	{
		Console.WriteLine(_viewModel.MoneyInTransaction);
		Console.WriteLine(_viewModel.MoneyInside);
	}

}