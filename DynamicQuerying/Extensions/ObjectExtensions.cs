namespace DynamicQuerying.Extensions
{
    internal static class ObjectExtensions
    {
        public static string AsString(this object value) => value?.ToString() ?? string.Empty;
    }
}