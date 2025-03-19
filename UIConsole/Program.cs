// See https://aka.ms/new-console-template for more information

using ConsoleApp;
using ConsoleApp.Atms;
using ConsoleApp.SnackMachines;
using Logic;
using Logic.Atms;

internal class Program
{
	public static void Main(string[] args)
	{
		Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");

		SnackMachine snackMachine = new SnackMachineRepo().GetById(1L);

		var useCases = new UseCasesSnackMachine(new SnackMachineViewModel(snackMachine));
		// useCases.Report();
		// useCases.ReturnMoney();
		// useCases.BuySnack();
		// useCases.InsertDollarBuy3DollarSnack();


		var atm = new AtmRepo().GetById(1L);
		var atmView = new AtmViewModel(atm);
		var usesCasesAtm = new UseCasesAtm(atmView);
		usesCasesAtm.TakeOneDollar();
	}
}