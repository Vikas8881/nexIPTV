using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexIPTV.API.Data;
using NexIPTV.API.DTOs;
using NexIPTV.API.Entities;
using NexIPTV.API.Interfaces;

namespace NexIPTV.API.Services
{
    public class ResellerService : IResellerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public ResellerService(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task CreateResellerAsync(CreateResellerDto dto)
        {
            var reseller = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                CreditBalance = 0
            };

            var result = await _userManager.CreateAsync(reseller, dto.Password);
            if (!result.Succeeded)
                throw new Exception("Failed to create reseller");

            await _userManager.AddToRoleAsync(reseller, "Reseller");
        }
        public async Task TransferCreditsAsync(CreditTransferDto dto)
        {
            var fromUser = await _userManager.FindByIdAsync(dto.FromUserId);
            var toUser = await _userManager.FindByIdAsync(dto.ToUserId);

            if (fromUser == null || toUser == null)
                throw new Exception("User not found");

            // Implement credit transfer logic
            fromUser.CreditBalance -= dto.Amount;
            toUser.CreditBalance += dto.Amount;

            await _context.SaveChangesAsync();
        }
        public async Task<List<ApplicationUser>> GetResellerHierarchyAsync()
        {
            return await _context.Users
                .Where(u => u.ParentUserId != null)
                .ToListAsync();
        }

    }
}