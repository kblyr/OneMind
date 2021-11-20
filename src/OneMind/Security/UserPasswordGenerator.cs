using System.Security.Cryptography;

namespace OneMind.Security;

sealed class UserPasswordGenerator : IUserPasswordGenerator
{
    public string Generate()
    {
        var data = RandomNumberGenerator.GetBytes(8);
        return Encoding.UTF8.GetString(data);
    }
}
