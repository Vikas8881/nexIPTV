using NexIPTV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Services
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
                    // Parse logic
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
            // Implement regex parsing for:
            // group-title="(.*?)"
            // tvg-name="(.*?)"
            // tvg-logo="(.*?)"
        }
    }
}
