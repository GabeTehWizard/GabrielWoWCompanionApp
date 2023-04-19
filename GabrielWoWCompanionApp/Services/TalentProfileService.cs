namespace GabrielWoWCompanionApp.Model;

public class TalentProfileService
{
    public int SelectedProfile { get; set; } = 0;
    public List<TalentProfile> Profiles { get; set; } = new List<TalentProfile>()
    {
        new TalentProfile() { Name = "Talent Set 1", TotalCount = 0 }
    };

    public TalentProfileService() { }

    public static void LoadTalents(ObservableCollection<Talent> talents, int[] talentArr, int totalCount)
    {
        for(int i = 0; i < talents.Count; i++)
        {
            talents[i].CurrentRank = talentArr[i];
            talents[i].TotalCount = totalCount;
        }
    }
}

public class TalentProfile
{
    public string Name { get; set; }
    public int TotalCount { get; set; }
    public int DisciplineTalentCount { get; set; }
    public int[] DisciplineTalentArr { get; set; } = new int[27];
    public int HolyTalentCount { get; set; } 
    public int[] HolyTalentArr { get; set; } = new int[27];
    public int ShadowTalentCount { get; set; }
    public int[] ShadowTalentArr { get; set; } = new int[27];
}