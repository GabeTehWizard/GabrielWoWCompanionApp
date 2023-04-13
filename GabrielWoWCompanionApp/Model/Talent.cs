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

        [ObservableProperty]
        public int currentRank;

        public Talent ReferenceTalent { get; set; }

        partial void OnCurrentRankChanged(int value)
        {
            ReplaceDescription();
        }

        public int TotalRequiredTalents { get; set; }

        public void ReplaceDescription()
        {
            if (TalentModifier2 != 0)
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier), Math.Round(currentRank * TalentModifier2));
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
            if (this.TotalCount < TotalRequiredTalents || this.TotalCount == 71 || this.ReferenceTalent != null && this.ReferenceTalent.CurrentRank < this.ReferenceTalent.MaxRank)
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
