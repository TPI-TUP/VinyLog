using System.Text.Json;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class YoutubeService : IYoutubeService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public YoutubeService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string?> SearchAlbumVideoAsync(
        string album,
        string artist)
    {
        var apiKey = _configuration["YouTube:ApiKey"];

        var query = $"{artist} {album} full album";

        var url =
            $"https://www.googleapis.com/youtube/v3/search" +
            $"?part=snippet" +
            $"&maxResults=1" +
            $"&q={Uri.EscapeDataString(query)}" +
            $"&type=video" +
            $"&key={apiKey}";

        var json = await _httpClient.GetStringAsync(url);

        using JsonDocument doc = JsonDocument.Parse(json);

        var items = doc.RootElement.GetProperty("items");

        if (items.GetArrayLength() == 0)
        {
            return null;
        }

        return items[0]
            .GetProperty("id")
            .GetProperty("videoId")
            .GetString();
    }
}