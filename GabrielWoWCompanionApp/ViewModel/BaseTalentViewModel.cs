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

        [RelayCommand]
        public void ModifyTalentRank(object index)
        {
            if (!int.TryParse(index.ToString(), out int talentIndex)) return;

            if (AddTalentFlag)
            {
                if (AddTalentConditionals(Talents[talentIndex]))
                {
                    if (CheckPrerequisiteTalent(Talents[talentIndex])) return;

                    IncrementTalents(Talents, talentIndex);
                    IncrementProfileTalents(talentIndex);
                    UpdateProfileTotalCount();
                }
            }
            else
            {
                if (RemoveTalentConditional(Talents[talentIndex]))
                {
                    if (CheckDependantTalents(Talents[talentIndex])) return;

                    DecrementTalents(Talents, talentIndex);
                    DecrementProfileTalents(talentIndex);
                    UpdateProfileTotalCount();
                }
            }
        }
        #endregion

        #region Void/Action Methods
        internal void UpdateProfileTotalCount()
        {
            TotalTalentCount = profileService.Profiles[selectedProfile].TotalCount;
        }

        internal void LoadTalents(Specilization specilization)
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

        internal void IncrementTalents(ObservableCollection<Talent> talents, int index)
        {
            talents[index].CurrentRank++;
            SpecilizationTalentCount++;
            TierValues[talents[index].TotalRequiredTalents]++;
        }

        internal void DecrementTalents(ObservableCollection<Talent> talents, int index)
        {
            talents[index].CurrentRank--;
            specilizationTalentCount--;
            TierValues[talents[index].TotalRequiredTalents]--;
        }

        internal void IncrementProfileTalents(int index)
        {
            if (PageSpecilization == Specilization.Discipline)
                profileService.Profiles[profileService.SelectedProfile].DisciplineTalentArr[index] = Talents[index].CurrentRank;
            else if (PageSpecilization == Specilization.Holy)
                profileService.Profiles[profileService.SelectedProfile].HolyTalentArr[index] = Talents[index].CurrentRank;
            else if (PageSpecilization == Specilization.Shadow)
                profileService.Profiles[profileService.SelectedProfile].ShadowTalentArr[index] = Talents[index].CurrentRank;

            profileService.Profiles[profileService.SelectedProfile].TotalCount++;
        }

        internal void DecrementProfileTalents(int index)
        {
            if (PageSpecilization == Specilization.Discipline)
                profileService.Profiles[profileService.SelectedProfile].DisciplineTalentArr[index] = Talents[index].CurrentRank;
            else if (PageSpecilization == Specilization.Holy)
                profileService.Profiles[profileService.SelectedProfile].HolyTalentArr[index] = Talents[index].CurrentRank;
            else if (PageSpecilization == Specilization.Shadow)
                profileService.Profiles[profileService.SelectedProfile].ShadowTalentArr[index] = Talents[index].CurrentRank;

            profileService.Profiles[profileService.SelectedProfile].TotalCount--;
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
        partial void OnTotalTalentCountChanged(int value)
        {
            for (int i = 0; i < Talents.Count; i++) { Talents[i].TotalCount = SpecilizationTalentCount; }
        }
        #endregion

        #region Custom Events

        public delegate void CheckProfile();
        public event CheckProfile CheckProfileEvent;

        // To be used for the delegate event on the page behind for Holy Talents
        public void OnAppearing() { CheckProfileEvent?.Invoke(); }
        #endregion
    }
}
