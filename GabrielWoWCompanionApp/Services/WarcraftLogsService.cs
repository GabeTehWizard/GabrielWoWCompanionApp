using HtmlAgilityPack;
using System.Net.Http.Json;

namespace GabrielWoWCompanionApp.Services;

public  class WarcraftLogsService
{
    HttpClient httpClient;
    List<HealingLogs> parseList = new();
    int killsLogged;
    decimal allStarPoints;
    decimal rank;
    decimal perfAverage;

    public WarcraftLogsService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<HealingLogs>> GetRankings(string spec, string metric)
    {
        var url = $"https://classic.warcraftlogs.com/v1/rankings/character/Cla%C3%AFra/Whitemane/US?metric={metric}&timeframe=historical&api_key=8f7cffbff18ed8b91f25f5580f3f0e6a";

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

    public async Task<int> GetKillsLogged()
    {
        if (killsLogged > 0) return killsLogged;

        var url = "https://classic.warcraftlogs.com/v1/parses/character/Cla%C3%AFra/Whitemane/US?metric=hps&class=7&spec=1&timeframe=historical&api_key=8f7cffbff18ed8b91f25f5580f3f0e6a";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var parseList = await response.Content.ReadFromJsonAsync<List<HealingLogs>>(options);
            return parseList.Where(p => p.Size == 25).ToList().Count;
        }

        return killsLogged;
    }

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
}

public static class WarcraftLogsClient
{
    private static HttpClient _client;
    private static string clientProjectName = "WoWFinalSchoolProject";
    private static string clientID = "98e300ce-3604-4e8a-a04f-ddccbe85bf1e";
    private static string clientSecret = "5ctgbRkKcMwVKz0HXjQ8wuYy4X5pYLsJ09ewLirB";  
}
