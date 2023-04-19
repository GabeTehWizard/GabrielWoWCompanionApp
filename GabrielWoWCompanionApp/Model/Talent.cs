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
        public int totalCount;

        partial void OnTotalCountChanged(int value)
        {
            ReplaceIcon();
        }

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
        public double TalentModifier { get; set; }

        public double TalentModifier2 { get; set; }

        public double TalentModifier3 { get; set; }

        [ObservableProperty]
        public int currentRank;

        public Talent PreRequisitieTalent { get; set; }

        public Talent DependantTalent { get; set; }

        public Talent DependantTalent2 { get; set; }

        partial void OnCurrentRankChanged(int value)
        {
            ReplaceDescription();
        }

        public int TotalRequiredTalents { get; set; }

        public void ReplaceDescription()
        {
            if (TalentModifier3 != 0) // For Focused Will Talent
            {
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier, 1), Math.Round(currentRank * TalentModifier2, 1) + 1, Math.Round(currentRank * TalentModifier3, 1) + 2);
            }
            else if (TalentModifier2 != 0)
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier,1) + (this.Name == "Rapture" ? 1 : 0), Math.Round(currentRank * TalentModifier2, 1));
            else
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier, 1));
        }

        [ObservableProperty]
        public string displayIconPath;

        [ObservableProperty]
        public string iconPath;

        partial void OnIconPathChanged(string value)
        {
            ReplaceIcon();
        }

        public string GrayIconPath => "gray_" + IconPath;

        public void ReplaceIcon()
        {
            if (this.TotalCount < TotalRequiredTalents || this.TotalCount == 71 || this.PreRequisitieTalent != null && this.PreRequisitieTalent.CurrentRank < this.PreRequisitieTalent.MaxRank)
            {
                if (this.CurrentRank != 0) 
                    this.DisplayIconPath = this.IconPath;
                else
                    this.DisplayIconPath = this.GrayIconPath;
            }
            else
                this.DisplayIconPath = this.IconPath;
        }
    }
}
