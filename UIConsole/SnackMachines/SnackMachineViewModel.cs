using ConsoleApp.Common;
using DddInPractice.Logic;
using Logic;
using Logic.SharedKernel;

namespace ConsoleApp
{
	public class SnackMachineViewModel : ViewModel
	{
		private readonly SnackMachine _snackMachine;
		private readonly SnackMachineRepo _snackMachineRepo;
		private string _message;
		public override string Caption => "Snack Machine";
		public string MoneyInTransaction => $"Current money in transaction:  {_snackMachine.MoneyInTransaction}";
		public Money MoneyInside => _snackMachine.MoneyInside;

		public IReadOnlyList<SnackPileViewModel> Piles
		{
			get
			{
				return _snackMachine.GetAllSnackPiles()
					.Select(snackPile => new SnackPileViewModel(snackPile))
					.ToList();
			}
		}


		public string Message
		{
			get { return _message; }
			private set
			{
				_message = value;
				Notify();
			}
		}

		public Command InsertCentCommand { get; private set; }
		public Command InsertCent10Command { get; private set; }
		public Command InsertCent25Command { get; private set; }
		public Command InsertDollarCommand { get; private set; }
		public Command InsertDollar5Command { get; private set; }
		public Command InsertDollar20Command { get; private set; }

		public Command ReturnMoneyCommand { get; private set; }
		public Command<string> BuySnackCommand { get; private set; }


		public SnackMachineViewModel(SnackMachine snackMachine)
		{
			_snackMachine = snackMachine;
			_snackMachineRepo = new SnackMachineRepo();
			BuySnackCommand = new Command<string>(BuySnack);
			ReturnMoneyCommand = new Command(() => ReturnMoney());

			InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
			InsertCent10Command = new Command(() => InsertMoney(Money.Cent10));
			InsertCent25Command = new Command(() => InsertMoney(Money.Cent25));
			InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
			InsertDollar5Command = new Command(() => InsertMoney(Money.Dollar5));
			InsertDollar20Command = new Command(() => InsertMoney(Money.Dollar20));
		}

		private void BuySnack(string positionString)
		{
			int pos = int.Parse(positionString);

			string error = _snackMachine.CanBuySnack(pos);
			if (error != string.Empty)
			{
				NotifyClient(error);
				return;
			}
			
			_snackMachine.BuySnack(position:2);
			_snackMachineRepo.Save(_snackMachine);
			NotifyClient("You bought a snack");
		}

		private void InsertMoney(Money coinOrNote)
		{
			_snackMachine.InsertMoney(coinOrNote);
			NotifyClient("You have inserted " + coinOrNote);
		}

		private void ReturnMoney()
		{
			_snackMachine.ReturnMoney();
			NotifyClient("Money returned");
		}

		private void NotifyClient(string message)
		{
			Notify(nameof(MoneyInTransaction));
			Notify(nameof(MoneyInside));
			Notify(nameof(Piles));
			Message = message;
			Console.WriteLine(Message);
		}
	}
}