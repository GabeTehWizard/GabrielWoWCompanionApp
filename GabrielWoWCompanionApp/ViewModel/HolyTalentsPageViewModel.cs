// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseTalentViewModel
{
    public HolyTalentsPageViewModel(TalentProfileService talentProfileService)
    {
        try
        {
            Talents = TalentService.GetHolyTalents();               // Initialize the collection of talents
            profileService = talentProfileService;                  // Inject the Profile Service Dependancy
            selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing
            TotalTalentCount = profileService.Profiles[selectedProfile].TotalCount; // Assign the Talent Count on Instantiation

            // Load Existing Talent Profile State
            PageSpecilization = Specilization.Holy;
            LoadTalents(PageSpecilization);
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Page Load Error", ex.Message, "OK");
        }
    }
}
