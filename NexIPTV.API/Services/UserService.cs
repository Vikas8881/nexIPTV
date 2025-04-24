using Microsoft.AspNetCore.Identity;
using NexIPTV.API.Data;
using NexIPTV.API.Entities;

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

        public async Task TransferCreditsAsync(string senderId, string receiverId, decimal amount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var sender = await _userManager.FindByIdAsync(senderId);
                var receiver = await _userManager.FindByIdAsync(receiverId);

                if (sender.CreditBalance < amount)
                    throw new InsufficientCreditsException();

                sender.CreditBalance -= amount;
                receiver.CreditBalance += amount;

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