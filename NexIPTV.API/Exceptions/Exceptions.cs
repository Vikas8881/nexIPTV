namespace NexIPTV.API.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userId)
            : base($"User {userId} not found") { }
    }

    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException()
            : base("Not enough credits to activate user") { }
    }
}
