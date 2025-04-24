using NexIPTV.API.DTOs;
using NexIPTV.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.API.Services
{
    // IResellerService.cs
    public interface IResellerService
    {
        Task TransferCreditsAsync(CreditTransferDto dto);
        Task<List<ApplicationUser>> GetResellerHierarchyAsync();
    }
}
