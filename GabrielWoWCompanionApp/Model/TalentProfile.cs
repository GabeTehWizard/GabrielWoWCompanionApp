namespace GabrielWoWCompanionApp.Model;

public static class TalentProfiles
{
    public static int SelectedProfile { get; set; } = 0;
    public static List<TalentProfile> Profiles { get; set; } = new List<TalentProfile>()
    {
        new TalentProfile() { Name = "Talent Set 1", TotalCount = 0 }
    };

    public static void LoadTalents(ObservableCollection<Talent> talents, int[] talentArr)
    {
        for(int i = 0; i < talents.Count; i++)
        {
            talents[i].currentRank = talentArr[i] - 0;
        }
    }
}

public class TalentProfile
{
    public string Name { get; set; }
    public int TotalCount { get; set; }
    public int DisciplineTalentCount { get; set; }
    public string DisciplineTalentArr { get; set; } = new string('0', 26);
    public int HolyTalentCount { get; set; } 
    public int[] HolyTalentArr { get; set; } = new int[27];
    public int ShadowTalentCount { get; set; }
    public string ShadowTalentArr { get; set; } = new string('0', 26);
}