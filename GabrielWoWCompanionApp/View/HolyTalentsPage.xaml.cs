namespace GabrielWoWCompanionApp.View;

public partial class HolyTalentsPage : ContentPage
{
    public HolyTalentsPage(HolyTalentsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.CheckProfileEvent += TalentCheckEventCall;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HolyTalentsPageViewModel viewModel)
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

        if (BindingContext is HolyTalentsPageViewModel viewModel)
        {
            viewModel.CheckProfileEvent -= TalentCheckEventCall;
        }
    }

    private void TalentCheckEventCall()
    {
        if (BindingContext is HolyTalentsPageViewModel viewModel)
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