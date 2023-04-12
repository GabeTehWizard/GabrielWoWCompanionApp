using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Model
{
    public partial class Talent : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string name;

        public List<string> DescriptionList { get; set; }

        [ObservableProperty]
        public string descriptionTemplate;

        partial void OnDescriptionTemplateChanged(string value)
        {
            ReplaceDescription();
        }

        [ObservableProperty]
        public string description;

        public int MaxRank { get; set; }
        public int RequiredTalentId { get; set; }
        public double TalentModifier { get; set; }

        [ObservableProperty]
        public int currentRank;

        partial void OnCurrentRankChanged(int value)
        {
            ReplaceDescription();
        }

        public int TotalRequiredTalents { get; set; }

        public void ReplaceDescription()
        {
            this.Description = string.Format(DescriptionTemplate, currentRank * TalentModifier);
        }
    }
}
