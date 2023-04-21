// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.Model
{
    public partial class Talent : ObservableObject
    {
        #region Properties
        public string GrayIconPath => "gray_" + IconPath;   // Used to Set the Gray Pictures String Path for Display
        public int TotalRequiredTalents { get; set; }       // Threshold to Allow Talent to Be Updated
        public int MaxRank { get; set; }                    // The Highest Value a Single Talent Can Progress to
        public double TalentModifier { get; set; }          // Value Used In Talent String Assignment
        public double TalentModifier2 { get; set; }         // Value Used In Talent String Assignment
        public double TalentModifier3 { get; set; }         // Value Used In Talent String Assignment
        public Talent PreRequisitieTalent { get; set; }     // Reference to a the Talent Required to Learn this Specific Talent (Can be Null)
        public Talent DependantTalent { get; set; }         // Reference to a Talent that Requires this Specific Talent to Learn (Can be Null)
        public Talent DependantTalent2 { get; set; }        // Reference to a Second Talent that Requires this Specific Talent to Learn
        #endregion

        #region Observable Properties
        [ObservableProperty]
        public int totalCount;                              // Count of the Total Talents of This Specilization (Used for Image Display)

        [ObservableProperty]
        public int id;                                      // Talent ID

        [ObservableProperty]
        public string name;                                 // Talent Name

        [ObservableProperty]
        public string descriptionTemplate;                  // Pre-Formatted String that Will be Used for Dispaly in Combination with Talent Modifier Properties

        [ObservableProperty]
        public string description;                          // Description Created by the Template and Adjusted by the Talent Modifiers

        [ObservableProperty]
        public int currentRank;                             // Current Skill Rank

        [ObservableProperty]
        public string displayIconPath;                      // The Actual String Path Property for the Talent Icon (Either iconPath, or Gray Icon Path)

        [ObservableProperty]
        public string iconPath;                             // The Coloured Version of the Icon to be Displayed
        #endregion

        #region Action / Void Methods
        public void ReplaceDescription()                    // Method for the Description Display Based on Current Rank
        {
            if (TalentModifier3 != 0)                       // For Focused Will Talent
            {
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier, 1), Math.Round(currentRank * TalentModifier2, 1) + 1, Math.Round(currentRank * TalentModifier3, 1) + 2);
            }
            else if (TalentModifier2 != 0)
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier, 1) + (this.Name == "Rapture" ? 1 : 0), Math.Round(currentRank * TalentModifier2, 1));
            else
                this.Description = string.Format(DescriptionTemplate, Math.Round(currentRank * TalentModifier, 1));
        }

        public void ReplaceIcon()                           // Method to Replace the Talent Icon Path if the Total Count for the Specific Talent Set is at the Threshold
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
        #endregion

        #region Event Methods / OnChanged
        partial void OnTotalCountChanged(int value)         // Change Talent Icon from Gray => Coloured and Vice Versa
        {
            ReplaceIcon();
        }

        partial void OnDescriptionTemplateChanged(string value) // Update the Description on Assignment
        {
            ReplaceDescription();
        }

        partial void OnCurrentRankChanged(int value)        // Update the Description for the Current Rank * Modifier Values
        {
            ReplaceDescription();
        }

        partial void OnIconPathChanged(string value)        // Update the IconPath on Assignment
        {
            ReplaceIcon();
        }
        #endregion
    }
}
