namespace GabrielWoWCompanionApp.View;

public partial class DisciplineTalentsPage : ContentPage
{
	public DisciplineTalentsPage(DisciplineTalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}