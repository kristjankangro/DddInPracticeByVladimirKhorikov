using Logic;

namespace ConsoleApp;

public class SnackPileViewModel
{
	private readonly SnackPile SnackPile;
	public string Price => SnackPile.Price.ToString("C2");
	public int ImageWidth => GetImageWidth(SnackPile.Snack);

	public string Image => "image from applicaiton";
	//public ImageSource Image => (ImageSource)Application.Current().FindResource("img" + SnackPile.Snack.Name);


	private int GetImageWidth(Snack snack)
	{
		if (snack == Snack.MarsBar) return 50;
		return 60;
	}

	public SnackPileViewModel(SnackPile snackPile)
	{
		SnackPile = snackPile;
	}
}