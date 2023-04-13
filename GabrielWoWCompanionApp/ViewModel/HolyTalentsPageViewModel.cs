using GabrielWoWCompanionApp.Services;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private static int totalTalentCount = 0;

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
    }

    [RelayCommand]
    public void IncreaseTalentRank(object index)
    {
        if (!int.TryParse(index.ToString(), out int parsedIndex)) return;
        if (Talents[parsedIndex].CurrentRank < Talents[parsedIndex].MaxRank && Talents[parsedIndex].TotalRequiredTalents <= TotalTalentCount && TotalTalentCount < 71)
        {
            if (Talents[parsedIndex].ReferenceTalent != null && Talents[parsedIndex].ReferenceTalent.CurrentRank != Talents[parsedIndex].ReferenceTalent.MaxRank) return;
            
            Talents[parsedIndex].CurrentRank++;
            TotalTalentCount++;
        }
    }
}
