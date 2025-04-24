using NexIPTV.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.API.Services
{
    // IM3UParser.cs
    public interface IM3UParser
    {
        Playlist ParseM3UContent(string m3uContent);
    }
}
