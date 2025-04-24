using NexIPTV.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Services
{
    // IResellerService.cs
    public interface IResellerService
    {
        Task TransferCreditsAsync(CreditTransferDto dto);
    }
}
