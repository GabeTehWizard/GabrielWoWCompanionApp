namespace GabrielWoWCompanionApp.View;

// LogPage Bound to the LogPagePageViewModel 
public partial class LogPage : ContentPage
{
	public LogPage(LogPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}