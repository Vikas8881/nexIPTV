using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Services
{
    public class M3UParser : IM3UParser
    {
        public Playlist ParseM3UContent(string m3uContent, string userId)
        {
            var playlist = new Playlist();
            var lines = m3uContent.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTM3U"))
                    continue;

                if (line.StartsWith("#EXTINF"))
                {
                    var currentItem = new PlaylistItem();
                    currentItem.Metadata = ParseExtinf(line);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    currentItem.Url = line;
                    playlist.Items.Add(currentItem);
                }
            }

            return playlist;
        }

        private Metadata ParseExtinf(string line)
        {
            // Implement regex parsing for:
            // group-title="(.*?)"
            // tvg-name="(.*?)"
            // tvg-logo="(.*?)"
        }
    }
}
