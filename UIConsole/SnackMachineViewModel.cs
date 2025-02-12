using System.Globalization;
using ConsoleApp.Common;
using Logic;
using NHibernate;

namespace ConsoleApp
{
    public class SnackMachineViewModel : ViewModel
    {
        private readonly SnackMachine _snackMachine;

        public Command InsertCentCommand { get; private set; }
        public Command InsertCent10Command { get; private set; }
        public Command InsertCent25Command { get; private set; }
        public Command InsertDollarCommand { get; private set; }
        public Command InsertDollar5Command { get; private set; }
        public Command InsertDollar20Command { get; private set; }
        
        public SnackMachineViewModel(SnackMachine snackMachine)
        {
            _snackMachine = snackMachine;
            
            InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
            InsertCent10Command = new Command(() => InsertMoney(Money.Cent10));
            InsertCent25Command = new Command(() => InsertMoney(Money.Cent25));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertDollar5Command = new Command(() => InsertMoney(Money.Dollar5));
            InsertDollar20Command = new Command(() => InsertMoney(Money.Dollar20));
        }

        public override string Caption => "Snack Machine";
        public string MoneyInTransaction => $"Current money in transaction:  {_snackMachine.MoneyInTransaction}";

        private void InsertMoney(Money money)
        {
            _snackMachine.InsertMoney(money);
            Console.WriteLine($"Inserted money:  {money.ToString()}");
            // Notify("MoneyInTransaction");
        }
    }
}
