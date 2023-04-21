// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class TalentsPageViewModel : BaseTalentViewModel
{
    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    int discTalents;

    [ObservableProperty]
    int holyTalents;

    [ObservableProperty]
    int shadowTalents;

    [ObservableProperty]
    string profileName;

    public TalentsPageViewModel(TalentProfileService talentProfileService)
    {
        profileService = talentProfileService;                  // Inject the Profile Service Dependancy
        selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing
    }

    public async Task InitializeTalentsAsync()
    {
        IsBusy = true;
        await Task.Run(() => profileService.GetTalentProfiles());
        DiscTalents = profileService.Profiles[selectedProfile].DisciplineTalentCount;
        HolyTalents = profileService.Profiles[selectedProfile].HolyTalentCount;
        ShadowTalents = profileService.Profiles[selectedProfile].ShadowTalentCount;
        ProfileName = profileService.Profiles[selectedProfile].Name;
        IsBusy = false;
    }

    [RelayCommand]
    public void RefreshTalents()
    {
        IsRefreshing = true;
        DiscTalents = profileService.Profiles[selectedProfile].DisciplineTalentArr.Sum();
        HolyTalents = profileService.Profiles[selectedProfile].HolyTalentArr.Sum();
        ShadowTalents = profileService.Profiles[selectedProfile].ShadowTalentArr.Sum();
        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task SaveProfileAsync()
    {
        await profileService.UpdateTalentProfile();
        await Shell.Current.DisplayAlert("Update Successful", $"Your profile {ProfileName} has been successfully updated in the database.", "OK");
    }

    partial void OnProfileNameChanged(string value)
    {
        profileService.Profiles[selectedProfile].Name = value;
    }
}
