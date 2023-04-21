using HtmlAgilityPack;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.Services;

public  class WarcraftLogsService
{
    #region Data Fields
    int killsLogged;                                    // Field to Store Boss Kills Logged                           
    decimal allStarPoints;                              // Field to Store Today's All Star Points
    decimal rank;                                       // Field to Store Today's Rank
    decimal perfAverage;                                // Field to Store Today's Performance Average
    List<HealingLogs> parseList = new();                // Field to Store a List of Logs

    // Api URL and Key
    string warcraftLogsApiURL = "https://classic.warcraftlogs.com/v1";
    string apiKey = "8f7cffbff18ed8b91f25f5580f3f0e6a"; // My Custom Api Key from Warcraft Logs Website

    HttpClient httpClient; // Client to Connect to HTTP Server
    #endregion

    #region Constructors
    public WarcraftLogsService()
    {
        httpClient = new HttpClient();
    }
    #endregion

    #region Methods
    // API Call to Retrieve Character Parses (My Character)
    public async Task<List<HealingLogs>> GetRankings(string spec, string metric)
    {
        var url = $"{warcraftLogsApiURL}/rankings/character/Cla%C3%AFra/Whitemane/US?metric={metric}&timeframe=historical&api_key={apiKey}";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            parseList = await response.Content.ReadFromJsonAsync<List<HealingLogs>>(options);
        }

        // Filter the logs
        parseList = parseList.Where(p => p.Spec == spec && p.EncounterName != "Flame Leviathan" && p.Size == 25).ToList();

        return parseList;
    }

    // API Call to Retrieve the Total Kills Recorded (These Totals Are Updated Daily)
    public async Task<int> GetKillsLogged()
    {
        if (killsLogged > 0) return killsLogged;        // Return if the Kills Logged Data has Been Loaded Into the App Today Already

        var url = $"{warcraftLogsApiURL}/parses/character/Cla%C3%AFra/Whitemane/US?metric=hps&class=7&spec=1&timeframe=historical&api_key={apiKey}";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var parseList = await response.Content.ReadFromJsonAsync<List<HealingLogs>>(options);
            return parseList.Where(p => p.Size == 25).ToList().Count;
        }

        return killsLogged;
    }

    // Rather than Using the API, I get this Specific Data Directly from the Website Because it is not Accessible in their V1 API
    public async Task<List<decimal>> GetAllStarPoints(string spec, string metric)
    {
        if (allStarPoints != 0) return new List<decimal>() { allStarPoints, rank, perfAverage };
        var url = "";
        if (metric == "hps")
        {
            url = $"https://classic.warcraftlogs.com/character/us/whitemane/claïra#spec={spec}&class=Priest";
        }
        else
        {
            url = $"https://classic.warcraftlogs.com/character/us/whitemane/claïra#spec={spec}&class=Priest&metric=dps";
        }

        var response = await httpClient.GetAsync(url);

        string content = "";

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        }

        if (content == "") return new List<decimal>() { allStarPoints, rank, perfAverage };

        var doc = new HtmlDocument();
        doc.LoadHtml(content);

        doc.DocumentNode.Descendants()
                .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment")
                .ToList()
                .ForEach(n => n.Remove());


        var aspNode = doc.DocumentNode.SelectSingleNode("//div[@class='header-zone-ranks']");
        decimal.TryParse(aspNode.Descendants("span").FirstOrDefault(x => x.Attributes["class"].Value == "primary header-rank")?.InnerText, out allStarPoints);
        decimal.TryParse(aspNode.Descendants("span").LastOrDefault(x => x.Attributes["class"].Value.Contains("header-rank"))?.InnerText, out rank);
        var rankNode = aspNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("Title") && x.Attributes["Title"].Value.Contains("%"));
        decimal.TryParse(rankNode.Attributes.Contains("Title") ? rankNode.Attributes["Title"].Value.Trim('%') : null, out perfAverage);

        return new List<decimal>() { allStarPoints, rank, perfAverage };
    }
    #endregion
}