using System.Text;

namespace OneMind.Security;
public static class IUserPasswordCryptoServiceExtensions
{
    public static string ComputeHash(this IUserPasswordCryptoService service, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return "";

        return BitConverter.ToString(service.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
    }
}
