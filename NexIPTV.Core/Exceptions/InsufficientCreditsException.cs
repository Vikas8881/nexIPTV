using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Exceptions
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException()
            : base("Insufficient credits to perform this action") { }
    }
}