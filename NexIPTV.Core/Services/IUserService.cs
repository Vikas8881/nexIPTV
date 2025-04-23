using NexIPTV.Core.Entities;
using NexIPTV.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Services
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
