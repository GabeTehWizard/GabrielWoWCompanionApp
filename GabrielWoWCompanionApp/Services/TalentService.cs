using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Services;

public class TalentService
{


    public static ObservableCollection<Talent> GetHolyTalents()
    {
        return new ObservableCollection<Talent>
        {
            new() { Id = 1, Name = "Healing Focus", MaxRank = 2, Description = "Reduces the pushback suffered from damaging attacks while casting any healing spell by {0}%.", TalentModifier = 35},
            
            new() { Id = 2, Name = "Improved Renew", MaxRank = 3, DescriptionTemplate = "Increases the amount healed by your Renew spell by {0}%.", TalentModifier = 5 },


        };
    }
}
