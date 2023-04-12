namespace GabrielWoWCompanionApp.View;

public partial class TalentsPage : ContentPage
{
	public TalentsPage(TalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}