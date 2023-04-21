using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Model
{
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
