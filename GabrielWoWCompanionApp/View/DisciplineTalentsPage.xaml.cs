namespace GabrielWoWCompanionApp.View;

public partial class DisciplineTalentsPage : ContentPage
{
	public DisciplineTalentsPage(DisciplineTalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        viewModel.CheckProfileEvent += TalentCheckEventCall;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DisciplineTalentsPageViewModel viewModel)
        {
            viewModel.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is DisciplineTalentsPageViewModel viewModel)
        {
            viewModel.CheckProfileEvent -= TalentCheckEventCall;
        }
    }

    private void TalentCheckEventCall()
    {
        if (BindingContext is DisciplineTalentsPageViewModel viewModel)
        {
            if (viewModel.selectedProfile != viewModel.profileService.SelectedProfile)
            {
                viewModel.selectedProfile = viewModel.profileService.SelectedProfile;
                viewModel.LoadTalents(viewModel.PageSpecilization);
            }
        }
    }
}