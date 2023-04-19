using GabrielWoWCompanionApp.Services;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class ShadowTalentsPageViewModel : BaseTalentViewModel
{
    public ShadowTalentsPageViewModel(TalentProfileService talentProfileService)
    {
        Talents = TalentService.GetShadowTalents();             // Initialize the collection of talents
        profileService = talentProfileService;                  // Inject the Profile Service Dependancy
        selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing

        // Load Existing Talent Profile State
        PageSpecilization = Specilization.Shadow;
        LoadTalents(PageSpecilization);
    }
}
