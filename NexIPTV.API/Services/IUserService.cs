
using Microsoft.AspNetCore.Identity;
using NexIPTV.API.Data;
using NexIPTV.API.Entities;

namespace NexIPTV.API.Services
{
    public interface IUserService
    {
        Task TransferCreditsAsync(string senderId, string receiverId, decimal amount);
        Task ActivateUserAsync(string activatorId, string userId);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public async Task ActivateUserAsync(string activatorId, string userId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var activator = await _userManager.FindByIdAsync(activatorId);
                var user = await _userManager.FindByIdAsync(userId);

                if (activator.CreditBalance < 1)
                    throw new InvalidOperationException("Insufficient credits");

                activator.CreditBalance -= 1;
                user.IsTrial = false;
                user.ExpiryDate = DateTime.UtcNow.AddYears(1);
                user.ActivatedBy = activator;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
