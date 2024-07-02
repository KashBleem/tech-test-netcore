// GravatarService.cs

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class GravatarService : IGravatarService
{
    private readonly HttpClient _httpClient;
    private readonly string _gravatarApiKey;
    private readonly string _gravatarUrl;

    public GravatarService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _gravatarApiKey = configuration["GravatarAPIKey"];
        _gravatarUrl = configuration["GravatarURL"];

        _httpClient.Timeout = TimeSpan.FromSeconds(3);
    }

    public async Task<GravatarProfile> GetProfileDetailsAsync(string email)
    {
        try
        {
            string hash = CalculateHash(email.ToLower());
            string url = $"{_gravatarUrl}/profiles/{hash}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_gravatarApiKey}");

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                GravatarProfile profile = JsonConvert.DeserializeObject<GravatarProfile>(jsonResponse);
                return profile;
            }
            else
            {
                Console.WriteLine($"Failed to fetch Gravatar profile. Status code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while fetching Gravatar profile: {ex.Message}");
            return null;
        }
    }

    private string CalculateHash(string email)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
