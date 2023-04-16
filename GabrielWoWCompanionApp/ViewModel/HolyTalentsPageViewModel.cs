using GabrielWoWCompanionApp.Services;
using GabrielWoWCompanionApp.View;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private static int totalTalentCount = 0;
    internal int selectedProfile;

    public delegate void CheckProfile();
    public event CheckProfile CheckProfileEvent;

    partial void OnTotalTalentCountChanged(int value)
    {
        for(int i = 0; i < Talents.Count; i++)
        {
            Talents[i].TotalCount = TotalTalentCount;
        }
    }

    public ObservableCollection<Talent> Talents { get; set; }

    public HolyTalentsPageViewModel()
    {
        // Initialize the collection of talents
        Talents = TalentService.GetHolyTalents();

        selectedProfile = TalentProfiles.SelectedProfile;
        TalentProfiles.LoadTalents(Talents, TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr);
    }

    [RelayCommand]
    public void IncreaseTalentRank(object index)
    {
        if (!int.TryParse(index.ToString(), out int parsedIndex)) return;
        if (Talents[parsedIndex].CurrentRank < Talents[parsedIndex].MaxRank && Talents[parsedIndex].TotalRequiredTalents <= TotalTalentCount && TotalTalentCount < 71)
        {
            if (Talents[parsedIndex].ReferenceTalent != null && Talents[parsedIndex].ReferenceTalent.CurrentRank != Talents[parsedIndex].ReferenceTalent.MaxRank) return;
            
            Talents[parsedIndex].CurrentRank++;
            TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr[parsedIndex] = Talents[parsedIndex].CurrentRank;
            TotalTalentCount++;
            TalentProfiles.Profiles[TalentProfiles.SelectedProfile].TotalCount++;
        }
    }
}
