using GabrielWoWCompanionApp.Services;
using GabrielWoWCompanionApp.View;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseTalentViewModel
{
    private Dictionary<int, int> TierValues { get; set; } = new Dictionary<int, int>() {
        { 0, 0 },
        { 5, 0 },
        { 10, 0 },
        { 15, 0 },
        { 20, 0 },
        { 25, 0 },
        { 30, 0 },
        { 35, 0 },
        { 40, 0 },
        { 45, 0 },
        { 50, 0 }
    };

    [ObservableProperty]
    private static int totalTalentCount = 0;

    internal int selectedProfile;

    [ObservableProperty]
    public bool addTalentFlag;

    public delegate void CheckProfile();
    public event CheckProfile CheckProfileEvent;

    partial void OnTotalTalentCountChanged(int value)
    {
        for(int i = 0; i < Talents.Count; i++)
        {
            Talents[i].TotalCount = TotalTalentCount;
        }
    }

    // To be used for the delegate event on the page behind for Holy Talents
    public void OnAppearing()
    {
        CheckProfileEvent?.Invoke();
    }

    public ObservableCollection<Talent> Talents { get; set; }

    public HolyTalentsPageViewModel()
    {
        // Initialize the collection of talents
        Talents = TalentService.GetHolyTalents();
        AddTalentFlag = true;

        selectedProfile = TalentProfiles.SelectedProfile;
        TalentProfiles.LoadTalents(Talents, TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr);
    }

    [RelayCommand]
    public void ChangeSelectionMode(object mode)
    {
        if (mode.ToString() == "Add") AddTalentFlag = true;
        else AddTalentFlag = false;
    }

    [RelayCommand]
    public void IncreaseTalentRank(object index)
    {
        if (!int.TryParse(index.ToString(), out int parsedIndex)) return;

        if (AddTalentFlag)
        {
            if (Talents[parsedIndex].CurrentRank < Talents[parsedIndex].MaxRank && Talents[parsedIndex].TotalRequiredTalents <= TotalTalentCount && TotalTalentCount < 71)
            {
                if (Talents[parsedIndex].PreRequisitieTalent != null && Talents[parsedIndex].PreRequisitieTalent.CurrentRank != Talents[parsedIndex].PreRequisitieTalent.MaxRank) return;
                TierValues[Talents[parsedIndex].TotalRequiredTalents]++;
                Talents[parsedIndex].CurrentRank++;
                TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr[parsedIndex] = Talents[parsedIndex].CurrentRank;
                TotalTalentCount++;
                TalentProfiles.Profiles[TalentProfiles.SelectedProfile].TotalCount++;
            }
        }
        else
        {
            if (Talents[parsedIndex].CurrentRank != 0 && TotalTalentCount > 0 && TalentTierConditional(Talents[parsedIndex]))
            {
                if (Talents[parsedIndex].DependantTalent != null && Talents[parsedIndex].DependantTalent.CurrentRank != 0) return;
                TierValues[Talents[parsedIndex].TotalRequiredTalents]--;
                Talents[parsedIndex].CurrentRank--;
                TalentProfiles.Profiles[TalentProfiles.SelectedProfile].HolyTalentArr[parsedIndex] = Talents[parsedIndex].CurrentRank;
                TotalTalentCount--;
                TalentProfiles.Profiles[TalentProfiles.SelectedProfile].TotalCount--;
            }
        }
    }

    private bool TalentTierConditional(Talent talent)
    {
        return talent.TotalRequiredTalents == TierValues.LastOrDefault(x => x.Value > 0).Key ? true : TierValues.Where(x => x.Key < talent.TotalRequiredTalents + 5).Sum(x => x.Value) > talent.TotalRequiredTalents + 5;
    }
}
