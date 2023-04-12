namespace GabrielWoWCompanionApp.View;

public partial class ShadowTalentsPage : ContentPage
{
	public ShadowTalentsPage(ShadowTalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}