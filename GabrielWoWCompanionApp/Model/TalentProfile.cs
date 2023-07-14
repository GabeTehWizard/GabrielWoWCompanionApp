//Code by Gabriel Atienza-Norris

namespace GabrielWoWCompanionApp.Model
{
    /// <summary>
    /// Talent Profile Class
    /// Stores Talent Positional and Count Data
    /// </summary>
    public class TalentProfile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public int DisciplineTalentCount { get; set; }
        public int[] DisciplineTalentArr { get; set; } = new int[28];
        public int HolyTalentCount { get; set; }
        public int[] HolyTalentArr { get; set; } = new int[27];
        public int ShadowTalentCount { get; set; }
        public int[] ShadowTalentArr { get; set; } = new int[27];
    }
}
