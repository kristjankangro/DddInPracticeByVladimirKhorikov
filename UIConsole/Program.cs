// See https://aka.ms/new-console-template for more information

using ConsoleApp;
using Logic;

Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");

new UseCases().Return();
new UseCases().BuySnack();



Console.WriteLine("Hello, World!");

public class UseCases
{
	private readonly SnackMachineViewModel _viewModel = new SnackMachineViewModel(new SnackMachine());

	public void Return()
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
		Console.WriteLine(_viewModel.MoneyInTransaction);
		Console.WriteLine(_viewModel.MoneyInside);
	}

	public void BuySnack()
	{
		Console.WriteLine(">>insert money and buy a snack");
		_viewModel.InsertCent25Command.Execute("igno");
		_viewModel.InsertDollar20Command.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.BuySnackCommand.Execute("igno");
		Console.WriteLine(_viewModel.MoneyInTransaction);
		Console.WriteLine(_viewModel.MoneyInside);
	}
}