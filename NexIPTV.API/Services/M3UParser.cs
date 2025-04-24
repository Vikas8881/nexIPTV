using NexIPTV.API.Models;
using NexIPTV.Core.Entities;
using System.Text.RegularExpressions;

namespace NexIPTV.API.Services
{
    public interface IM3UParser
    {
        ParsedPlaylist Parse(string m3uContent);
    }

    public class M3UParser : IM3UParser
    {
        public ParsedPlaylist Parse(string m3uContent)
        {
            var playlist = new ParsedPlaylist();
            var lines = m3uContent.Split('\n');
            PlaylistItem? currentItem = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    currentItem = new PlaylistItem
                    {
                        Metadata = ParseExtInf(line)
                    };
                }
                else if (!string.IsNullOrWhiteSpace(line) && currentItem != null)
                {
                    currentItem.Url = line.Trim();
                    playlist.Items.Add(currentItem);
                    currentItem = null;
                }
            }

            return playlist;
        }

        private Metadata ParseExtInf(string line)
        {
            var metadata = new Metadata();
            var groupMatch = Regex.Match(line, @"group-title=""([^""]*)""");
            var nameMatch = Regex.Match(line, @"tvg-name=""([^""]*)""");
            var logoMatch = Regex.Match(line, @"tvg-logo=""([^""]*)""");

            metadata.GroupTitle = groupMatch.Success ? groupMatch.Groups[1].Value : null;
            metadata.TvgName = nameMatch.Success ? nameMatch.Groups[1].Value : null;
            metadata.TvgLogo = logoMatch.Success ? logoMatch.Groups[1].Value : null;
            metadata.Type = DetectContentType(metadata.GroupTitle, metadata.TvgName);

            return metadata;
        }

        private ContentType DetectContentType(string? group, string? name)
        {
            if (group?.Contains("movie", StringComparison.OrdinalIgnoreCase) == true)
                return ContentType.Movie;

            if (name?.Contains(" S", StringComparison.OrdinalIgnoreCase) == true)
                return ContentType.Series;

            return ContentType.Live;
        }
    }
}