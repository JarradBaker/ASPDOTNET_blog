namespace DOTNET_DIARIES.Helpers;

public static class BlogHelpers
{
    /// <summary>
    /// Truncates a string to a specified maximum length and appends "..." if truncated.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum length of the string.</param>
    /// <returns>The truncated string with "..." if it was truncated, otherwise the original string.</returns>
    public static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
    }
}