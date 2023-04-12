namespace GabrielWoWCompanionApp.View;

public partial class HolyTalentsPage : ContentPage
{
	public HolyTalentsPage(HolyTalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}