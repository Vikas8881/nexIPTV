namespace NexIPTV.API.Interfaces
{
    public interface IUserService
    {
        Task TransferCreditsAsync(string senderId, string receiverId, decimal amount);
        Task ActivateUserAsync(string activatorId, string userId);
    }
}