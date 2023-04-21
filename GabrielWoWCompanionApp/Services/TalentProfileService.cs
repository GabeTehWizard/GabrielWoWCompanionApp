// Code by Gabriel Atienza-Norris
using System.Net.Http.Json;

namespace GabrielWoWCompanionApp.Model;

/// <summary>
/// Talent Profile Class Intended to Store Profile Data for the Application
/// Intended for Multiple Proifiles
/// </summary>
public class TalentProfileService
{
    HttpClient httpClient;
    string profileApiURL = "https://gabrielwowapi.azurewebsites.net/api";
    public int SelectedProfile { get; set; } = 0;       // Currently Selected Profile (By Index)
    public List<TalentProfile> Profiles { get; set; } = new List<TalentProfile>() // A List of the Talent Profiles
    {
        new TalentProfile() { Name = "Talent Set 1", TotalCount = 0 }
    };

    public TalentProfileService() 
    {
        httpClient = new HttpClient();
    }

    public async Task<List<TalentProfile>> GetTalentProfiles()
    {
        var url = $"{profileApiURL}/TalentProfiles";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<TalentProfileDTO> dtoProfiles = await response.Content.ReadFromJsonAsync<List<TalentProfileDTO>>(options);
            
            // Convert the DTO into Profile Objects
            var profiles = dtoProfiles.Select(x => new TalentProfile
            {
                ID = x.ID,
                Name = x.Name,
                TotalCount = x.TotalCount,
                DisciplineTalentCount = x.DisciplineTalentCount,
                DisciplineTalentArr = x.DisciplineTalentArr.Select(x => int.Parse(x.ToString())).ToArray(),
                HolyTalentCount = x.HolyTalentCount,
                HolyTalentArr = x.HolyTalentArr.Select(x => int.Parse(x.ToString())).ToArray(),
                ShadowTalentCount = x.ShadowTalentCount,
                ShadowTalentArr = x.ShadowTalentArr.Select(x => int.Parse(x.ToString())).ToArray(),
            }).ToList();

            Profiles = profiles;

            return profiles;
        }
        else
        {
            throw new Exception("Could not load profile data from server :c!");
        }
    }

    public async Task UpdateTalentProfile()
    {
        TalentProfile profile = this.Profiles[this.SelectedProfile];
        TalentProfileDTO profileDTO = new()
        {
            ID = profile.ID,
            Name = profile.Name,
            TotalCount = profile.TotalCount,
            DisciplineTalentArr = string.Concat(profile.DisciplineTalentArr.Select(x => x.ToString())),
            DisciplineTalentCount = profile.DisciplineTalentArr.Sum(),
            HolyTalentArr = string.Concat(profile.HolyTalentArr.Select(x => x.ToString())),
            HolyTalentCount = profile.HolyTalentArr.Sum(),
            ShadowTalentArr = string.Concat(profile.ShadowTalentArr.Select(x => x.ToString())),
            ShadowTalentCount = profile.ShadowTalentArr.Sum()
        };

        var response = await httpClient.PutAsJsonAsync($"{profileApiURL}/TalentProfiles/{profileDTO.ID}", profileDTO);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not Access the Server to Update Talent Profile.");
        }
    }

    // Utility Method to Load Profile Data
    public static void LoadTalents(ObservableCollection<Talent> talents, int[] talentArr, int totalCount)
    {
        for(int i = 0; i < talents.Count; i++)
        {
            talents[i].CurrentRank = talentArr[i];
            talents[i].TotalCount = totalCount;
        }
    }
}