namespace GabrielWoWCompanionApp.View;

// DisciplineTalentspage Bound to the DisciplineTalentsPageViewModel
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
            try
            {
                viewModel.OnAppearing();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Page Load Error", ex.Message, "OK");
            }
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
            try
            {
                if (viewModel.selectedProfile != viewModel.profileService.SelectedProfile)
                {
                    viewModel.selectedProfile = viewModel.profileService.SelectedProfile;
                    viewModel.LoadTalents(viewModel.PageSpecilization);
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Page Load Error", ex.Message, "OK");
            }
        }
    }
}