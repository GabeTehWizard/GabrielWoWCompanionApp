// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class ShadowTalentsPageViewModel : BaseTalentViewModel
{
    public ShadowTalentsPageViewModel(TalentProfileService talentProfileService)
    {
        Talents = TalentService.GetShadowTalents();             // Initialize the collection of talents
        profileService = talentProfileService;                  // Inject the Profile Service Dependancy
        selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing
        TotalTalentCount = profileService.Profiles[selectedProfile].TotalCount; // Assign the Talent Count on Instantiation

        // Load Existing Talent Profile State
        PageSpecilization = Specilization.Shadow;
        LoadTalents(PageSpecilization);
    }
}
