using GabrielWoWCompanionApp.Services;

namespace GabrielWoWCompanionApp.ViewModel;

public partial class LogPageViewModel : BaseViewModel
{
    IConnectivity connectivity;
    WarcraftLogsService warcraftLogsService;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    decimal allStarPoints;

    [ObservableProperty]
    decimal rank;

    [ObservableProperty]
    decimal perfAvg;

    [ObservableProperty]
    decimal killsLogged;

    [ObservableProperty]
    string selectedItem;

    [ObservableProperty]
    int selectedIndexSpec;

    [ObservableProperty]
    int selectedIndexMetric;

    [ObservableProperty]
    decimal medianPerfAverage;

    public ObservableCollection<HealingLogs> HealingLogs { get; set; } = new();

    public ObservableCollection<string> specialization { get; set; } = new() { "Discipline", "Holy", "Shadow" };

    public ObservableCollection<string> metric { get; set; } = new() { "Healing", "Damage" };

    public LogPageViewModel(IConnectivity connectivity, WarcraftLogsService warcraftLogsService)
    {
        this.connectivity = connectivity;
        this.warcraftLogsService = warcraftLogsService;
        this.SelectedIndexSpec = 0;
        this.SelectedIndexMetric = 0;
        //SelectedItem = specialization.FirstOrDefault();
    }

    [RelayCommand] // Aka ICommand
    async Task GetLogsAsync()
    {
        if (IsBusy) return; // If Data is still loading ignore

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
                HealingLogs.Clear();

            foreach (var log in logs)
                HealingLogs.Add(log);

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
}

