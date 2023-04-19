namespace GabrielWoWCompanionApp.View;

public partial class ShadowTalentsPage : ContentPage
{
	public ShadowTalentsPage(ShadowTalentsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        viewModel.CheckProfileEvent += TalentCheckEventCall;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ShadowTalentsPageViewModel viewModel)
        {
            viewModel.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is ShadowTalentsPageViewModel viewModel)
        {
            viewModel.CheckProfileEvent -= TalentCheckEventCall;
        }
    }

    private void TalentCheckEventCall()
    {
        if (BindingContext is ShadowTalentsPageViewModel viewModel)
        {
            if (viewModel.selectedProfile != viewModel.profileService.SelectedProfile)
            {
                viewModel.selectedProfile = viewModel.profileService.SelectedProfile;
                viewModel.LoadTalents(viewModel.PageSpecilization);
            }
        }
    }
}