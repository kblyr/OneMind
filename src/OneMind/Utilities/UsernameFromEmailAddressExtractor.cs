namespace OneMind.Utilities;

sealed class UsernameFromEmailAddressExtractor : IUsernameFromEmailAddressExtractor
{
    public string Extract(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            return "";

        return emailAddress.Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0] ?? "";
    }
}