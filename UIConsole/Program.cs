// See https://aka.ms/new-console-template for more information

using ConsoleApp;
using DddInPractice.Logic;
using Logic;
using NHibernate;

internal class Program
{
	public static void Main(string[] args)
	{
		Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=DddInPractice;Trusted_Connection=true");


		SnackMachine snackMachine = new SnackMachineRepo().GetById(1L);
		
		var useCases = new UseCases(new SnackMachineViewModel(snackMachine));
			
		useCases.Report();
		useCases.ReturnMoney();
		useCases.BuySnack();
		
	}
}