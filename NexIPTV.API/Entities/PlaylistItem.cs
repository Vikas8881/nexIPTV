using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.API.Entities
{
    public class PlaylistItem
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public string TvgName { get; set; }
        public string TvgLogo { get; set; }
        public string Url { get; set; }
        public ContentType Type { get; set; }
    }
    public enum ContentType { LiveTV, Movie, Series, Other }
}
