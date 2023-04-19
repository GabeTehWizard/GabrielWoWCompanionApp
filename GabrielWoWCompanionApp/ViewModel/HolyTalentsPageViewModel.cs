using GabrielWoWCompanionApp.Services;
using GabrielWoWCompanionApp.View;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseTalentViewModel
{
    public HolyTalentsPageViewModel(TalentProfileService talentProfileService)
    {
        Talents = TalentService.GetHolyTalents();               // Initialize the collection of talents
        profileService = talentProfileService;                  // Inject the Profile Service Dependancy
        selectedProfile = profileService.SelectedProfile;       // Assign an Integer Designating the Currently Selected Profile to Utilize with Indexing

        // Load Existing Talent Profile State
        PageSpecilization = Specilization.Holy;
        LoadTalents(PageSpecilization);
    }
}
