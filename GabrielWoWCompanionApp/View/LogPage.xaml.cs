namespace GabrielWoWCompanionApp.View;

public partial class LogPage : ContentPage
{
	public LogPage(LogPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}