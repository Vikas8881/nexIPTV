using NexIPTV.API.DTOs;
using NexIPTV.API.Entities;

namespace NexIPTV.API.Interfaces
{
    public interface IResellerService
    {
        Task CreateResellerAsync(CreateResellerDto dto);
        Task TransferCreditsAsync(CreditTransferDto dto);
        Task<List<ApplicationUser>> GetResellerHierarchyAsync();
    }
}