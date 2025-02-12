// See https://aka.ms/new-console-template for more information

using ConsoleApp;
using Logic;

// Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");

new UseCases().ShowMoneyInTransaction();



Console.WriteLine("Hello, World!");

public class UseCases
{
	private readonly SnackMachineViewModel _viewModel = new SnackMachineViewModel(new SnackMachine());

	public void ShowMoneyInTransaction()
	{
		Console.WriteLine("insert money in transaction");
		_viewModel.InsertDollarCommand.Execute("igno");
		_viewModel.InsertCent25Command.Execute("igno");
		_viewModel.InsertDollar20Command.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		_viewModel.InsertCentCommand.Execute("igno");
		Console.WriteLine(_viewModel.MoneyInTransaction);
		
	}
}