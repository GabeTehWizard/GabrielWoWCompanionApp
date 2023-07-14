//Code by Gabriel Atienza-Norris

namespace GabrielWoWCompanionApp.Model
{
    /// <summary>
    /// Data Transfer Object Class used for sending talent data to my personally created API/Database.
    /// </summary>
    class TalentProfileDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public int DisciplineTalentCount { get; set; }
        public string DisciplineTalentArr { get; set; } 
        public int HolyTalentCount { get; set; }
        public string HolyTalentArr { get; set; } 
        public int ShadowTalentCount { get; set; }
        public string ShadowTalentArr { get; set; }
    }
}
