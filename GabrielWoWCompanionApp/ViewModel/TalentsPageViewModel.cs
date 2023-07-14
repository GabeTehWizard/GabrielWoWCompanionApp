// Code by Gabriel Atienza-Norris, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class TalentsPageViewModel : BaseTalentViewModel
{
    #region Observable Property

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

    #endregion

    #region Constructor
    public TalentsPageViewModel(TalentProfileService talentProfileService)
    {
        profileService = talentProfileService;                  // Inject the Profile Service Dependancy
        selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing
    }
    #endregion

    #region Void / Action Methods

    public async Task InitializeTalentsAsync()
    {
        try
        {
            IsBusy = true;
            await Task.Run(() => profileService.GetTalentProfiles());
            DiscTalents = profileService.Profiles[selectedProfile].DisciplineTalentCount;
            HolyTalents = profileService.Profiles[selectedProfile].HolyTalentCount;
            ShadowTalents = profileService.Profiles[selectedProfile].ShadowTalentCount;
            ProfileName = profileService.Profiles[selectedProfile].Name;
            IsBusy = false;
        }
        catch (IndexOutOfRangeException) // Note GetTalentProfiles Already Throws an Exception
        {
            throw new IndexOutOfRangeException("Error accessing talent profile.");
        }
        finally { IsBusy = false; }
    }

    #endregion

    #region Relay Commands

    [RelayCommand]
    public void RefreshTalents()
    {
        try
        {
            IsRefreshing = true;
            DiscTalents = profileService.Profiles[selectedProfile].DisciplineTalentArr.Sum();
            HolyTalents = profileService.Profiles[selectedProfile].HolyTalentArr.Sum();
            ShadowTalents = profileService.Profiles[selectedProfile].ShadowTalentArr.Sum();
            IsRefreshing = false;
        }
        catch (IndexOutOfRangeException)
        {
            throw new IndexOutOfRangeException("Error calculating talent totals.");
        }
        finally { IsRefreshing = false; }
    }

    [RelayCommand]
    public async Task SaveProfileAsync()
    {
        try
        {
            await profileService.UpdateTalentProfile();
            await Shell.Current.DisplayAlert("Update Successful", $"Your profile {ProfileName} has been successfully updated in the database.", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Save Error", "Your profile could not be saved to the database: " + ex.Message, "OK");
        }
    }

    #endregion

    #region OnChangedMethods
    partial void OnProfileNameChanged(string value)
    {
        try
        {
            profileService.Profiles[selectedProfile].Name = value;
        }
        catch (IndexOutOfRangeException)
        {
            Shell.Current.DisplayAlert("Profile Name Error", "Could not update profile name.", "OK");
        }
    }
    #endregion
}
