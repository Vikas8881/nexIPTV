namespace NexIPTV.API.Extensions
{
    public static class NullCheckExtensions
    {
        public static T ThrowIfNull<T>(this T? value, string paramName) where T : class
        {
            return value ?? throw new ArgumentNullException(paramName);
        }
    }
}
