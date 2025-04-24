using NexIPTV.API.Interfaces;
using NexIPTV.Core.Entities;
using System.Net;
using System.Text.RegularExpressions;

namespace NexIPTV.API.Services
{
    public class PlaylistParserService : IPlaylistParserService
    {
        private readonly HttpClient _httpClient;

        public PlaylistParserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Playlist> ParsePlaylistAsync(string source, bool isXtream, string? username, string? password)
        {
            return isXtream
                ? await ParseXtreamPlaylist(source, username, password)
                : await ParseM3UPlaylist(source);
        }

        private async Task<Playlist> ParseM3UPlaylist(string url)
        {
            var content = await _httpClient.GetStringAsync(url);
            var playlist = new Playlist
            {
                Source = url,
                IsXtream = false,
                LastUpdated = DateTime.UtcNow
            };

            PlaylistItem? currentItem = null;
            var lines = content.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    currentItem = new PlaylistItem
                    {
                        Title = GetAttribute(line, "tvg-name") ?? "Untitled",
                        Category = GetAttribute(line, "group-title") ?? "General",
                        EpgId = GetAttribute(line, "tvg-id"),
                        HasCatchup = line.Contains("catchup"),
                        CatchupSource = GetAttribute(line, "catchup-source")
                    };
                }
                else if (!string.IsNullOrWhiteSpace(line) && currentItem != null)
                {
                    currentItem.Url = WebUtility.HtmlDecode(line.Trim());
                    currentItem.Type = DetectContentType(currentItem.Url);
                    playlist.Items.Add(currentItem);
                    currentItem = null;
                }
            }

            return playlist;
        }

        private async Task<Playlist> ParseXtreamPlaylist(string host, string? username, string? password)
        {
            var apiUrl = $"{host}/player_api.php?username={username}&password={password}";
            var response = await _httpClient.GetFromJsonAsync<XtreamResponse>(apiUrl);

            return new Playlist
            {
                Source = host,
                IsXtream = true,
                Host = host,
                Username = username,
                Password = password,
                LastUpdated = DateTime.UtcNow,
                Items = response.Streams.Select(s => new PlaylistItem
                {
                    Title = s.Name,
                    Category = response.Categories.First(c => c.Id == s.CategoryId).Name,
                    Url = $"{host}/{s.StreamType}/{username}/{password}/{s.StreamId}",
                    Type = s.StreamType switch
                    {
                        "live" => ContentType.Live,
                        "movie" => ContentType.Movie,
                        "series" => ContentType.Series,
                        _ => ContentType.Other
                    },
                    EpgId = s.EpgId,
                    HasCatchup = s.CatchupSupported
                }).ToList()
            };
        }

        private ContentType DetectContentType(string url)
        {
            var uri = new Uri(url);
            return uri.AbsolutePath switch
            {
                string p when p.Contains("/live/") => ContentType.Live,
                string p when p.Contains("/movie/") => ContentType.Movie,
                string p when p.Contains("/series/") => ContentType.Series,
                _ => ContentType.Other
            };
        }

        private string? GetAttribute(string line, string attribute)
        {
            var match = Regex.Match(line, $@"{attribute}=""([^""]*)""");
            return match.Success ? match.Groups[1].Value : null;
        }

        private class XtreamResponse
        {
            public List<XtreamCategory> Categories { get; set; }
            public List<XtreamStream> Streams { get; set; }
        }

        private class XtreamCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class XtreamStream
        {
            public int StreamId { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }
            public string StreamType { get; set; }
            public string EpgId { get; set; }
            public bool CatchupSupported { get; set; }
        }
    }
}
