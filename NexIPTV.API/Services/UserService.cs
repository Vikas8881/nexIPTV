using Microsoft.AspNetCore.Identity;
using NexIPTV.API.Data;
using NexIPTV.API.Entities;
using NexIPTV.API.Exceptions;
using NexIPTV.API.Interfaces;

namespace NexIPTV.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // In UserService.cs
        public async Task TransferCreditsAsync(string senderId, string receiverId, decimal amount)
        {
            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);

            // Implementation with actual await calls
            sender.CreditBalance -= amount;
            receiver.CreditBalance += amount;

            await _context.SaveChangesAsync();
        }
      
        public async Task ActivateUserAsync(string activatorId, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var activator = await _userManager.FindByIdAsync(activatorId)
                    ?? throw new UserNotFoundException(activatorId);

                var user = await _userManager.FindByIdAsync(userId)
                    ?? throw new UserNotFoundException(userId);

                if (activator.CreditBalance < 1)
                    throw new InsufficientCreditsException();

                // Deduct 1 credit from reseller
                activator.CreditBalance -= 1;
                await _userManager.UpdateAsync(activator);

                // Activate user
                user.IsTrial = false;
                user.ExpiryDate = DateTime.UtcNow.AddYears(1);
                user.ActivatedBy = activator;
                await _userManager.UpdateAsync(user);

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