using ConsoleApp.Common;
using Logic.Atms;
using Logic.SharedKernel;

namespace ConsoleApp.Atms;

public class AtmViewModel : ViewModel
{
	private Atm _atm;
	private AtmRepo _repo;
	private PaymentGateway _paymentGateway;

	public override string Caption => "Atm";
	public Money MoneyInside => _atm.MoneyInside;
	public string MoneyCharged => _atm.MoneyCharged.ToString("C2");
	public Command<decimal> TakeMoneyCommand { get; private set; }

	private string _message;

	public string Message
	{
		get { return _message; }
		private set
		{
			_message = value;
			Notify();
		}
	}

	public AtmViewModel(Atm atm)
	{
		_atm = atm;
		_paymentGateway = new PaymentGateway();
		_repo = new AtmRepo();
		TakeMoneyCommand = new Command<decimal>(x => x > 0, TakeMoney);
	}

	private void TakeMoney(decimal amount)
	{
		string error = _atm.CanTakeMoney(amount);
		if (error != string.Empty)
		{
			NotifyClient(error);
			return;
		}
		
		var amountWithCharge = _atm.CalculateAmountWithComission(amount);
		_paymentGateway.ChargePayemnt(amountWithCharge);
		_atm.TakeMoney(amount);
		_repo.Save(_atm);

		// var headOffice = GetHeadOfficeInstance();
		// headOffice.Balane += amountWithCharge;
		// _officeRepository.Save(headOffice);
		NotifyClient($"You have taken money {amount:C2}.");
	}

	private void NotifyClient(string message)
	{
		Message = message;
		Notify(nameof(MoneyInside));
		Notify(nameof(MoneyCharged));
		// Notify(nameof(Piles));
		
		Console.WriteLine(Message);
	}
	
}