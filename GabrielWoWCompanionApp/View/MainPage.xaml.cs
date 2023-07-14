namespace GabrielWoWCompanionApp.View;


// Main Page Bound to MainPageViewModel
public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}