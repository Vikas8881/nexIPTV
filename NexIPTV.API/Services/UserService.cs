using Microsoft.AspNetCore.Identity;
using NexIPTV.API.Data;
using NexIPTV.API.Entities;
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
            if (string.IsNullOrEmpty(activatorId))
                throw new ArgumentNullException(nameof(activatorId));
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var activator = await _userManager.FindByIdAsync(activatorId)
                    ?? throw new InvalidOperationException("Activator not found");

                var user = await _userManager.FindByIdAsync(userId)
                    ?? throw new InvalidOperationException("User not found");

                if (activator.CreditBalance < 1)
                    throw new InvalidOperationException("Insufficient credits");

                // Rest of the implementation...
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}