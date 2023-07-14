// Code by Gabriel Atienza-Norris, 04/20/2023
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace GabrielWoWCompanionApp.ViewModel
{
    public partial class BaseTalentViewModel : BaseViewModel
    {
        #region Data Fields 

        internal TalentProfileService profileService;       // Variable to Hold the Injected Talent Profile Service
        internal int selectedProfile;                       // The Index of the Currently Selected Profile

        #endregion

        #region Properties

        public Specilization PageSpecilization { get; set; } // Variable to Hold the Page Specilization | Initialized in the VM Constructor
        internal Dictionary<int, int> TierValues { get; set; } = new Dictionary<int, int>() { // Class level Dictionary Used to Store the Current Talent Point Values
            { 0, 0 },
            { 5, 0 },
            { 10, 0 },
            { 15, 0 },
            { 20, 0 },
            { 25, 0 },
            { 30, 0 },
            { 35, 0 },
            { 40, 0 },
            { 45, 0 },
            { 50, 0 }
        };

        #endregion

        #region Observable Properties

        [ObservableProperty]
        internal int totalTalentCount = 0;                  // Variable to Store the Total Talent Count on This Page

        [ObservableProperty]
        internal int specilizationTalentCount = 0;          // Variable to Store Current Specilization Talent Count

        [ObservableProperty]
        internal bool addTalentFlag = true;                 // Flag to Denote the Talent Selection Mode   

        public ObservableCollection<Talent> Talents { get; set; } // Collection to Populate View Data

        #endregion

        #region Relay Commands

        [RelayCommand] // Command to Toggle the Talent Selection Flag
        public void ChangeSelectionMode(object mode)
        {
            if (mode.ToString() == "Add") AddTalentFlag = true;
            else AddTalentFlag = false;
        }

        [RelayCommand] // Command to Update Talent Based Objects on Button Click
        public void ModifyTalentRank(object index)
        {
            if (!int.TryParse(index.ToString(), out int talentIndex)) return;

            try
            {
                if (AddTalentFlag)
                {
                    if (AddTalentConditionals(Talents[talentIndex]))
                    {
                        if (CheckPrerequisiteTalent(Talents[talentIndex])) return;

                        IncrementTalents(Talents, talentIndex);
                        ModifyProfileTalents(talentIndex);
                        UpdateProfileTotalCount();
                    }
                }
                else
                {
                    if (RemoveTalentConditional(Talents[talentIndex]))
                    {
                        if (CheckDependantTalents(Talents[talentIndex])) return;

                        DecrementTalents(Talents, talentIndex);
                        ModifyProfileTalents(talentIndex);
                        UpdateProfileTotalCount();
                    }
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Talent Update Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        public void ClearTalents()
        {
            try
            {
                SpecilizationTalentCount = 0;
                int talentSum = TierValues.Sum(x => x.Value);
                TotalTalentCount = profileService.Profiles[selectedProfile].TotalCount;
                TotalTalentCount -= talentSum;
                profileService.Profiles[selectedProfile].TotalCount -= talentSum;
                TierValues = TierValues.Keys.ToDictionary(key => key, value => 0);
                for (int i = Talents.Count - 1; i > -1; i--)
                {
                    Talents[i].CurrentRank = 0;
                    Talents[i].TotalCount = 0;
                    if (PageSpecilization == Specilization.Discipline)
                        profileService.Profiles[selectedProfile].DisciplineTalentArr[i] = 0;
                    else if (PageSpecilization == Specilization.Holy)
                        profileService.Profiles[selectedProfile].HolyTalentArr[i] = 0;
                    else if (PageSpecilization == Specilization.Shadow)
                        profileService.Profiles[selectedProfile].ShadowTalentArr[i] = 0;

                    if (Talents[i].TotalRequiredTalents != 0) Talents[i].DisplayIconPath = Talents[i].GrayIconPath;
                }
            }
            catch (Exception)
            {
                Shell.Current.DisplayAlert("Talent Update Error", "There was a problem clearing your talents.", "OK");
            }
        }
        #endregion

        #region Void/Action Methods
        internal void UpdateProfileTotalCount() // Method to Increment the Total Count of Profile Object
        {
            try
            {
                TotalTalentCount = profileService.Profiles[selectedProfile].TotalCount;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("There was a problem accessing your talent profile.");
            }
        }

        internal void LoadTalents(Specilization specilization) // Method Used to Load a Profile's Talent Settings Into the Page Talents Collection
        {
            try
            {
                if (specilization == Specilization.Discipline)
                    TalentProfileService.LoadTalents(Talents, profileService.Profiles[profileService.SelectedProfile].DisciplineTalentArr, profileService.Profiles[selectedProfile].DisciplineTalentCount);
                else if (specilization == Specilization.Holy)
                    TalentProfileService.LoadTalents(Talents, profileService.Profiles[profileService.SelectedProfile].HolyTalentArr, profileService.Profiles[selectedProfile].HolyTalentCount);
                else if (specilization == Specilization.Shadow)
                    TalentProfileService.LoadTalents(Talents, profileService.Profiles[profileService.SelectedProfile].ShadowTalentArr, profileService.Profiles[selectedProfile].ShadowTalentCount);

                TierValues = TierValues.Keys.ToDictionary(key => key, value => 0);

                foreach (Talent t in Talents)
                {
                    TierValues[t.TotalRequiredTalents] += t.CurrentRank;
                }
            }
            catch (IndexOutOfRangeException ex)
            { 
                if (!ex.Message.Contains("Error updating talent at index"))
                {
                    throw new IndexOutOfRangeException("Error accessing talent profile.");
                }
                else
                {
                    throw ex;                                       // Assumes this is the Exception from the LoadTalents in the TalentProfileService
                }
            }
        }

        internal void IncrementTalents(ObservableCollection<Talent> talents, int index) // Increment Class Level Collections
        {
            try
            {
                talents[index].CurrentRank++;
                SpecilizationTalentCount++;
                TierValues[talents[index].TotalRequiredTalents]++;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Error increasing talent rank.");
            }
        }

        internal void DecrementTalents(ObservableCollection<Talent> talents, int index) // Increment Class Level Collections
        {
            try
            {
                talents[index].CurrentRank--;
                specilizationTalentCount--;
                TierValues[talents[index].TotalRequiredTalents]--;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Error decreasing talent rank.");
            }
        }

        internal void ModifyProfileTalents(int index) // Assign the Profile Talent Array the Current Talent Ranks
        {
            try
            {
                if (PageSpecilization == Specilization.Discipline)
                    profileService.Profiles[profileService.SelectedProfile].DisciplineTalentArr[index] = Talents[index].CurrentRank;
                else if (PageSpecilization == Specilization.Holy)
                    profileService.Profiles[profileService.SelectedProfile].HolyTalentArr[index] = Talents[index].CurrentRank;
                else if (PageSpecilization == Specilization.Shadow)
                    profileService.Profiles[profileService.SelectedProfile].ShadowTalentArr[index] = Talents[index].CurrentRank;

                if (AddTalentFlag)
                    profileService.Profiles[profileService.SelectedProfile].TotalCount++;
                else
                    profileService.Profiles[profileService.SelectedProfile].TotalCount--;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Error modifying talent rank.");
            }
            
        }
        #endregion

        #region Conditional Encapsulation Methods

        internal bool AddTalentConditionals(Talent talent) => BelowPointCap(talent) && RequiredPointsAllocated(talent) && CheckTalentLimit();
        internal bool RemoveTalentConditional(Talent talent) => talent.CurrentRank != 0 && AbovePointFloor() && CheckTalentTier(talent);
        internal bool CheckPrerequisiteTalent(Talent talent) => talent.PreRequisitieTalent != null && talent.PreRequisitieTalent.CurrentRank != talent.PreRequisitieTalent.MaxRank;
        internal bool CheckDependantTalent(Talent talent) => talent.DependantTalent != null && talent.DependantTalent.CurrentRank != 0;
        internal bool CheckDependantTalents(Talent talent)
        {
            if (this.PageSpecilization == Specilization.Shadow)
            {
                if (talent.DependantTalent2 != null && talent.DependantTalent2.CurrentRank != 0)
                    if (talent.DependantTalent != null && talent.DependantTalent.CurrentRank != 0)
                        return true;
                return false;
            }
            else 
                return CheckDependantTalent(talent);
        }
        internal bool BelowPointCap(Talent talent) => talent.CurrentRank < talent.MaxRank;
        internal bool RequiredPointsAllocated(Talent talent) => talent.TotalRequiredTalents <= Talents.First().TotalCount;
        internal bool CheckTalentLimit() => TotalTalentCount < 71;
        internal bool AbovePointFloor() => Talents.First().TotalCount > 0;
        internal bool CheckTalentTier(Talent talent)
        {
            return talent.TotalRequiredTalents == TierValues.LastOrDefault(x => x.Value > 0).Key ? true
                : TierValues.Where(x => x.Key < talent.TotalRequiredTalents + 5).Sum(x => x.Value) > talent.TotalRequiredTalents + 5;
        }

        #endregion

        #region Property Changed Events
        partial void OnTotalTalentCountChanged(int value) // Event to Modify Each Talent in the Talent Collection and Invoke the Talent Class Custom Event
        {
            try
            {
                for (int i = 0; i < Talents.Count; i++) { Talents[i].TotalCount = SpecilizationTalentCount; }
            }
            catch (IndexOutOfRangeException)
            {
                Shell.Current.DisplayAlert("Talent Error","Error modifying talent count.", "OK");
            }
        }
        #endregion

        #region Custom Events

        // Declared Delegate/Event to Invoke on Page Navigated To. Unfortunately, there is currently a bug with that method stated by microsoft.
        // https://github.com/dotnet/maui/issues/8102
        /// <summary>
        /// We've moved this issue to the Backlog milestone. This means that it is not going to be worked on for the coming release. We will 
        /// reassess the backlog following the current release and consider this item at that time. To learn more about our issue management 
        /// process and to have better expectation regarding different types of issues you can read our Triage Process.
        /// </summary>

        public delegate void CheckProfile();
        public event CheckProfile CheckProfileEvent;

        // To be used for the delegate event on the page behind for Talents
        public void OnAppearing() { CheckProfileEvent?.Invoke(); }
        #endregion
    }
}
