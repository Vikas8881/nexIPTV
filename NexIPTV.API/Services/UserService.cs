using Microsoft.AspNetCore.Identity;
using NexIPTV.Core.Entities;
namespace NexIPTV.API.Services
{
    public class UserService
    {
        public async Task RequestActivation(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActivationPending = true;
            await _context.SaveChangesAsync();
        }

        public async Task ActivateUser(string activatorId, string targetUserId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var activator = await _userManager.FindByIdAsync(activatorId);
                var targetUser = await _userManager.FindByIdAsync(targetUserId);

                if (activator.CreditBalance < 1)
                    throw new InsufficientCreditsException();

                activator.CreditBalance -= 1;
                targetUser.ExpiryDate = DateTime.UtcNow.AddYears(1);
                targetUser.ActivatedBy = activator;

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
