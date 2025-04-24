using NexIPTV.Core.Entities;

namespace NexIPTV.API.Interfaces
{
    public interface IPlaylistParserService
    {
        Task<Playlist> ParsePlaylistAsync(string source, bool isXtream, string? username, string? password);
    }
}
