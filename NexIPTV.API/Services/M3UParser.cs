using NexIPTV.API.Entities;
using NexIPTV.API.Entities;

namespace NexIPTV.API.Services
{
    public class M3UParser : IM3UParser
    {
        public Playlist ParseM3UContent(string m3uContent)
        {
            var playlist = new Playlist();
            PlaylistItem currentItem = null;

            foreach (var line in m3uContent.Split('\n'))
            {
                if (line.StartsWith("#EXTINF"))
                {
                    currentItem = new PlaylistItem();
                    currentItem.Metadata = ParseExtinf(line);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    if (currentItem != null)
                    {
                        currentItem.Url = line;
                        playlist.Items.Add(currentItem);
                        currentItem = null;
                    }
                }
            }
            return playlist;
        }

        private Metadata ParseExtinf(string line)
        {
            // Implement parsing logic
            return new Metadata();
        }
    }

    public class Metadata
    {
        // Metadata properties
    }
}