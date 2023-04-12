using GabrielWoWCompanionApp.Services;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class HolyTalentsPageViewModel : BaseViewModel
{
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
        if (Talents[parsedIndex].CurrentRank < Talents[parsedIndex].MaxRank) Talents[parsedIndex].CurrentRank++;
    }
}
