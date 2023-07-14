// Code by Gabriel Atienza-Norris, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class LogPageViewModel : BaseViewModel
{
    #region Data Fields
    IConnectivity connectivity;                         // Represents App Connectivity Status i.e. Connected to Internet / Not
    WarcraftLogsService warcraftLogsService;            // Variable to Hold the WarcraftLogs Service
    #endregion

    #region Observable Properties
    [ObservableProperty]
    bool isRefreshing;                                  // Flag to Prevent Over Calling API

    [ObservableProperty]
    decimal allStarPoints;                              // Variable to Store All Star Points from the API

    [ObservableProperty]
    decimal rank;                                       // Variable to Store Rank from the API

    [ObservableProperty]
    decimal perfAvg;                                    // Variable to Store Performance Average from the API

    [ObservableProperty]
    decimal killsLogged;                                // Variable to Store Total Boss Kills from the API

    [ObservableProperty]
    decimal medianPerfAverage;                          // Stores the Median Performance Average from the API

    [ObservableProperty]
    string selectedItem;                                // Property for Future Use

    [ObservableProperty]
    int selectedIndexSpec;                              // Store the Selected Index from the Specilization Picker

    [ObservableProperty]
    int selectedIndexMetric;                            // Store the Selected Metric from the Specilization Picker

    public ObservableCollection<HealingLogs> HealingLogs { get; set; } = new(); // Collection to Hold the Logs from the API

    public ObservableCollection<string> specialization { get; set; } = // Collection to be Used in the Specilization Picker
        new() { nameof(Specilization.Discipline), nameof(Specilization.Holy), nameof(Specilization.Shadow) };

    public ObservableCollection<string> metric { get; set; } = // Collection to be Used in the Metric Picker
        new() { "Healing", "Damage" };
    #endregion

    #region Constructor
    public LogPageViewModel(IConnectivity connectivity, WarcraftLogsService warcraftLogsService)
    {
        this.connectivity = connectivity;               // Assign the Current Connectivity Service
        this.warcraftLogsService = warcraftLogsService; // Assign the Warcraft Logs Service 
        this.SelectedIndexSpec = 0;                     // Set the Default Index for the Metric Picker
        this.SelectedIndexMetric = 0;                   // Set the Default Index for the Metric Picker
    }
    #endregion

    #region Methods / Commands
    [RelayCommand]                                      
    async Task GetLogsAsync()                           // API Call to Retrieve Warcraft Log Data
    {
        if (IsBusy) return;                             // If Data is Still Loading Ignore Request and Return to Program

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Issue!", "Check your internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var logs = await warcraftLogsService.GetRankings(GetSpecByIndex(SelectedIndexSpec), GetMetricByIndex(SelectedIndexMetric));

            if (logs.Count != 0)
                HealingLogs.Clear();                    // Clear the Logs if They Were Refreshed

            foreach (var log in logs)
                HealingLogs.Add(log);                   // Add the Logs to the Log Object

            var asp = await warcraftLogsService.GetAllStarPoints(GetSpecByIndex(SelectedIndexSpec), GetMetricByIndex(SelectedIndexMetric));

            AllStarPoints = asp[0];
            Rank = asp[1];
            PerfAvg = asp[2];

            MedianPerfAverage = 98.3m;

            KillsLogged = await warcraftLogsService.GetKillsLogged();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get logs: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    private string GetSpecByIndex(int selectedIndex) => selectedIndex == 0 ? "Discipline" : selectedIndex == 1 ? "Holy" : "Shadow";

    private string GetMetricByIndex(int selectedIndex) => selectedIndex == 0 ? "hps" : "dps";
    #endregion
}

