namespace GabrielWoWCompanionApp.View;

public partial class HolyTalentsPage : ContentPage
{
    public HolyTalentsPage(HolyTalentsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.CheckProfileEvent += TalentCheckEventCall;
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
            if (viewModel.selectedProfile != TalentProfiles.SelectedProfile)
            {
                viewModel.selectedProfile = TalentProfiles.SelectedProfile;
                TalentProfiles.LoadTalents(viewModel.Talents, TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr);
            }
        }
    }
}