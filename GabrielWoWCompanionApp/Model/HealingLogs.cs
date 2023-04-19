using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Model
{
    /// <summary>
    /// The HealingLogs class is used to represent the World of Warcraft fight log data from
    /// warcraft logs.
    /// </summary>
    public class HealingLogs 
    {        
            public int EncounterID { get; set; }
            public string EncounterName { get; set; }
            public string Class { get; set; }
            public string Spec { get; set; }
            public int Rank { get; set; }
            public int OutOf { get; set; }
            public int Difficulty { get; set; }
            public int Size { get; set; }
            public int CharacterID { get; set; }
            public string CharacterName { get; set; }
            public string Server { get; set; }
            public decimal Percentile { get; set; }
            public decimal Total { get; set; }
    }
}
